using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using UIOMaticAddons.Export.Controllers;
using Umbraco;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Infrastructure.WebAssets;
using Umbraco.Extensions;
using Umbraco.Web;

namespace UIOMaticAddons.Export {

	public class ServerVariableParserEventComposer : IComposer {
		public void Compose(IUmbracoBuilder builder) {
		
			builder.AddNotificationHandler<ServerVariablesParsingNotification, ServerVariableParserEvent>();

		}
	}

	public class ServerVariableParserEvent : INotificationHandler<ServerVariablesParsingNotification> {
		private readonly UmbracoApiControllerTypeCollection _umbracoApiControllerTypeCollection;
		private readonly IActionContextAccessor _actionContextAccessor;
		private readonly IUrlHelperFactory _urlFactory;

		public ServerVariableParserEvent(UmbracoApiControllerTypeCollection umbracoApiControllerTypeCollection, IActionContextAccessor actionContext, IUrlHelperFactory urlFactory) {
			_umbracoApiControllerTypeCollection = umbracoApiControllerTypeCollection;
			_actionContextAccessor = actionContext;
			_urlFactory = urlFactory;
		}

		public void Terminate() { }

		public void Handle(ServerVariablesParsingNotification notification) 
			var urlHelper = _urlFactory.GetUrlHelper(_actionContextAccessor.ActionContext);



			var mainDictionary = new Dictionary<string, object>
		{
				{
					"ecBaseUrl",
					urlHelper.GetUmbracoApiServiceBaseUrl<ExportController>(_umbracoApiControllerTypeCollection, controller => controller.ToString())
				}
			};

			if (!notification.ServerVariables.ContainsKey("uioMaticAddons")) {
				notification.ServerVariables.Add("uioMaticAddons", mainDictionary);
			}
		}
	}


}
