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
    public class LoginModel : PageModel
    { 
        private readonly UserManager<SystemUsers> _userManager;
        private readonly SignInManager<SystemUsers> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public LoginModel(DatabaseContext context,
                                     UserManager<SystemUsers> userManager,
                                     SignInManager<SystemUsers> signInManager,
                                     RoleManager<IdentityRole> roleManager,
                                     IMapper mapper)

        {
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        public void OnGet()
        {
        }

        [BindProperty]
        public LoginDto Input { get; set; }
        public async Task<IActionResult> OnPost()
        {
            var user = _mapper.Map<LoginDto>(Input);
            if (ModelState.IsValid)
            {
                var identityResult = await _signInManager.PasswordSignInAsync(user.Email, Input.Password, false, false);
                if (identityResult.Succeeded)
                {
                    return await Task.Run(() => RedirectToPage("Index", "Home"));
                }
                else return await Task.Run(() => BadRequest());
            }

            return await Task.Run(() => Page());
        }
    }
}
