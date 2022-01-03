using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UIOMaticAddons.Export.Controllers;
using Umbraco;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Persistence;
using Umbraco.Web;
using Umbraco.Web.JavaScript;

namespace UIOMaticAddons.Export
{
    
        public class ServerVariableParserEventComposer : IUserComposer
        {
            public void Compose(Composition composition)
            {
                composition.Components().Append<ServerVariableParserEvent>();
            }
        }

        public class ServerVariableParserEvent : IComponent
        {
            public ServerVariableParserEvent()
            {

            }

            public void Initialize()
            {
                ServerVariablesParser.Parsing += this.ServerVariablesParser_Parsing;
            }

            void ServerVariablesParser_Parsing(object sender, Dictionary<string, object> e)
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

            public void Terminate()
            { }


        }

        
    }
