using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RatingApp.Domain
{
    public class UserSkill
    {
      
        [Key]
        [Column(Order=0)]
        public int SkillId { get; set; }

        [Key]
        [Column(Order = 1)]
        public string UserId { get; set; }

        [Range(0, 5)]
        public int? Level { get; set; }

        [ForeignKey("SkillId")]
        public virtual Skill Skill { get; set; }


        [Required]
        public DateTime Added { get; set; }

        public DateTime? Updated { get; set; }
 

    }
}
