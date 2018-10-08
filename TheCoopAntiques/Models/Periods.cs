using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TheCoopAntiques.Models
{
    public class Periods
    {
        public int Id { get; set; }

        [NotMapped]
        private readonly Dictionary<int, string> monthNames;

        public Periods()
        {
            monthNames = new Dictionary<int, string>
            {
                { 1, "JAN" },
                { 2, "FEB" },
                { 3, "MAR" },
                { 4, "APR" },
                { 5, "MAY" },
                { 6, "JUN" },
                { 7, "JUL" },
                { 8, "AUG" },
                { 9, "SEP" },
                { 10, "OCT" },
                { 11, "NOV" },
                { 12, "DEC" }
            };
        }

        [Display(Name = "Type")]
        public string PeriodType { get; set; }

        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime EndDate { get; set; }
        
        [Display(Name="Period Name")]
        public virtual string PeriodName => YearInt + MonthInt.ToString("00");

        [Display(Name="Month Number")]
        [DisplayFormat(DataFormatString = "{0:00}")]
        public int MonthInt { get; set; }

        [Display(Name = "Month")]
        [NotMapped]
        public string Month
        {
            get
            {
                if (MonthInt > 0 && MonthInt < 13)
                {
                    return monthNames[MonthInt];
                }

                return String.Empty;
            }
        }
        
        [Display(Name = "Year")]
        public int YearInt { get; set; }

        public bool IsCurrent { get; set; }

        public override string ToString()
        {
            return $"Current Accounting Period: {Month} {YearInt.ToString()}";
        }
    }
}
