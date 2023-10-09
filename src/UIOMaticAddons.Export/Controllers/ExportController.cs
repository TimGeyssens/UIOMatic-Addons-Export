using CsvHelper;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Formats.Asn1;
using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Web.BackOffice.Controllers;
using System.Globalization;
using Umbraco.Cms.Core.Cache;
using UIOMatic;
using UIOMatic.Interfaces;
using UIOMatic.Services;
using Umbraco.Cms.Core.Hosting;
using System.Linq;

namespace UIOMaticAddons.Export.Controllers {
	public class ExportController : UmbracoAuthorizedJsonController {
		private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
		private readonly IHostingEnvironment _umbHostingEnvironment;
		private readonly AppCaches _appCaches;
		private readonly IUIOMaticHelper _uIOMaticHelper;
		private readonly UIOMaticObjectService _uIOMaticObjectService;

		public ExportController(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment, Umbraco.Cms.Core.Hosting.IHostingEnvironment umbHostingEnvironment, AppCaches appCaches, IUIOMaticHelper uIOMaticHelper, UIOMaticObjectService uIOMaticObjectService) {
			_hostingEnvironment = hostingEnvironment;
			_umbHostingEnvironment = umbHostingEnvironment;
			_appCaches = appCaches;
			_uIOMaticHelper = uIOMaticHelper;
			_uIOMaticObjectService = uIOMaticObjectService;
		}

		public object GetExport(string typeAlias) {
			var guid = Guid.NewGuid();

			//Create the path if it doesn't exist
			var path = Path.Combine(_hostingEnvironment.WebRootPath, "App_Plugins/UIOMaticAddons/Exports/");
			System.IO.Directory.CreateDirectory(path);
			using (var textWriter = new StreamWriter(Path.Combine(path, guid + ".csv"))) {
				textWriter.NewLine = "\n";
				using (var csv = new CsvWriter(textWriter, CultureInfo.InvariantCulture)) {
					var os = new UIOMatic.Services.NPocoObjectService(_appCaches, _umbHostingEnvironment, _uIOMaticHelper, (UIOMaticObjectService)_uIOMaticObjectService);

					var data = os.GetAll(_uIOMaticHelper.GetUIOMaticTypeByAlias(typeAlias));

				

					csv.WriteHeader(data.FirstOrDefault().GetType()); //Write the header record
					csv.NextRecord();//WriteHeader will not advance you to the next row, so this line is needed


					foreach (var item in data)
					{
						Type myObjOriginalType = item.GetType();
						PropertyInfo[] myProps = myObjOriginalType.GetProperties();

						foreach (var prop in myProps)
						{

							//x.Add(prop.Name, prop.GetValue(item) == null ? string.Empty: prop.GetValue(item).ToString().Replace(System.Environment.NewLine," "));
							csv.WriteField(prop.GetValue(item) == null ? string.Empty : prop.GetValue(item).ToString().Replace(System.Environment.NewLine, " "));
						}


						//foreach (var v in (IDictionary<string, object>)item)
						//{
						//    csv.WriteField(v.Value);
						//}
						csv.NextRecord();
						//csv.WriteRecord(x);
					}

				}
			}

			return new { data = "../App_Plugins/UIOMaticAddons/Exports/" + guid.ToString() + ".csv" };
		}
	}
}
