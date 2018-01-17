using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace UIOMaticAddons.Export.Configuration
{
    public class UIOMaticConfiguration
    {
        public static UIOMaticConfiguration FromWebConfig(NameValueCollection nameValueCollection)
        {
            try
            {
                return new UIOMaticConfiguration
                {
                    Delimiter = nameValueCollection["iomatic:DelimeterKey"]
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Invalid settings in web.config", ex);
            }
        }

        public string Delimiter { get; set; }
    }
}