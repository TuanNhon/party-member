using Microsoft.AspNetCore.Identity;
using System;

namespace WebApp1.Data
{
    public class PartyMember : IdentityUser
    {
        [PersonalData]
        public string BirthName { get; set; }
        [PersonalData]
        public DateTime BirthDay { get; set; }
        [PersonalData]
        public string Gender { get; set; }
        // Dân tộc
        [PersonalData]
        public string Ethnicity { get; set; }
        // Tôn giáo
        [PersonalData]
        public string Religion { get; set; }
        // Quê quán
        [PersonalData]
        public string Country { get; set; }
        [PersonalData]
        // Trình độ học vấn
        public string EduLevel { get; set; }
        [PersonalData]
        // Công việc trước khi vào Đảng, Công việc hiện tại
        public string JobInfo { get; set; }
        [PersonalData]
        // Ngày vào Đảng, ngày chính thức
        public string PartyDateInfo { get; set; }
        [PersonalData]
        // Số thẻ đảng viên, số LL đảng viên
        public string PartyCardNum { get; set; }
        [PersonalData]
        // Bộ đội, công an, hưu trí
        public string MilitaryPoliceRetire { get; set; }
        // Ngày rời đi, đến đảng bộ, cơ sở đảng
        [PersonalData]
        public string OutGoingInfo { get; set; }
        // Ngày gia nhập chi bộ Đảng, từ cơ sở Đảng nào
        [PersonalData]
        public string InComingInfo { get; set; }
        // Ngày từ trần, lý do
        [PersonalData]
        public string DiedInfo { get; set; }
        // Ngày ra khỏi đảng, hình thức
        [PersonalData]
        public string LeaveInfo { get; set; }
        //Ghi chú
        [PersonalData]
        public string Comment { get; set; }

    }
}
