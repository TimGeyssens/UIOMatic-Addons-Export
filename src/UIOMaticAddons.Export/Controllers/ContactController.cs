using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace UIOMaticAddons.Export.Controllers
{
    public class ContactController : SurfaceController
    {
        [ChildActionOnly]
        public ActionResult Render()
        {
            return PartialView("ContactEntry",new Models.ContactEntry());
        }

        [HttpPost]
        public ActionResult HandlePost(Models.ContactEntry model)
        {
            if (!ModelState.IsValid)
                return CurrentUmbracoPage();

            //add to db
            model.Created = DateTime.Now;
            var db = ApplicationContext.DatabaseContext.Database;
            db.Insert(model);

            //send email, do other things...

            TempData["Success"] = true;
            return RedirectToCurrentUmbracoPage();
        }
    }
}