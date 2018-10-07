using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TheCoopAntiques.Models
{
    public class Orders
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name="Order ID")]
        public string InvoiceNumber { get; set; }

        [Required]
        [Display(Name = "Order Date")]
        [DisplayFormat(DataFormatString= "{0:MM/dd/yyyy}")]
        public DateTime OrderDate { get; set; }

        [Required]
        [Display(Name="Transaction Type")]
        public int TransactionTypeId { get; set; }

        [ForeignKey("TransactionTypeId")]
        [Display(Name="Type")]
        public virtual TransactionTypes TransactionTypes { get; set; }

        [Required(AllowEmptyStrings = true)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name="On Account")]
        public int DealerId { get; set; }

        [ForeignKey("DealerId")]
        public virtual Dealers Dealers { get; set; }

        public decimal TaxRate { get; set; }
    }

}
