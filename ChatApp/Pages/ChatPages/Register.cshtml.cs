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
   
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public User User { get; set; }

        public IActionResult OnPost()
        {
            User = UserDataProvider.RegisterUser(User);
            return RedirectToPage("./User", new { userId = User.Id });
        }
    }
}