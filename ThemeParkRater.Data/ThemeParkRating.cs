using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemeParkRater.Data
{
    public class ThemeParkRating
    {
        [Key]
        public int ThemeParkRatingID { get; set; }

        [Required]
        public float GoodnessLevel { get; set; }

        [Required]
        public int ThemeParkID { get; set; }

        public virtual ThemePark ThemePark { get; set; }
    }
}
