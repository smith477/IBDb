using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DataProvider;
using Application.Domain.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace ChatApp.Pages.ChatPages
{
    public class LoginModel : PageModel
    {
        private readonly IConfiguration config;

        [TempData]
        public string Message { get; set; }
        [BindProperty]
        public User User { get; set; }

        public LoginModel(IConfiguration config)
        {
            this.config = config;

        }

        public IActionResult OnPost()
        {

            User = UserDataProvider.LoginUser(User);
            if(User == null)
            {
                TempData["Message"] = "User doesn't exit";
                return RedirectToPage("./Login");
            }
            else
            {
                return RedirectToPage("./User", new { userId = User.Id});
            }
        }
    }
}