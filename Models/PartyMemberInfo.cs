using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WebApp1.Models
{
    public class PartyMemberInfo
    {
        [Key]
        public string Id { get; set; }
        [PersonalData, Display(Name = "Số di động")]
        public string PhoneNumber { get; set; }
        [PersonalData ,Display(Name = "Email")]
        public string Email { get; set; }
        [PersonalData]
        public string BirthName { get; set; }
        [PersonalData, Display(Name = "Họ tên")]
        public DateTime BirthDay { get; set; }
        [PersonalData, Display(Name = "Ngày sinh")]
        public string Gender { get; set; }
        // Dân tộc
        [PersonalData, Display(Name = "Giới tính")]
        public string Ethnicity { get; set; }
        // Tôn giáo
        [PersonalData, Display(Name = "Tôn giáo")]
        public string Religion { get; set; }
        // Quê quán
        [PersonalData, Display(Name = "Quê quán")]
        public string Country { get; set; }
        [PersonalData, Display(Name = "Trình độ học vấn")]
        // Trình độ học vấn
        public string EduLevel { get; set; }
        [PersonalData, Display(Name = "Lịch sử công việc (trước khi vào đảng, hiện tại)")]
        // Công việc trước khi vào Đảng, Công việc hiện tại
        public string JobInfo { get; set; }
        [PersonalData, Display(Name = "Ngày vào Đảng chính thức")]
        // Ngày vào Đảng, ngày chính thức
        public string PartyDateInfo { get; set; }
        [PersonalData, Display(Name = "Số thẻ Đảng viên")]
        // Số thẻ đảng viên, số LL đảng viên
        public string PartyCardNum { get; set; }
        [PersonalData, Display(Name = "Bộ đội / Công an / Hưu trí")]
        // Bộ đội, công an, hưu trí
        public string MilitaryPoliceRetire { get; set; }
        [PersonalData, Display(Name = "Ngày rời đi, đến đảng bộ, cơ sở đảng")]
        // Ngày rời đi, đến đảng bộ, cơ sở đảng
        public string OutGoingInfo { get; set; }
        // Ngày gia nhập chi bộ Đảng, từ cơ sở Đảng nào
        [PersonalData, Display(Name = "Ngày gia nhập chi bộ Đảng, từ cơ sở Đảng nào")]
        public string InComingInfo { get; set; }
        // Ngày từ trần, lý do
        [PersonalData, Display(Name = "Ngày từ trần, lý do")]
        public string DiedInfo { get; set; }
        // Ngày ra khỏi đảng, hình thức
        [PersonalData, Display(Name = "Ngày ra khỏi đảng, hình thức")]
        public string LeaveInfo { get; set; }
        //Ghi chú
        [PersonalData, Display(Name = "Ghi chú")]
        public string Comment { get; set; }
        [PersonalData, Display(Name = "Ảnh đại diện")]
        public string Avatar { get; set; }

    }
}
