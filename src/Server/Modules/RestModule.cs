using System.Collections.Generic;
using System.IO;
using Nancy;
using Newtonsoft.Json;
using TreeGecko.Archery.Server.Helpers;
using TreeGecko.Archery.Server.JsonObjects;
using TreeGecko.Library.Archery.Managers;
using TreeGecko.Library.Archery.Objects;
using TreeGecko.Library.Net.Objects;

namespace TreeGecko.Archery.Server.Modules
{
    public class RestModule : NancyModule
    {
        private ArcheryScorerManager m_Manager = new ArcheryScorerManager();

        public RestModule()
        {
            Post["/rest/login"] = _parameters =>
            {
                Response response = HandleLoginPost(_parameters);
                response.ContentType = "application/json";
                return response;
            };

            Get["/rest/shooters"] = _parameters =>
            {
                Response response = HandleShootersGet(_parameters);
                response.ContentType = "application/json";
                return response;
            };

            Get["/rest/shooter/{guid}"] = _parameters =>
            {
                Response response = HandleShooterGet(_parameters);
                response.ContentType = "application/json";
                return response;
            };

            Post["/rest/shooter"] = _parameters =>
            {
                Response response = HandleShooterPost(_parameters);
                response.ContentType = "application/json";
                return response;
            };
        }

        private string HandleShooterPost(DynamicDictionary _parameters)
        {
            return null;
        }

        private string HandleShooterGet(DynamicDictionary _parameters)
        {
            return null;
        }

        private string HandleShootersGet(DynamicDictionary _parameters)
        {
            TGUser user;

            User jUser = AuthHelper.ValidateToken(m_Manager, Request, out user);
            if (jUser != null )
            {
                Account account = m_Manager.GetAccountByUser(user.Guid);

                if (account != null)
                {
                    var shooters = m_Manager.GetShooters(account.Guid);

                    var jShooters = JsonHelper.ConvertShooters(shooters);
                    return JsonConvert.SerializeObject(jShooters);
                }

                return null;
            }

            return null;
        }

        private string HandleLoginPost(DynamicDictionary _parameters)
        {
            string json = ReadBody();
            LoginRequest jLoginRequest = JsonConvert.DeserializeObject<LoginRequest>(json);

            if (jLoginRequest != null)
            {
                TGUser user;

                LoginResult jResult = AuthHelper.Authorize(m_Manager,
                    jLoginRequest.Username, jLoginRequest.Password, out user);

                return JsonConvert.SerializeObject(jResult);
            }

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