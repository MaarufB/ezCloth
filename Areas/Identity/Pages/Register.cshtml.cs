using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class RegisterModel : PageModel
    {

        private readonly UserManager<SystemUsers> _userManager;
        private readonly SignInManager<SystemUsers> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        
        public RegisterModel(DatabaseContext context,
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
        public RegisterDto Input { get; set; }

        public class InputModel
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            [Required]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            public string UserRole { get; set; }
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<SystemUsers>(Input);
                user.UserName = Input.Email;

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    if (Input.UserRole.ToLower() == "--select--")
                    {
                        await _userManager.AddToRoleAsync(user, "INDIVIDUAL");
                    }
                    else await _userManager.AddToRoleAsync(user, Input.UserRole.ToUpper());

                    await _signInManager.SignInAsync(user, false);
                    return await Task.Run(() => RedirectToAction("Index", "Home"));
                }
                //else return BadRequest();
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }


            return await Task.Run(() => Page());
        }



    }
}
