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

        public string OwnerID { get; set; }

        [Display(Name = "Số tiền")]
        public int Currency { get; set; }

        [Display(Name = "Trang thái")]
        public bool isCollect { get; set; }

        [Display(Name = "Quỹ hiện tại")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0} vnđ")]
        public int TotalFund { get; set; }

        [Display(Name = "Ngày thực thi")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? UpatedDate { get; set; }

        [Display(Name = "Mô tả/Lý do")]
        public string Description { get; set; }
    }
}
