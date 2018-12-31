using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp1.Models
{
    public class Funds
    {
        // spellchecker: disable
        [Key]
        public int FundID { get; set; }
        
        // user ID from User table.
        [Display(Name = "Thủ quỹ")]
        public string OwnerID { get; set; }

        // another user ID from User table
        [Required, Display(Name = "Người thực hiện")]
        public string TransactionUserID { get; set; }

        [Display(Name = "Số tiền")]
        [DataType(DataType.Currency),Range(1, Int32.MaxValue)]
        [DisplayFormat(DataFormatString = "{0:N0} vnđ")]
        public int Currency { get; set; }

        [Required, Display(Name = "Loại phí")]
        public bool isCollect { get; set; }

        [Display(Name = "Quỹ hiện tại")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0} vnđ")]
        public int TotalFund { get; set; }

        [Display(Name = "Ngày thực hiện")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? UpatedDate { get; set; }

        [Display(Name = "Mô tả/Lý do")]
        public string Description { get; set; }
    }
}
