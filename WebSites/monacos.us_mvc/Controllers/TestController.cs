using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace monacos.us_mvc.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(string Param1, string Param2 )
        {

            System.Console.Write(Param1);
            System.Console.Write(Param1);

            return View();
        }



        [HttpPost]
        public ActionResult APICallOne(string Param1, string Param2 )
        {


            System.Console.Write(Param1);
            System.Console.Write(Param2);

            return new HttpStatusCodeResult(200);

        }

        [HttpGet]
        public ActionResult APICallTwo(string id)
        {


            System.Console.Write(id);
           
            return new HttpStatusCodeResult(200);

        }


        
        [HttpGet]
        public ActionResult APICallThree(string clientid, string campaignid, string id)
        {

            System.Console.Write(clientid);
            System.Console.Write(campaignid);
            System.Console.Write(id);

            return new HttpStatusCodeResult(200);

        }


        [HttpGet]
        public ActionResult APICallFour(string Param1, string Param2, string Param3 )
        {

            System.Console.Write(Param1);
            System.Console.Write(Param2);
            System.Console.Write(Param3);

            return new HttpStatusCodeResult(200);

        }

        [HttpGet]
        public ActionResult APICallFive(string Param1, string Param2, string Param3)
        {

            JsonResult objJR = new JsonResult {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new { status = "success" },
                // Data = mode can go here as well    Any .NET Object Will be Serialized to JSON Object ( Not String ) on receiving side AJAX call.
                // Data =  "{ status : 'success' }",  You Can Do this But the Return is a String that needs to be JSONify
                ContentType = "application/json",
                ContentEncoding = System.Text.Encoding.UTF8
            };

            System.Console.Write(Param1);
            System.Console.Write(Param2);
            System.Console.Write(Param3);

            Response.StatusCode = 200;
            Response.StatusDescription = "Success";

            return objJR;

        }

        // Important!!!
        // Note Parameters on Controller Action Method can be Populated From Model, Post Data ( Form or JSON Data with Post) , URI Route ( If configured), and query string!!



        // Action Method to Invoke Function Below
        public ActionResult IndexDlgTwoContent()
        {

            
            string ViewContent = RenderViewToString("ViewName", null );

            return Content(ViewContent);

        }


        // Cool Method To Render Content of View as String 
        public string RenderViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new System.IO.StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }



    }
}