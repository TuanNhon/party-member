using WebApp1.Models;
using WebApp1.Data;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System;

namespace WebApp1.ViewModels
{
    public class FileViewModel
    {
        public FileViewModel()
        {
            Guid id = Guid.NewGuid();
            FileUpload = new FilesUpload();
            BirthName = "";
            MeetingTitle = "";
            Extension = ".txt";
            FileNameWithoutExt = id.ToString();

            FileUpload.FileName = FileNameWithoutExt + Extension;
            FileUpload.FilePath = "\\Files\\" + FileUpload.FileName;
            FileUpload.UploadDate = DateTime.Now;
        }
        public FileViewModel(FilesUpload fileUpload, string ownerName, string meetingTitle)
        {
            FileUpload = fileUpload;
            BirthName = ownerName;
            MeetingTitle = meetingTitle;
        }
        public FilesUpload FileUpload { get; set; }

        [Display(Name = "Người đăng tải")]
        public string BirthName { get; set; }
        [Display(Name = "Cuộc họp")]
        public string MeetingTitle { get; set; }
        public string Extension { get; set; }
        [Display(Name ="Tên tài liệu")]
        public string FileNameWithoutExt { get; set; }
   
    }
}
