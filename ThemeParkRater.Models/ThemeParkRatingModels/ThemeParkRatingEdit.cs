using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemeParkRater.Models.ThemeParkRatingModels
{
    public class ThemeParkRatingEdit
    {
        [Required]
        public int ThemeParkRatingID { get; set; }

        [Required]
        public float GoodnessLevel { get; set; }

        [Required]
        public int ThemeParkID { get; set; }
    }
}
