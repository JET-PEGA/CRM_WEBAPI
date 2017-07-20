using CRM_WEBAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace CRM_WEBAPI.Controllers
{
    public class Test04Controller : ApiController
    {
        /// <summary>
        /// 上傳檔案 ()
        /// </summary>
        /// <returns></returns>
        [Route("Test04/PostJson")]
        public UserInfo PostJson(UserInfo userInfo)
        {
            userInfo.Name = "Jet";
            return userInfo;
        }

        /// <summary>
        /// 上傳檔案 ()
        /// </summary>
        /// <returns></returns>
        [Route("Test04/PostFiles")]
        public string PostFiles()
        {
            HttpContext httpContext = HttpContext.Current;              // 取得目前的 HttpContext
            string fileName = string.Empty;

            foreach (string file in httpContext.Request.Files)
            {
                HttpPostedFile postedFile = httpContext.Request.Files[file];
                fileName = Path.GetFileName(postedFile.FileName);
                SaveFile(postedFile.InputStream, @"d:\Webapi_", postedFile.FileName);
            }

            return fileName;
        }


        /// <summary>
        /// 儲存檔案
        /// </summary>
        /// <param name="stream">資料串流</param>
        /// <param name="filePath">檔案路徑</param>
        /// <param name="fileName">檔案名稱</param>
        /// <returns></returns>
        private bool SaveFile(Stream stream, string filePath, string fileName)
        {
            bool ret = false;
            string fullPath = Path.Combine(filePath, fileName);

            if (File.Exists(fullPath)) // 若檔案存在，刪除檔案
                File.Delete(fullPath);

            if (!Directory.Exists(filePath)) // 若目錄不存在，建立目錄
                Directory.CreateDirectory(filePath);

            using (FileStream fs = new FileStream(fullPath, FileMode.Create))
            {
                stream.CopyTo(fs);
                fs.Close();
                stream.Seek(0, SeekOrigin.Begin);
                stream.Position = 0;
                ret = true;
            }

            return ret;
        }
    }
}
