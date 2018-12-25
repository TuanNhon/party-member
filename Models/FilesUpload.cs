using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace WebApp1.Models
{
    public class FilesUpload
    {
        // spellchecker: disable
        [Key]
        public int FileID { get; set; }
        // user ID from Meeting table.
        public int MeetingID { get; set; }
        // user ID from User table.
        public string OwnerID { get; set; }
        [Display(Name = "Tên tài liệu")]
        public string FileName { get; set; }
        [Display(Name = "Ngày tạo")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? UploadDate { get; set; }
        [Display(Name = "Mô tả")]
        public string Description {get; set;}
        [Display(Name = "Đường dẫn")]
        public string FilePath {get; set;}
    }
}
