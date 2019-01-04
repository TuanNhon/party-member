using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp1.Data;

namespace WebApp1.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<PartyMember> _userManager;
        private readonly SignInManager<PartyMember> _signInManager;
        private readonly IEmailSender _emailSender;

        public IndexModel(
            UserManager<PartyMember> userManager,
            SignInManager<PartyMember> signInManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            #region Thông tin đảng viên
            [Required, DataType(DataType.Text), Display(Name = "Tên khai sinh")]
            public string BirthName { get; set; }

            [Display(Name = "Ngày sinh"), DataType(DataType.Date)]
            public DateTime? BirthDay { get; set; }

            [Required, DataType(DataType.Text), Display(Name = "Giới tính")]
            public string Gender { get; set; }
            [Required, DataType(DataType.Text), Display(Name = "Dân tộc")]
            public string Ethnicity { get; set; }
            [Required, DataType(DataType.Text), Display(Name = "Tôn giáo")]
            public string Religion { get; set; }
            [Required, DataType(DataType.Text), Display(Name = "Quê quán")]
            public string Country { get; set; }
            [Required, DataType(DataType.Text), Display(Name = "Trình độ học vấn")]
            public string EduLevel { get; set; }
            [DataType(DataType.Text), Display(Name = "Thông tin nghề nghiệp")]
            public string JobInfo { get; set; }
            [Required, DataType(DataType.Text), Display(Name = "Ngày vào Đảng, ngày chính thức")]
            public string PartyDateInfo { get; set; }
            [Required, DataType(DataType.Text), Display(Name = "Số thẻ, số LL Đảng viên")]
            public string PartyCardNum { get; set; }
            [DataType(DataType.Text), Display(Name = "Bộ đội, công an, hưu trí")]
            public string MilitaryPoliceRetire { get; set; }
            [DataType(DataType.Text), Display(Name = "Ngày chuyển đi, đến Đảng bộ cơ sở")]
            public string OutGoingInfo { get; set; }
            [DataType(DataType.Text), Display(Name = "Ngày chuyển đến, từ Đảng bộ cơ sở")]
            public string InComingInfo { get; set; }
            [DataType(DataType.Text), Display(Name = "Ngày từ trần, lý do")]
            public string DiedInfo { get; set; }
            [DataType(DataType.Text), Display(Name = "Ngày ra rời Đảng, hình thức ra khỏi Đảng")]
            public string LeaveInfo { get; set; }
            [DataType(DataType.Text), Display(Name = "Ghi chú")]
            public string Comment { get; set; }
            #endregion

            [Required, EmailAddress, Display(Name = "Email")]
            public string Email { get; set; }

            [Phone, Display(Name = "Số điện thoại")]
            public string PhoneNumber { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                #region Thông tin Đảng viên
                BirthName = user.BirthName,
                BirthDay = user.BirthDay,
                Gender = user.Gender,
                Ethnicity = user.Ethnicity,
                Religion = user.Religion,
                Country = user.Country,
                EduLevel = user.EduLevel,
                JobInfo = user.JobInfo,
                PartyDateInfo = user.PartyDateInfo,
                PartyCardNum = user.PartyCardNum,
                MilitaryPoliceRetire = user.MilitaryPoliceRetire,
                OutGoingInfo = user.OutGoingInfo,
                InComingInfo = user.InComingInfo,
                DiedInfo = user.DiedInfo,
                LeaveInfo = user.LeaveInfo,
                Comment = user.Comment,
                #endregion

                Email = email,
                PhoneNumber = phoneNumber
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            #region Thông tin Đảng Viên
            if(Input.BirthName != user.BirthName)
            {
                user.BirthName = Input.BirthName;
            }
            if (Input.BirthDay != user.BirthDay)
            {
                user.BirthDay = Input.BirthDay;
            }
            if (Input.Gender != user.Gender)
            {
                user.Gender = Input.Gender;
            }
            if(Input.Ethnicity != user.Ethnicity)
            {
                user.Ethnicity = Input.Ethnicity;
            }
            if(Input.Religion != user.Religion)
            {
                user.Religion = Input.Religion;
            }
            if(Input.Country != user.Country)
            {
                user.Country = Input.Country;
            }
            if(Input.EduLevel != user.EduLevel)
            {
                user.EduLevel = Input.EduLevel;
            }
            if(Input.JobInfo != user.JobInfo)
            {
                user.JobInfo = Input.JobInfo;
            }
            if(Input.PartyDateInfo != user.PartyDateInfo)
            {
                user.PartyDateInfo = Input.PartyDateInfo;
            }
            if(Input.PartyCardNum != user.PartyCardNum)
            {
                user.PartyCardNum = Input.PartyCardNum;
            }
            if(Input.MilitaryPoliceRetire != user.MilitaryPoliceRetire)
            {
                user.MilitaryPoliceRetire = Input.MilitaryPoliceRetire;
            }
            if(Input.OutGoingInfo != user.OutGoingInfo)
            {
                user.OutGoingInfo = Input.OutGoingInfo;
            }
            if(Input.InComingInfo != user.InComingInfo)
            {
                user.InComingInfo = Input.InComingInfo;
            }
            if(Input.DiedInfo != user.DiedInfo)
            {
                user.DiedInfo = Input.DiedInfo;
            }
            if(Input.LeaveInfo != user.LeaveInfo)
            {
                user.LeaveInfo = Input.LeaveInfo;
            }
            if(Input.Comment != user.Comment)
            {
                user.Comment = Input.Comment;
            }
            #endregion

            var email = await _userManager.GetEmailAsync(user);
            if (Input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                }
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }


            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToPage();
        }
    }
}
