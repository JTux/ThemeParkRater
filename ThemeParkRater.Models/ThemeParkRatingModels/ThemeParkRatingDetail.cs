using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemeParkRater.Models.ThemeParkRatingModels
{
    public class ThemeParkRatingDetail
    {
        public int ThemeParkRatingID { get; set; }

        public float GoodnessLevel { get; set; }

        public int ThemeParkID { get; set; }

        public string ThemeParkName { get; set; }
    }
}
