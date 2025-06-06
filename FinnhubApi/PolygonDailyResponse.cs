﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Investment_App.FinnhubApi
{
    public class PolygonDailyResponse
    {
        public string status { get; set; }
        public string from { get; set; }
        public string symbol { get; set; }
        public decimal open { get; set; }
        public decimal high { get; set; }
        public decimal low { get; set; }
        public decimal close { get; set; }
        public decimal volume { get; set; }
        public decimal afterHours { get; set; }
        public decimal preMarket { get; set; }
    }
}
