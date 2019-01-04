using WebApp1.Authorization;
using WebApp1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApp1.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // For sample purposes we are seeding 2 users both with the same password.
                // The password is set with the following command:
                // dotnet user-secrets set SeedUserPW <pw>
                // The admin user can do anything

                var adminID = await EnsureUser(serviceProvider, testUserPw, "Bí thư", "admin@cntt.epu.com");
                await EnsureRole(serviceProvider, adminID, Constants.MeetingAdministratorsRole);

                // allowed user can create and edit Meetings that they create
                var uid = await EnsureUser(serviceProvider, testUserPw, "Thủ quỹ", "manager@cntt.epu.com");
                await EnsureRole(serviceProvider, uid, Constants.MeetingManagersRole);

                SeedDB(context, adminID);
            }
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                                    string testUserPw, string userName, string email = "")
        {
            var userManager = serviceProvider.GetService<UserManager<PartyMember>>();

            var user = await userManager.FindByNameAsync(email);
            if (user == null)
            {
                user = new PartyMember() { UserName = email };
                user.Email = email;
                await userManager.CreateAsync(user, testUserPw);
                var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                await userManager.ConfirmEmailAsync(user, code);
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      string uid, string role)
        {
            IdentityResult IR = null;
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<PartyMember>>();

            var user = await userManager.FindByIdAsync(uid);

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }
        public static void SeedDB(ApplicationDbContext context, string adminID)
        {
            if (!context.Meeting.Any())
            {
                context.Meeting.AddRange(
                    // spellchecker: disable
                    new Meeting
                    {
                        MeetingTitle = "Cuộc họp số 1",
                        OwnerID = adminID,
                        MeetingDate = DateTime.Parse("2015-12-17"),
                        Summary = "Cuộc họp công nghệ lần thứ nhất"
                    },
                    new Meeting
                    {
                        MeetingTitle = "Cuộc họp số 2",
                        OwnerID = adminID,
                        MeetingDate = DateTime.Parse("2015-12-27"),
                        Summary = "Quản lý Đảng viên"
                    },
                    new Meeting
                    {
                        MeetingTitle = "Cuộc họp số 3",
                        OwnerID = adminID,
                        MeetingDate = DateTime.Parse("2015-11-17"),
                        Summary = "Kế hoạch phát triển khoa Công nghệ thông tin"
                    },
                    new Meeting
                    {
                        MeetingTitle = "Cuộc họp số 4",
                        OwnerID = adminID,
                        MeetingDate = DateTime.Parse("2016-12-27"),
                        Summary = "Chào mừng ngày nhà giáo Việt Nam"
                    },
                    new Meeting
                    {
                        MeetingTitle = "Cuộc họp số 5",
                        OwnerID = adminID,
                        MeetingDate = DateTime.Parse("2016-02-17"),
                        Summary = "Lập kế hoạch phát triển Đảng viên"
                    },
                    new Meeting
                    {
                        MeetingTitle = "Cuộc họp số 6",
                        OwnerID = adminID,
                        MeetingDate = DateTime.Parse("2016-04-15"),
                        Summary = "Cuộc cách mạng 4.0"
                    },
                    new Meeting
                    {
                        MeetingTitle = "Cuộc họp số 7",
                        OwnerID = adminID,
                        MeetingDate = DateTime.Parse("2016-05-22"),
                        Summary = "Tái đổi mới cơ cấu khoa Công nghệ thông tin"
                    },
                    new Meeting
                    {
                        MeetingTitle = "Cuộc họp số 8",
                        OwnerID = adminID,
                        MeetingDate = DateTime.Parse("2016-08-08"),
                        Summary = "Ngành học máy và trí tuệ nhân tạo"
                    },
                    new Meeting
                    {
                        MeetingTitle = "Cuộc họp số 9",
                        OwnerID = adminID,
                        MeetingDate = DateTime.Parse("2017-01-17"),
                        Summary = "Cuộc họp công nghệ lần thứ hai"
                    },
                    new Meeting
                    {
                        MeetingTitle = "Cuộc họp số 10",
                        OwnerID = adminID,
                        MeetingDate = DateTime.Parse("2017-06-10"),
                        Summary = "Liên hoan văn nghệ trường Đại học Điện Lức"
                    },
                    new Meeting
                    {
                        MeetingTitle = "Cuộc họp số 11",
                        OwnerID = adminID,
                        MeetingDate = DateTime.Parse("2018-11-29"),
                        Summary = "Cuộc thi tiếng hát giáo viên"
                    },
                    new Meeting
                    {
                        MeetingTitle = "Cuộc họp số 12",
                        OwnerID = adminID,
                        MeetingDate = DateTime.Parse("2018-10-30"),
                        Summary = "Nghị quyết trung ương Đảng lần thứ 4"
                    }
                 );
            }

            if (!context.TotalFunds.Any())
            {
                context.TotalFunds.AddRange(
                    new TotalFunds
                    {
                        TotalFundsValue = 5000000
                    }
                );

            }

            context.SaveChanges();
        }
    }
}
