using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RatingApp.DAL;
using RatingApp.Domain;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System;
using Microsoft.EntityFrameworkCore;
using RatingApp.Web.Controllers.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RatingApp.Web.Controllers.Api
{
    
    [Authorize]
    public class SkillsController : Controller
    {

        private RatingDBContext _context;

        public SkillsController(RatingDBContext context)
        {
            _context = context;
        } 
      
        [HttpGet]
        public IEnumerable<Skill> GetAll()
        {
            return _context.Skills.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Skill Get(int id)
        {
            return _context.Skills.FirstOrDefault(e => e.SkillId == id);
        }

        //[Route("api/search/{value}")]

        [HttpPost]
        public JsonResult Search([FromBody]SearchModel model)
        {
            var result = _context.Skills.Where(data => data.SkillName.StartsWith(model.SearchTerm.Trim())).ToList();

            if (result.Count==0 && !string.IsNullOrWhiteSpace(model.SearchTerm)){

                var skill = new Skill
                {
                    SkillId = 0,
                    SkillName = model.SearchTerm
                };
                result = new List<Skill>();
                result.Add(skill);

            }
            return Json(result);
        }

        [HttpGet]
        public JsonResult UserSkills() { 

             var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            {
                var userSkills = _context.UserSkills.Include(p=> p.Skill).OrderBy(p=>p.Skill.SkillName).Where(u => u.UserId == userId).ToList();
                return Json(userSkills);
            }
           
        }

        [HttpPost]
        public IActionResult Add([FromBody]SkillAddModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            int skillId;

            if (!int.TryParse(model.SkillId, out skillId))
                return BadRequest();

            //Do not allow another set of skill and user in DB
            if (_context.UserSkills.Any(p => p.SkillId == skillId && p.UserId == userId))
            {

                return BadRequest();
            }

            //New skill to be added
            if (skillId == 0)
            {
                var skill = new Skill
                {
                    SkillName = model.SkillName
                };
                _context.Skills.Add(skill);
                _context.SaveChanges();
                skillId = skill.SkillId;
            }

            else
            {
                skillId = int.Parse(model.SkillId);
                if (skillId != 0)
                {
                    var skill = _context.Skills.FirstOrDefault(p => p.SkillId == skillId);
                    skillId = skill.SkillId;
                }
            }
            
    
            var userSkill = new UserSkill
            {
                UserId= userId,
                SkillId = skillId,
                Added = DateTime.Now
            };
                _context.UserSkills.Add(userSkill);
                _context.SaveChanges();

                return Ok();
        }

        [HttpDelete]
        public IActionResult UserSkills([FromBody] UserSkillDeleteModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int skillIId = int.Parse(model.SkillId);

            var userSkill = _context.UserSkills.FirstOrDefault(p => p.SkillId == skillIId && p.UserId == userId);

            if (userSkill != null)
            {

                _context.UserSkills.Remove(userSkill);
                _context.SaveChanges();
                return Ok();

            }
            else
                return BadRequest("Unable to remove skill");
        }

        

        public IActionResult UserSkillLevel ([FromBody]UserSkillLevelModel model)
        {

            if (ModelState.IsValid)
            {

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                int skillIId = int.Parse(model.SkillId);
                int level = int.Parse(model.Level);

                if (level < 1)
                    return BadRequest();

                var userSkill = _context.UserSkills.Where(p => p.SkillId == skillIId && p.UserId == userId).FirstOrDefault();

                if (userSkill == null) {
                    return NotFound();
                        }
                else {
                    userSkill.Level = level;
                    userSkill.Updated = DateTime.Now;
                    _context.SaveChanges();
                }
                return Ok();
            }

            else return BadRequest("Missing arguments for SkillId or level");
        }
     
    }
}
