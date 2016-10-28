using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UIOMaticAddons.Export.Controllers;
using Umbraco.Core;
using Umbraco.Core.Persistence;
using Umbraco.Web.UI.JavaScript;
using Umbraco.Web;
namespace UIOMaticAddons.Export
{
    public class App: ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            //var ctx = applicationContext.DatabaseContext;
            //var db = new DatabaseSchemaHelper(ctx.Database, applicationContext.ProfilingLogger.Logger, ctx.SqlSyntax);

            ////Check if the DB table does NOT exist
            //if (!db.TableExist("ContactEntries"))
            //{
            //    //Create DB table - and set overwrite to false
            //    db.CreateTable(false, typeof(ContactEntry));
            //}

            ServerVariablesParser.Parsing += ServerVariablesParser_Parsing;
        }

        private void ServerVariablesParser_Parsing(object sender, Dictionary<string, object> e)
        {
            if (HttpContext.Current == null) return;
            var urlHelper = new UrlHelper(new RequestContext(new HttpContextWrapper(HttpContext.Current), new RouteData()));

            var mainDictionary = new Dictionary<string, object>
            {
                {
                    "ecBaseUrl",
                    urlHelper.GetUmbracoApiServiceBaseUrl<ExportController>(controller => controller.ToString())
                }
            };

            if (!e.Keys.Contains("uioMaticAddons"))
            {
                e.Add("uioMaticAddons", mainDictionary);
            }
        }
    }
}