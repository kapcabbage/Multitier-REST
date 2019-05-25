using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;
using RestSharp;

namespace DataAccess.Common
{
    public static class CountryValidator
    {
        public static bool Validate(string code)
        {
            var client = new RestClient("https://restcountries.eu/rest/v2/alpha");
            
            var request = new RestRequest("/{code}", Method.GET);
            request.AddUrlSegment("code", code); // replaces matching token in request.Resource
            // execute the request
            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                return true;
            }

            return false;
        }
    }
}
