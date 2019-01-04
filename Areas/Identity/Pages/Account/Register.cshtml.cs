using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WebApp1.Data;

namespace WebApp1.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<PartyMember> _signInManager;
        private readonly UserManager<PartyMember> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<PartyMember> userManager,
            SignInManager<PartyMember> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            #region Thông tin Đảng viên
            // [Required]
            // [DataType(DataType.Text)]
            // [Display(Name = "Tên khai sinh")]
            // public string BirthName { get; set; }

            // [Required]
            // [Display(Name = "Ngày sinh")]
            // [DataType(DataType.Date)]
            // public DateTime BirthDay { get; set; }

            // [Required]
            // [DataType(DataType.Text)]
            // [Display(Name = "Giới tính")]
            // public string Gender { get; set; }
            // [Required]
            // [DataType(DataType.Text)]
            // [Display(Name = "Dân tộc")]
            // public string Ethnicity { get; set; }
            // [Required]
            // [DataType(DataType.Text)]
            // [Display(Name = "Tôn giáo")]
            // public string Religion { get; set; }
            // [Required]
            // [DataType(DataType.Text)]
            // [Display(Name = "Quê quán")]
            // public string Country { get; set; }
            // [Required]
            // [DataType(DataType.Text)]
            // [Display(Name = "Trình độ học vấn")]
            // public string EduLevel { get; set; }
            // [DataType(DataType.Text)]
            // [Display(Name = "Thông tin nghề nghiệp")]
            // public string JobInfo { get; set; }
            // [Required]
            // [DataType(DataType.Text)]
            // [Display(Name = "Ngày vào Đảng, ngày chính thức")]
            // public string PartyDateInfo { get; set; }
            // [Required]
            // [DataType(DataType.Text)]
            // [Display(Name = "Số thẻ, số LL Đảng viên")]
            // public string PartyCardNum { get; set; }
            // [DataType(DataType.Text)]
            // [Display(Name = "Bộ đội, công an, hưu trí")]
            // public string MilitaryPoliceRetire { get; set; }
            // [DataType(DataType.Text)]
            // [Display(Name = "Ngày chuyển đi, đến Đảng bộ cơ sở")]
            // public string OutGoingInfo { get; set; }
            // [DataType(DataType.Text)]
            // [Display(Name = "Ngày chuyển đến, từ Đảng bộ cơ sở")]
            // public string InComingInfo { get; set; }
            // [DataType(DataType.Text)]
            // [Display(Name = "Ngày từ trần, lý do")]
            // public string DiedInfo { get; set; }
            // [DataType(DataType.Text)]
            // [Display(Name = "Ngày ra rời Đảng, hình thức ra khỏi Đảng")]
            // public string LeaveInfo { get; set; }
            // [DataType(DataType.Text)]
            // [Display(Name = "Ghi chú")]
            // public string Comment { get; set; }
            #endregion

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new PartyMember {
                    UserName = Input.Email,
                    Email = Input.Email,
                    #region Thông tin Đảng viên
                    // BirthName = Input.BirthName,
                    // BirthDay = Input.BirthDay,
                    // Gender = Input.Gender,
                    // Ethnicity = Input.Ethnicity,
                    // Religion = Input.Religion,
                    // Country = Input.Country,
                    // EduLevel = Input.EduLevel,
                    // JobInfo = Input.JobInfo,
                    // PartyDateInfo = Input.PartyDateInfo,
                    // PartyCardNum = Input.PartyCardNum,
                    // MilitaryPoliceRetire = Input.MilitaryPoliceRetire,
                    // OutGoingInfo = Input.OutGoingInfo,
                    // InComingInfo = Input.InComingInfo,
                    // DiedInfo = Input.DiedInfo,
                    // LeaveInfo = Input.LeaveInfo,
                    // Comment = Input.Comment
                    #endregion

                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Xác thực email của bạn",
                        $"Xác thực email của bạn, hãy <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>ấn vào đây</a>.");

                    // await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
