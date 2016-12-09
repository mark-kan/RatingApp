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
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RatingApp.Web.Controllers.Api
{
    
    [Authorize]
    public class SkillsController : Controller
    {

       
        private IRatingRepository _repo;

        public SkillsController(IRatingRepository repo)
        {
           
            _repo = repo;
        } 
      

        // GET api/values/5
        [HttpGet("{id}")]
        public Skill Get(int id)
        {
            return _repo.Find(id);
            
        }

        //[Route("api/search/{value}")]

        [HttpPost]
        public async Task <JsonResult> Search([FromBody]SearchModel model)
        {
            var result = await _repo.Find(model.SearchTerm);

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
                var userSkills = _repo.GetUserSkills(userId);
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
            if (_repo.HasUserSkill(userId, skillId))
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
                _repo.AddSkill(skill);
                skillId = skill.SkillId;
            }

            else
            {
                skillId = int.Parse(model.SkillId);
                if (skillId != 0)
                {
                    var skill = _repo.Find(skillId);
                    skillId = skill.SkillId;
                }
            }
            
    
            var userSkill = new UserSkill
            {
                UserId= userId,
                SkillId = skillId,
                Added = DateTime.Now
            };

            _repo.AddUserSkill(userSkill);

            return Ok();
            
        }

        [HttpDelete]
        public IActionResult UserSkills([FromBody] UserSkillDeleteModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int skillId = int.Parse(model.SkillId);

            var userSkill = _repo.GetUserSkill(userId, skillId);

            if (userSkill != null)
            {
                _repo.DeleteUserSkill(userSkill);
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

                int skillId = int.Parse(model.SkillId);
                int level = int.Parse(model.Level);

                if (level < 1)
                    return BadRequest();

                var userSkill = _repo.GetUserSkill(userId, skillId);

                if (userSkill == null) {
                    return NotFound();
                        }
                else {
                    userSkill.Level = level;
                    userSkill.Updated = DateTime.Now;

                     _repo.UpdateUserSkill(userSkill);
                }
                return Ok();
            }

            else return BadRequest("Missing arguments for SkillId or level");
        }
     
    }
}
