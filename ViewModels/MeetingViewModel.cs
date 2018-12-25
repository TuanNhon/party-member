using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp1.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApp1.ViewModels
{
    public class MeetingViewModel
    {
        public MeetingViewModel()
        {
        }
        public MeetingViewModel(Meeting meeting, List<FilesUpload> files, string ownerMeetingName)
        {
            Meeting = meeting;
            Files = files;
            OwnerMeetingName = ownerMeetingName;
        }
        public Meeting Meeting { get; set; }
        public List<FilesUpload> Files { get; set; }

        [Display(Name = "Người tạo")]
        public string OwnerMeetingName { get; set; }
    }
}
