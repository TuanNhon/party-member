using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace WebApp1.Models
{
    public class Meeting
    {
        // spellchecker: disable
        [Key]
        public int MeetingID { get; set; }
        // user ID from Meeting table.
        public string OwnerID { get; set; }
        [Display(Name = "Tiêu đề")]
        public string MeetingTitle { get; set; }
        [Display(Name = "Ngày họp")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? MeetingDate { get; set; }
        [Display(Name = "Nội dung chính")]
        public string Summary {get; set;}
    }
}
