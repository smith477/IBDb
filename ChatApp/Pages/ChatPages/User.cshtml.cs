using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DataProvider;
using Application.Domain.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChatApp.Pages.ChatPages
{
    public class UserModel : PageModel
    {
        public User User { get; set; }

        public IActionResult OnGet(string userId)
        {
            User = UserDataProvider.GetUser(userId);
            if(User == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }
    }
}