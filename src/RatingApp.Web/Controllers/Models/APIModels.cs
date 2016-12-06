using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatingApp.Web.Controllers.Models
{   
        public class SearchModel
        {
            public string SearchTerm { get; set; }
        }

        public class SkillAddModel
        {
            public string SkillId { get; set; }
            public string SkillName { get; set; }
        }

        public class UserSkillLevelModel
        {
            public string SkillId { get; set; }
            public string Level { get; set; }
        }

        public class UserSkillDeleteModel
        {
            public string SkillId { get; set; }

        }
    }

