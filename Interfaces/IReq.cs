using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Interfaces
{
    public interface IReq
    {
        Task<IEnumerable<Req>> GetReq(string token);
        Task<IEnumerable<HtmlTemplate>> GetHtml();
        Task<string> AddReq(Req req);
        Task<string> RemoveReq(Req req);
        Task<string> RemoveHtml(HtmlTemplate htmlTemplate);
        Task<string> UpdateReq(Req req);
        Task<string> UpdateHtml(HtmlTemplate htmlTemplate);
        Task<string> Login(string LoginProp, string Password);
        Task<string> Check(string token);
    }
}
