using System;
using System.ComponentModel.DataAnnotations;


namespace WebApp1.Models
{
    public class TotalFunds
    {
        // spellchecker: disable
        [Key]
        public int TotalID { get; set; }

        [Display(Name = "Tổng Ngân Quỹ")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0} vnđ")]
        public int TotalFundsValue { get; set; }
    }
}
