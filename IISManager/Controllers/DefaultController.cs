using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Web.Administration;
using IISManager.Models;

namespace IISManager.Controllers
{
    public class DefaultController : Controller
    {
        //
        // GET: /Default/

        public ActionResult Index()
        {

            List<IISSiteEntity> list_site=new List<IISSiteEntity>();
            ServerManager iisManager = new ServerManager();
            foreach (var site in iisManager.Sites)//遍历网站
            {
                IISSiteEntity s = new IISSiteEntity();
                s.Id = site.Id;
                s.Name = site.Name;
                s.PhysicalPath = site.Applications["/"].VirtualDirectories["/"].PhysicalPath;
                s.Pool = site.Applications["/"].ApplicationPoolName;
                //s.State = site.State.ToString();
                s.List_url = new List<string>();
                try
                {
                    foreach (var tmp in site.Bindings)
                    {
                        string url = tmp.Protocol + "://";
                        if (tmp.EndPoint.Address.ToString() == "0.0.0.0")
                        {
                            url += tmp.Host + ":" + tmp.EndPoint.Port;
                        }
                        else
                        {
                            url += tmp.EndPoint.Address.ToString() + ":" + tmp.EndPoint.Port;
                        }
                        s.List_url.Add(url);
                    }
                }
                catch (Exception ex)
                { 
                    
                }
                 
                list_site.Add(s);
            }

            string ss = HttpContext.Request.ServerVariables["SERVER_SOFTWARE"];
            //foreach (WorkerProcess w3wp in iisManager.WorkerProcesses)
            //{
            //   ss+="W3WP ("+w3wp.ProcessId+")  ";
             
            //    foreach (Request request in w3wp.GetRequests(0))
            //    {
            //        ss += request.Url + "  " + request.ClientIPAddr + "  " + request.TimeElapsed + "  " + request.TimeInState;
                    
            //    }
            //}
            ViewBag.s = ss;
            return View(list_site);
        }

        /// <summary>
        /// 回收应用程序池
        /// </summary>
        /// <param name="PoolName"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Recycile(string PoolName)
        {
            try
            {
                ServerManager iisManager = new ServerManager();
                iisManager.ApplicationPools[PoolName].Recycle();
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
            return Content("1");
        }

        /// <summary>
        /// 停止或者启动网站
        /// </summary>
        /// <param name="SiteName"></param>
        /// <returns></returns>
        public ActionResult ChangeState(string SiteName)
        {
            try
            {
                ServerManager iisManager = new ServerManager();
                var site = iisManager.Sites[SiteName];
                if (site.State == ObjectState.Started)
                {
                    site.Stop();
                }
                else if (site.State == ObjectState.Stopped)
                {
                    site.Start();
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

            return Content("1");

        }
         

        public ActionResult AddSite()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddSite(string siteName,string port,string path)
        {
            //string SiteName = "测试网站4"; //站点名称
            //string BindArgs = "*:89:"; //绑定参数，注意格式
            //string apl = "http"; //类型
            //string path = "d:\\发布"; //网站路径
            ServerManager sm = new ServerManager();
            sm.Sites.Add(siteName, "http", port, path);
            sm.CommitChanges();
          
            return View();
        }

      
        [HttpPost]
        public ActionResult DelSite(string siteName)
        {
            try
            {
                ServerManager iisManager = new ServerManager();
                Site site = iisManager.Sites[siteName];
                iisManager.Sites.Remove(site);
                iisManager.CommitChanges();
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
            return Content("1");
        }

      
            
    }
}
