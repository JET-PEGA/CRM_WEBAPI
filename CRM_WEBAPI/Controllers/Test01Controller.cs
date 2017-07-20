using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CRM_WEBAPI.Controllers
{
    /// <summary>
    /// 有參數的 Action 測試
    /// </summary>
    public class Test01Controller : ApiController
    {
        public string Get()
        {
            return $"Hello World! (Get)";
        }

        public string Post()
        {
            return $"Hello World! (Post)";
        }
    }
}
