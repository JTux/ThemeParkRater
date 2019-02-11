using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemeParkRater.Models.ThemeParkModels
{
    public class ThemeParkCreate
    {
        [Required]
        public string ThemeParkName { get; set; }

        [Required]
        public string ThemeParkCity { get; set; }

        [Required]
        public string ThemeParkState { get; set; }
    }
}
