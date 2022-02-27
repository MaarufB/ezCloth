using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ezCloth.Data;
using ezCloth.DTOs;
using ezCloth.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace ezCloth.Areas.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<SystemUsers> _signInManager;
        public LogoutModel(SignInManager<SystemUsers> signInManager)
        {
            _signInManager = signInManager;
        }   

        public async Task<IActionResult> OnPost()
        {
            await _signInManager.SignOutAsync();

            return await Task.Run(() => RedirectToPage("Login"));
        }
    }
}
