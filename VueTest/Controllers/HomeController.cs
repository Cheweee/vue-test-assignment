using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VueTest.Data.Models;
using VueTest.Models;
using VueTest.Services;

namespace VueTest.Controllers
{
    public class HomeController : Controller
    {
        private UserService _userService;

        public HomeController(UserService userService)
        {
            _userService = userService ?? throw new ArgumentException(nameof(userService));
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody]User model)
        {
            return Ok(await _userService.Create(model));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
