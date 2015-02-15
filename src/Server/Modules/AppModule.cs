﻿using System.Collections.Specialized;
using System.Linq;
using Nancy;
using TreeGecko.Library.Archery.Constants;
using TreeGecko.Library.Archery.Managers;
using TreeGecko.Library.Common.Helpers;
using TreeGecko.Library.Net.Objects;

namespace TreeGecko.Archery.Server.Modules
{
    public class AppModule : NancyModule
    {
        public AppModule()
        {
            Get["/"] = _parameters =>
            {
                return View["index.sshtml"];
            };

            Get["/dev/BuildDB"] = _parameters =>
            {
                bool devMode = Config.GetBooleanValue("DevMode", false);

                if (devMode)
                {
                    ArcheryScorerStructureManager assm = new ArcheryScorerStructureManager();
                    assm.BuildDB();

                    return View["dev_dbbuildresult.sshtml"];
                }

                return null;
            };

            Get["/register"] = _parameters =>
            {
                return View["register.sshtml"];
            };

            Post["/register"] = _parameters =>
            {
                Response response = (Response)Register(_parameters);
                response.ContentType = "application/json";
                return response;
            };
        }

        private string Register(DynamicDictionary _parameters)
        {
            string username = Request.Headers["Username"].First();
            string email = Request.Headers["Email"].First();
            string password = Request.Headers["Password"].First();

            ArcheryScorerManager manager = new ArcheryScorerManager();

            TGUser user = manager.GetUser(username);

            if (user == null)
            {
                user = new TGUser
                {
                    IsVerified = false,
                    Active = true,
                    DisplayName = username,
                    EmailAddress = email,
                    Username = username
                };
                manager.Persist(user);

                TGUserPassword userPassword = TGUserPassword.GetNew(user.Guid, username, password);
                manager.Persist(userPassword);

                TGUserEmailValidation validation = new TGUserEmailValidation(user);
                manager.Persist(validation);

                NameValueCollection nvc = new NameValueCollection
                {
                    {"SystemUrl", Config.GetSettingValue("SystemUrl")},
                    {"ValidationText", validation.ValidationText }
                };
                manager.SendCannedEmail(user, CannedEmailNames.ValidateEmailAddress, nvc);

                return "{ \"Result\":\"Success\" }";
            }

            return "{ \"Result\":\"UsernameNotAvailable\" }";
        }
    }
}