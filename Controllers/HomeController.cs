using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web.Interfaces;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IReq reqData;
        private IHostingEnvironment Environment;
        public HomeController(ILogger<HomeController> logger, IReq ReqData, IHostingEnvironment _environment)
        {
            _logger = logger;
            reqData = ReqData;
            this.Environment = _environment;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Check()
        {
            await reqData.Check(HttpContext.Session.GetString("token"));
            return Redirect("~/Home/Dashboard");
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            string token = HttpContext.Session.GetString("token");
            
            if (token != null)
            {
                var checkAuth = reqData.Check(token).Result;
                if (checkAuth != null)
                {
                    List<Req> reqs = (reqData.GetReq(token)).Result.ToList();
                    ViewBag.list = reqs;
                    return View();
                }
                else
                {
                    HttpContext.Session.Remove("token");
                    return Redirect("~/Home/Login");
                }
            }
            else
                return Redirect("~/Home/Login");
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new UserLogin()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLogin model)
        {
            if (ModelState.IsValid)
            {
                string token = await reqData.Login(model.LoginProp, model.Password);
                if (token != null)
                {
                    HttpContext.Session.SetString("token", token);

                    if (Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    return RedirectToAction("Dashboard", "Home");
                }

                ModelState.AddModelError("", "Пользователь не найден");
                return View(model);
            }

            ModelState.AddModelError("", "Некорректные данные");
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("token");
            return Redirect("~/Home/Index");
        }

        [HttpGet]
        public IActionResult Main()
        {
            string token = HttpContext.Session.GetString("token");

            if (token != null)
            {
                var checkAuth = reqData.Check(token).Result;
                if (checkAuth != null)
                {
                    return View();
                }
                else
                {
                    HttpContext.Session.Remove("token");
                    return Redirect("~/Home/Login");
                }
            }
            else
                return Redirect("~/Home/Login");
        }

        [HttpGet]
        public IActionResult Projects()
        {
            string token = HttpContext.Session.GetString("token");

            if (token != null)
            {
                var checkAuth = reqData.Check(token).Result;
                if (checkAuth != null)
                {
                    var allItems = reqData.GetHtml().Result;
                    var list = allItems.Where(x => x.Type == "project");
                    ViewBag.Projects = list;
                    return View();
                }
                else
                {
                    HttpContext.Session.Remove("token");
                    return Redirect("~/Home/Login");
                }
            }
            else
                return Redirect("~/Home/Login");
        }

        [HttpGet]
        public IActionResult Services()
        {
            string token = HttpContext.Session.GetString("token");

            if (token != null)
            {
                var checkAuth = reqData.Check(token).Result;
                if (checkAuth != null)
                {
                    var allItems = reqData.GetHtml().Result;
                    var list = allItems.Where(x => x.Type == "service");
                    ViewBag.Services = list;
                    return View();
                }
                else
                {
                    HttpContext.Session.Remove("token");
                    return Redirect("~/Home/Login");
                }
            }
            else
                return Redirect("~/Home/Login");
        }

        [HttpGet]
        public IActionResult Blog()
        {
            string token = HttpContext.Session.GetString("token");

            if (token != null)
            {
                var checkAuth = reqData.Check(token).Result;
                if (checkAuth != null)
                {
                    var allItems = reqData.GetHtml().Result;
                    var list = allItems.Where(x => x.Type == "blog");
                    ViewBag.Blogs = list;
                    return View();
                }
                else
                {
                    HttpContext.Session.Remove("token");
                    return Redirect("~/Home/Login");
                }
            }
            else
                return Redirect("~/Home/Login");
        }

        [HttpGet]
        public IActionResult Contacts()
        {
            string token = HttpContext.Session.GetString("token");

            if (token != null)
            {
                var checkAuth = reqData.Check(token).Result;
                if (checkAuth != null)
                {
                    var allItems = reqData.GetHtml().Result;
                    var list = allItems.Where(x => x.Type == "contact");
                    ViewBag.Contacts = list;
                    return View();
                }
                else
                {
                    HttpContext.Session.Remove("token");
                    return Redirect("~/Home/Login");
                }
            }
            else
                return Redirect("~/Home/Login");
        }

        [HttpGet]
        public IActionResult ProjectsGuest()
        {
            try
            {
                var allItems = reqData.GetHtml().Result;
                var list = allItems.Where(x => x.Type == "project");
                ViewBag.Projects = list;

            }catch{ }
            return View();
        }

        [HttpGet]
        public IActionResult ServicesGuest()
        {
            try
            {
                var allItems = reqData.GetHtml().Result;
                var list = allItems.Where(x => x.Type == "service");
                ViewBag.Services = list;
            }
            catch { }
            return View();
        }

        [HttpGet]
        public IActionResult BlogGuest()
        {
            try
            {
                var allItems = reqData.GetHtml().Result;
                var list = allItems.Where(x => x.Type == "blog");
                ViewBag.Blogs = list;
            }
            catch { }
            return View();
        }

        [HttpGet]
        public IActionResult ContactsGuest()
        {
            try
            {
                var allItems = reqData.GetHtml().Result;
                var list = allItems.Where(x => x.Type == "contact");
                ViewBag.Contacts = list;
            }
            catch { }
            return View();
        }

        [HttpGet]
        public IActionResult EditBlog(string Id, string type, string url)
        {
            if (type == null)
            {
                var allItems = reqData.GetHtml().Result;
                var item = allItems.FirstOrDefault(x => x.Id == Convert.ToInt32(Id));
                ViewBag.Item = item;
            }
            else
                ViewBag.Item = new HtmlTemplate() { Type = type };

            ViewBag.Url = url;
            return View();
        }
        
        [HttpPost]
        public IActionResult EditBlog(HtmlTemplate template)
        {
            var allItems = reqData.GetHtml().Result;
            var item = allItems.FirstOrDefault(x => x.Id == template.Id);

            if (template.Img == null && item.Img != null)
                template.Img = item.Img;

            item = template;
            return Redirect("~/Home/Blog");
        }
        
        [HttpPost]
        public IActionResult SaveCategories(string[] categories)
        {
            System.IO.File.WriteAllLines($"{Environment.WebRootPath}/Categories.txt", categories);
            return Redirect("~/Home/Main");
        }
        
        [HttpGet]
        public IActionResult MoreInfo(int id)
        {
            var allItems = reqData.GetHtml().Result;
            var item = allItems.FirstOrDefault(x => x.Id == id);
            ViewBag.Item = item;
            return View();
        }
        
        [HttpGet]
        public IActionResult Added()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }
    }
}
