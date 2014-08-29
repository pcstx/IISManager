using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.DirectoryServices; 

namespace IISManager.Controllers
{
    public class OldController : Controller
    {
        //
        // GET: /Old/

        public ActionResult Index()
        {

            List<IIS> list_iis = new List<IIS>();
            try
            {
                string path = "IIsWebService://" + System.Environment.MachineName + "/W3SVC";
                System.Collections.ArrayList webSite = new System.Collections.ArrayList();
                DirectoryEntry iis = new DirectoryEntry("IIS://localhost/W3SVC");
                if (iis != null)
                {
                    foreach (DirectoryEntry entry in iis.Children)
                    {
                        if (string.Compare(entry.SchemaClassName, "IIsWebServer") == 0)
                        {
                            IIS i = new IIS();
                            i.Name = entry.Name;
                            i.ServerComment = entry.Properties["ServerComment"].Value.ToString();
                            i.Path = entry.Path;
                            list_iis.Add(i);
                        } 
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

             

            return View(list_iis);
        }

    }

    public class IIS
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string ServerComment { get; set; }
    }


}
