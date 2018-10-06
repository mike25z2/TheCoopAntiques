using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheCoopAntiques.Models
{
    public class Periods
    {
        public int Id { get; set; }
        
        public string PeriodType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public virtual string PeriodName => YearInt + MonthInt.ToString("00");

        public int MonthInt { get; set; }

        public int YearInt { get; set; }

    }
}
