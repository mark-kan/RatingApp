using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        public string SkillId { get; set; }

        [Required]
        public string SkillName { get; set; }
        }

        public class UserSkillLevelModel
        {

        [Required]
        public string SkillId { get; set; }

        [Required]
        public string Level { get; set; }
        }

        public class UserSkillDeleteModel
        {

        [Required]
        public string SkillId { get; set; }

        }

    public class SkillResultModel
    {
        public int SkillId { get; set; }
        public string SkillName { get; set; }
        public string DisplayName { get; set; }
    }
    }

