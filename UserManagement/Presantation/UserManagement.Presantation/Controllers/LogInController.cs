using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Abstractions.Token;
using UserManagement.Application.DTOs;
using UserManagement.Application.Repositories;
using UserManagement.Domain.Entities;

namespace UserManagement.Presantation.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    [Route("[controller]/LogIn")]
    public class LogInController : Controller
    {
        
        private readonly IEmployeeReadRepository _employeeReadRepository;
        readonly ITokenHandler _tokenHandler;
        public LogInController(IEmployeeReadRepository employeeReadRepository,
            ITokenHandler TokenHandler)
        {
            _employeeReadRepository = employeeReadRepository;
            _tokenHandler = TokenHandler;
        }
        [HttpGet]
        public IActionResult LogIn()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(IFormCollection collect)
        {
            string username = collect["Username"];
            string password = collect["Password"];
            Employee user = _employeeReadRepository.Table.Include(user=> user.User)
                .Where(employee => employee.User.Username.Equals(username))
                .FirstOrDefault();
            if(user == null)
            {
                return BadRequest("This user is not found");
            }
            if(!(user.User.Password.Equals(password) && user.User.Username.Equals(username)))
            {
                return BadRequest("This user is not found");
            }
            Token token = _tokenHandler.CreateAccessToken(15);

            Response.Cookies.Append("JwtAccessToken",token.AccessToken,new CookieOptions { HttpOnly=true});
            Response.Cookies.Append("JwtExpirationTime", token.ExpirationTime.ToString(), new CookieOptions { HttpOnly = true });
            Response.Cookies.Append("UserId", user.Id.ToString(), new CookieOptions { HttpOnly = true });


            return RedirectToAction("Index","Employee");
        }
    }
}
