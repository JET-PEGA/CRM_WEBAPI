using System.Collections.Specialized;
using System.Web;
using System.Web.Http;

namespace CRM_WEBAPI.Controllers
{
    /// <summary>
    /// 有參數的 Action 測試
    /// </summary>
    public class Test02Controller : ApiController
    {
        public string Get(string userName)
        {
            return $"Hello World! {userName} (Get)";
        }

        public string Post()
        {
            string userName = string.Empty;
            HttpContext httpContext = HttpContext.Current;              // 取得目前的 HttpContext
            NameValueCollection formData = httpContext.Request.Form;    // 取得 From 資料集合
            userName = formData["userName"];
            return $"Hello World! {userName} (Post)";
        }
    }
}
