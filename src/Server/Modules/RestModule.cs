using System.Collections.Specialized;
using System.IO;
using System.Linq;
using Nancy;
using TreeGecko.Library.Archery.Constants;
using TreeGecko.Library.Archery.Managers;
using TreeGecko.Library.Common.Helpers;
using TreeGecko.Library.Net.Objects;

namespace TreeGecko.Archery.Server.Modules
{
    public class RestModule : NancyModule
    {
        
        public RestModule()
        {
            Post["/rest/login"] = _parameters =>
            {
                Response response = HandleLoginPost(_parameters);
                response.ContentType = "application/json";
                return response;
            };

            
        }

       

        private string HandleLoginPost(DynamicDictionary _parameters)
        {
            return null;
        }
        

        private string ReadBody()
        {
            using (StreamReader sr = new StreamReader(Request.Body))
            {
                string json = sr.ReadToEnd();
                return json;
            }

            return null;
        }

        

    }
}