using ezCloth.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using ezCloth.Data;
using ezCloth.DTOs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AutoMapper;

namespace ezCloth.Controllers
{
    public class UserAccountController : Controller
    {
        private readonly UserManager<SystemUsers> _userManager;
        private readonly SignInManager<SystemUsers> _signInManager;
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public UserAccountController(DatabaseContext context, 
                                     UserManager<SystemUsers> userManager,
                                     SignInManager<SystemUsers> signInManager,
                                     IMapper mapper)

        {
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if(ModelState.IsValid)
            {
                var user = _mapper.Map<SystemUsers>(registerDto);
                user.UserName = registerDto.Email;

                var result = await _userManager.CreateAsync(user, registerDto.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return await Task.Run(() => RedirectToAction("Index","Home"));
                }
                //else return BadRequest();
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return await Task.Run(() => View());
        }

        public async Task<IActionResult> Login()
        {
            return await Task.Run(() => View());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginUser)
        {
            var user = _mapper.Map<LoginDto>(loginUser);
            if(ModelState.IsValid)
            {
                var identityResult = await _signInManager.PasswordSignInAsync(user.Email, loginUser.Password, false, false);
                if (identityResult.Succeeded)
                {
                    return await Task.Run(() => RedirectToAction("Index", "Home"));
                }
                else return await Task.Run(() => BadRequest());

            }

            return await Task.Run(() => RedirectToAction("Login", "UserAccount"));
        }

        public IActionResult Logout(string emptyr=null)
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return await Task.Run(() => RedirectToAction("Login", "UserAccount"));
        }
        
        [HttpPost]
        public async Task<IActionResult> DontLogout()
        {

            return await Task.Run(() => RedirectToAction("Index", "Home"));
        }


    }
}
