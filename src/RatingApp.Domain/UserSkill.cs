using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RatingApp.Domain
{
    public class UserSkill
    {
      
        [ForeignKey("Skill")]
        public int SkillId { get; set; }

        public virtual Skill Skill { get; set; }
        
        public string UserId { get; set; }

        [Range(0, 5)]
        public int? Level { get; set; }

        [Required]
        public DateTime Added { get; set; }

        public DateTime? Updated { get; set; }
 

    }
}
