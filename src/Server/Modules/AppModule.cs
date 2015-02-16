using System.Collections.Specialized;
using Nancy;
using Nancy.Responses;
using TreeGecko.Library.Archery.Constants;
using TreeGecko.Library.Archery.Managers;
using TreeGecko.Library.Archery.Objects;
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
                if (Register(_parameters))
                {
                    return Response.AsRedirect("/#/login", RedirectResponse.RedirectType.Permanent);
                }
                
                return View["register.sshtml"];
            };
        }

        private bool Register(DynamicDictionary _parameters)
        {
            string username = Request.Form["txtUsername"];
            string email = Request.Form["txtEmailAddress"];
            string password = Request.Form["txtPassword"];

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

                Account account = new Account {PrimaryUserGuid = user.Guid};
                manager.Persist(account);

                NameValueCollection nvc = new NameValueCollection
                {
                    {"SystemUrl", Config.GetSettingValue("SystemUrl")},
                    {"ValidationText", validation.ValidationText }
                };
                manager.SendCannedEmail(user, CannedEmailNames.ValidateEmailAddress, nvc);

                return true;
            }

            return false;
        }
    }
}
