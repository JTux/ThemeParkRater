﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemeParkRater.Models.ThemeParkModels
{
    public class ThemeParkDetail
    {
        public int ThemeParkID { get; set; }

        public string ThemeParkName { get; set; }

        public string ThemeParkCity { get; set; }

        public string ThemeParkState { get; set; }

        public float GoodnessLevel { get; set; }
    }
}
