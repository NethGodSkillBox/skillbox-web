using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Web.Interfaces;
using Web.Models;

namespace Web.Data
{
    public class ReqDataApi : IReq
    {
        private HttpClient httpClient = new HttpClient(); 
        public async Task<string> AddReq(Req req)
        {
            string json = JsonConvert.SerializeObject(req);

            var PostAddFav = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, "http://localhost:36255/api/Home/addReq");
            PostAddFav.Content = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
            var PostAddFavSend = await httpClient.SendAsync(PostAddFav);
            var text = await PostAddFavSend.Content.ReadAsStringAsync();

            if (text.Contains("Заявка добавлена"))
            {
                return "ok";
            }
            return null;
        }
        public async Task<string> UpdateReq(Req req)
        {
            string json = JsonConvert.SerializeObject(req);

            var PostAddFav = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, "http://localhost:36255/api/Home/updateReq");
            PostAddFav.Content = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
            var PostAddFavSend = await httpClient.SendAsync(PostAddFav);
            var text = await PostAddFavSend.Content.ReadAsStringAsync();

            if (text.Contains("Заявка обновлена"))
            {
                return "ok";
            }
            return null;
        }
        public async Task<string> RemoveReq(Req req)
        {
            string json = JsonConvert.SerializeObject(req);

            var PostAddFav = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, "http://localhost:36255/api/Home/removeReq");
            PostAddFav.Content = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
            var PostAddFavSend = await httpClient.SendAsync(PostAddFav);
            var text = await PostAddFavSend.Content.ReadAsStringAsync();

            if (text.Contains("Заявка удалена"))
            {
                return "ok";
            }
            return null;
        }
        public async Task<string> UpdateHtml(HtmlTemplate htmlTemplate)
        {
            string json = JsonConvert.SerializeObject(htmlTemplate);

            var PostAddFav = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, "http://localhost:36255/api/Home/updateHtml");
            PostAddFav.Content = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
            var PostAddFavSend = await httpClient.SendAsync(PostAddFav);
            var text = await PostAddFavSend.Content.ReadAsStringAsync();

            if (text.Contains("Документ обновлен"))
            {
                return "ok";
            }
            return null;
        }
        public async Task<string> RemoveHtml(HtmlTemplate htmlTemplate)
        {
            string json = JsonConvert.SerializeObject(htmlTemplate);

            var PostAddFav = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, "http://localhost:36255/api/Home/removeHtml");
            PostAddFav.Content = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
            var PostAddFavSend = await httpClient.SendAsync(PostAddFav);
            var text = await PostAddFavSend.Content.ReadAsStringAsync();

            if (text.Contains("Документ удален"))
            {
                return "ok";
            }
            return null;
        }

        public async Task<IEnumerable<HtmlTemplate>> GetHtml()
        {
            var get = await httpClient.SendAsync(new HttpRequestMessage(System.Net.Http.HttpMethod.Get, "http://localhost:36255/api/Home/getHtml"));
            var text = await get.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<HtmlTemplate>>(text);
            return list;
        }

        public async Task<IEnumerable<Req>> GetReq(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var get = await httpClient.SendAsync(new HttpRequestMessage(System.Net.Http.HttpMethod.Get, "http://localhost:36255/api/Home/getReqs"));
            var text = await get.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<Req>>(text);
            
            return list;
        }

        public async Task<string> Login(string LoginProp, string Password)
        {
            var PostAddFav = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, "http://localhost:36255/token");
            PostAddFav.Content = new System.Net.Http.StringContent($"data=username={LoginProp},password={Password}", Encoding.UTF8, "application/x-www-form-urlencoded");
            var PostAddFavSend = await httpClient.SendAsync(PostAddFav);
            var text = await PostAddFavSend.Content.ReadAsStringAsync();
            if (!text.Contains("Invalid username or password"))
            {
                var getAuthInfo = JsonConvert.DeserializeObject<Testjson>(text);
                return getAuthInfo.access_token;
            }
            return null;
        }
        public async Task<string> Check(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var get = await httpClient.SendAsync(new HttpRequestMessage(System.Net.Http.HttpMethod.Get, "http://localhost:36255/api/Home/getlogin"));
            var text = await get.Content.ReadAsStringAsync();

            if (text.Contains("Ваш логин"))
            {
                return "ok";
            }
            return null;
        }

    }
}
