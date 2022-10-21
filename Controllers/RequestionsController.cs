using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Web.Interfaces;
using Web.Models;

namespace Web.Controllers
{
    public class RequestionsController : Controller
    {
        private readonly IReq reqData;
        public RequestionsController(IReq ReqData)
        {
            this.reqData = ReqData;
        }

        [HttpPost]
        public async Task<IActionResult> AddReq(Req req)
        {
            var add = await reqData.AddReq(req);
            if (add == "ok")
                return Redirect("~/Home/Added");

            return Redirect("~/Home/Error");
        }

        [HttpPost]
        public IActionResult EditReq(Req req)
        {
            string token = HttpContext.Session.GetString("token");

            if (token != null)
            {
                var checkAuth = reqData.Check(token).Result;
                if (checkAuth != null)
                {
                    var edit = reqData.UpdateReq(req).Result;
                    return Redirect("~/Home/Dashboard");
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

        [HttpPost]
        public IActionResult RemoveReq(Req req)
        {
            string token = HttpContext.Session.GetString("token");

            if (token != null)
            {
                var checkAuth = reqData.Check(token).Result;
                if (checkAuth != null)
                {
                    var edit = reqData.RemoveReq(req).Result;
                    return Redirect("~/Home/Dashboard");
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
        public IActionResult RemoveHtml(string id, string url)
        {
            string token = HttpContext.Session.GetString("token");

            if (token != null)
            {
                var checkAuth = reqData.Check(token).Result;
                if (checkAuth != null)
                {
                    var listHtml = reqData.GetHtml().Result;
                    var item = listHtml.FirstOrDefault(x => x.Id.ToString() == id);
                    var edit = reqData.RemoveHtml(item).Result;
                    return Redirect(url);
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


        [HttpPost]
        public IActionResult UpdateHtml(HtmlTemplate htmlTemplate, string url)
        {
            string token = HttpContext.Session.GetString("token");

            if (token != null)
            {
                var checkAuth = reqData.Check(token).Result;
                if (checkAuth != null)
                {
                    var edit = reqData.UpdateHtml(htmlTemplate).Result;
                    return Redirect(url);
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

    }
}
