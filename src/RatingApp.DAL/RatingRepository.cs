using RatingApp.DAL;

namespace RatingApp.DAL
{
    using Domain;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;

    public interface IRatingRepository
    {

        Skill Find(int key);
        Task<List<Skill>> Find(string searchTerm);
        List<UserSkill> GetUserSkills(string userId);
        UserSkill GetUserSkill(string userId, int skillId);
        void DeleteUserSkill(UserSkill userSkill);
        void UpdateUserSkill(UserSkill userSkill);
        bool HasUserSkill(string userId, int skillId);
        void AddSkill(Skill skill);
        void AddUserSkill(UserSkill userSkill);
    }

    public class RatingRepository : IRatingRepository
    {

        private RatingDBContext _context;

        public RatingRepository(RatingDBContext context)
        {
            _context = context;

        }

        public void AddSkill(Skill skill)
        {
            _context.Skills.Add(skill);
            _context.SaveChanges();

        }

        public Skill Find(int key)
        {
            return _context.Skills.FirstOrDefault(e => e.SkillId == key);
        }

        public async Task<List<Skill>> Find(string searchTerm)
        {
            return await _context.Skills.Where(data => data.SkillName.StartsWith(searchTerm.Trim())).ToListAsync();

        }

        public bool HasUserSkill (string userId, int skillId)
        {
            return _context.UserSkills.Any(p => p.SkillId == skillId && p.UserId == userId);

        }
     
        public List<UserSkill> GetUserSkills (string userId) {

            return _context.UserSkills.Include(p => p.Skill).OrderBy(p => p.Skill.SkillName).Where(u => u.UserId == userId).ToList();
        }

       public void AddUserSkill (UserSkill userSkill)
        {
            _context.UserSkills.Add(userSkill);
            _context.SaveChanges();
        }

        public UserSkill GetUserSkill (string userId, int skillId)
        {   
            return _context.UserSkills.FirstOrDefault(p => p.SkillId == skillId && p.UserId == userId);

        }

        public void UpdateUserSkill (UserSkill userSkill)
        {

            _context.UserSkills.Attach(userSkill);
            _context.Entry(userSkill).State = EntityState.Modified;
            _context.SaveChanges();

        }

        public void DeleteUserSkill (UserSkill userSkill)
        {
            _context.UserSkills.Remove(userSkill);
            _context.SaveChanges();
        }

      
    }
}

