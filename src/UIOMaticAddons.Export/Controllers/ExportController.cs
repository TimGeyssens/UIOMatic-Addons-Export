using CsvHelper;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Reflection;
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
                textWriter.NewLine = "\n";
                using (var csv = new CsvWriter(textWriter))
                {
                    var os = new UIOMatic.Services.PetaPocoObjectService();

                    var data = os.GetAll(UIOMatic.Helper.GetUIOMaticTypeByAlias(typeAlias));

                    csv.Configuration.Delimiter = ";";
                    //csv.Configuration.QuoteAllFields = true;
                    csv.Configuration.HasHeaderRecord = true;

                    //csv.WriteRecords(data);

                  
                    

                    foreach (var item in data)
                    {
                        Type myObjOriginalType = item.GetType();
                        PropertyInfo[] myProps = myObjOriginalType.GetProperties();


                        //var x = new ExpandoObject() as IDictionary<string, Object>;
                        

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