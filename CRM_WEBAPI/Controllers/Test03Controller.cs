using CRM_WEBAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Xml;

namespace CRM_WEBAPI.Controllers
{
    /// <summary>
    /// 各種資料格式下載上傳
    /// </summary>
    public class Test03Controller : ApiController
    {
        /// <summary>
        /// 取得自訂JSON
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Route("Test03/GetJson")]
        public HttpResponseMessage GetJson()
        {
            HttpResponseMessage responseMessage = default(HttpResponseMessage); // 回應訊息物件
            JObject content = JObject.FromObject(GetDataSet());
            responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            responseMessage.Content = new ObjectContent(typeof(JObject), content, new JsonMediaTypeFormatter());
            return responseMessage;
        }

        /// <summary>
        /// 取得自訂Xml
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Route("Test03/GetXml")]
        public HttpResponseMessage GetXml()
        {
            HttpResponseMessage responseMessage = default(HttpResponseMessage); // 回應訊息物件
            JObject content = JObject.FromObject(GetDataSet());
            responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            XmlDocument doc = JsonConvert.DeserializeXmlNode(content.ToString(), "root");
            XmlElement element = doc.DocumentElement;
            responseMessage.Content = new ObjectContent(typeof(XmlElement), element, new XmlMediaTypeFormatter());
            return responseMessage;
        }

        /// <summary>
        /// 下載一個PDF報表 (記得把範本放到 D:\DEMO_1.mrt)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Route("Test03/GetPdf")]
        public HttpResponseMessage GetPdf()
        {
            string fileName = @"d:/D3-03-SVG.pdf";
            HttpContext httpContext = HttpContext.Current;                      // 取得目前的 HttpContext
            HttpResponseMessage responseMessage = default(HttpResponseMessage); // 回應訊息物件
            byte[] content = File.ReadAllBytes(fileName);
            responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            responseMessage.Content = new ByteArrayContent(content);
            responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            string fileNme = string.Format("{0}.pdf", DateTime.Now.ToString("yyyyMMdd_HHmmss"));
            responseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = fileNme };
            return responseMessage;
        }

        /// <summary>
        /// 取得自訂物件
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Route("Test03/GetUserInfos")]
        public UserInfo[] GetUserInfos()
        {
            UserInfo[] userInfos =
            {
                new UserInfo() { Name = "Jet_Tseng", ID = "LA0900384", Password = "pega" },
                new UserInfo() { Name = "Eric_Wu", ID = "LA1000384", Password = "pega" },
                new UserInfo() { Name = "David_Su", ID = "LA1100384", Password = "pega" }
            };
            return userInfos;
        }

        /// <summary>
        /// 一參數取得自訂物件
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Route("Test03/GetUserInfo/{index}")]
        public UserInfo GetUserInfo(int index)
        {
            UserInfo[] userInfos =
            {
                new UserInfo() { Name = "Jet_Tseng", ID = "LA0900384", Password = "pega" },
                new UserInfo() { Name = "Eric_Wu", ID = "LA1000384", Password = "pega" },
                new UserInfo() { Name = "David_Su", ID = "LA1100384", Password = "pega" }
            };
            return userInfos[index];
        }

        ////=========================================================================================================================================

        /// <summary>
        /// 取得測試的 DataSet
        /// </summary>
        /// <returns></returns>
        private DataSet GetDataSet()
        {
            DataSet ds = new DataSet("DEMO");
            DataTable dt = new DataTable("Data");
            dt.Columns.Add("ID");
            dt.Columns.Add("NAME");
            dt.Rows.Add("LA01", "Jet");
            dt.Rows.Add("LA02", "Eric");
            dt.Rows.Add("LA03", "David");
            ds.Tables.Add(dt);
            return ds;
        }
    }
}
