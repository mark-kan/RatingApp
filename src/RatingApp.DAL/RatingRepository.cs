using RatingApp.DAL;

namespace RatingApp.DAL
{
    using Domain;
    using System.Collections.Generic;
    using System;

    public interface IRatingRepository
    {
        void AddSkill(string term);
        IEnumerable<Skill> GetAll();
        Skill Find(string key);
        UserSkill Find(string userId, int skillId);
        Skill Remove(string key);
        void Update(string userSkill, int skillId);
    }

    public class RatingRepository : IRatingRepository
    {

        private RatingDBContext _context;

        public RatingRepository(RatingDBContext context)
        {
            _context = context;

        }

        public void AddSkill(string description)
        {


        }

        public Skill Find(string key)
        {
            throw new NotImplementedException();
        }

        public UserSkill Find(string userId, int skillId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Skill> GetAll()
        {
            throw new NotImplementedException();
        }

        public Skill Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Update(string userSkill, int skillId)
        {
            throw new NotImplementedException();
        }
    }
}

