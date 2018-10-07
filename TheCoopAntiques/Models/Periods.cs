using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheCoopAntiques.Models
{
    public class Periods
    {
        public int Id { get; set; }

        [Display(Name = "Type")]
        public string PeriodType { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        
        [Display(Name="Period Name")]
        public virtual string PeriodName => YearInt + MonthInt.ToString("00");

        [Display(Name="Month")]
        public int MonthInt { get; set; }
        [Display(Name = "Year")]
        public int YearInt { get; set; }

    }
}
