using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using monacos.us_mvc.BusinessObjects;

namespace monacos.us_mvc.Controllers
{
    public partial class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }
    }
}