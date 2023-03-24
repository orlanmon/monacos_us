using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using monacos.us_mvc.Models;
using monacos.us_mvc.BusinessObjects;


namespace monacos.us_mvc.Controllers
{
    public partial class ContactController : Controller
    {

        // Note View Name is identical to action name so cshtml file name is identical to action method name.

        
        public ActionResult Contact_Dlg_One()
        {
           
            return PartialView("Contact_Dlg_One");

        }

        public ActionResult Contact_Dlg_Two()
        {
            
            return PartialView("Contact_Dlg_Two");


        }


        public ActionResult Contact_Dlg_Three()
        {

            return PartialView("Contact_Dlg_Three");


        }

        [HttpPost]
        public ActionResult ContactSubmit(string first_name, string last_name, string subject, string email, string msg )
        {

            ContactRequest_BO objContactRequest_BO = new ContactRequest_BO();
            string Status = "fail";
            JsonResult objJR = null;

            try
            {
              


                if (objContactRequest_BO.SendContactRequest(first_name, last_name, email, subject, msg))
                {

                    Status = "success";

                    Response.StatusCode = 200;
                    Response.StatusDescription = Status;

                }
                else
                {
                    Response.StatusCode = 500;
                    Response.StatusDescription = Status;

                }

                 objJR = new JsonResult
                {

                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new { status = Status, description = Status },
                    // Data = mode can go here as well    Any .NET Object Will be Serialized to JSON Object ( Not String ) on receiving side AJAX call.
                    // Data =  "{ status : 'success' }",  You Can Do this But the Return is a String that needs to be JSONify
                    ContentType = "application/json",
                    ContentEncoding = System.Text.Encoding.UTF8
                };

            }
            catch(Exception e)
            {
                Response.StatusCode = 500;
                Response.StatusDescription = Status + " - error - " + e.Message;

                objJR = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new { status = Status, description = "error - " + e.Message },
                    ContentType = "application/json",
                    ContentEncoding = System.Text.Encoding.UTF8
                };


            }

     
            return objJR;

        }




    }
}