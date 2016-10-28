using CsvHelper;
using System;
using System.IO;
using Umbraco.Core.IO;
using Umbraco.Web.Editors;

namespace UIOMaticAddons.Export.Controllers
{
    public class ExportController: UmbracoAuthorizedJsonController
    {
        public object GetExport(string typeAlias)
        {
            var guid = Guid.NewGuid();

            using (var textWriter = File.CreateText(IOHelper.MapPath(@"~\App_Plugins\UIOMaticAddons\Exports\" + guid + ".csv")))
            {
                using (var csv = new CsvWriter(textWriter))
                {
                    var os = new UIOMatic.Services.PetaPocoObjectService();

                    var data = os.GetAll(UIOMatic.Helper.GetUIOMaticTypeByAlias(typeAlias));

                    csv.Configuration.Delimiter = ";";
                    csv.Configuration.HasHeaderRecord = true;

                    csv.WriteRecords(data);
                }
            }
           
          return new { data = "../App_Plugins/UIOMaticAddons/Exports/" + guid.ToString() + ".csv" };
        }
    }
}