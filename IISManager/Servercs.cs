using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IISManager
{
    public class Servercs
    {
        /// <summary>
        /// 服务器IIS版本
        /// </summary>
        public string ServiceIIS
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["SERVER_SOFTWARE"];
            }
        }
    }
}