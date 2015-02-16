using System.Linq;
using Nancy;
using TreeGecko.Archery.Server.JsonObjects;
using TreeGecko.Library.Archery.Managers;
using TreeGecko.Library.Common.Helpers;
using TreeGecko.Library.Net.Objects;

namespace TreeGecko.Archery.Server.Helpers
{
    public static class AuthHelper
    {
        public static User ValidateToken(ArcheryScorerManager _manager, Request _request, out TGUser _tgUser)
        {
            string username = _request.Headers["Username"].First();
            string token = _request.Headers["AuthToken"].First();

            if (_manager.ValidateUser(username, token, out _tgUser))
            {
                User result = new User(_tgUser);

                return result;
            }

            return null;
        }

        public static LoginResult Authorize(ArcheryScorerManager _manager,
            string _username, string _password, out TGUser _user)
        {
            LoginResult result = new LoginResult();
            _user = _manager.GetUser(_username);

            if (_user != null)
            {
                if (_user.Active)
                {
                    if (_manager.ValidateUser(_user, _password))
                    {
                        string token = _manager.GetAuthorizationToken(_user.Guid, _password);

                        result.Result = "Success";
                        result.AuthToken = token;
                        result.Username = _username;
                    }
                    else
                    {
                        //Bad password or username
                        TraceFileHelper.Warning("User not found");
                        _user = null;

                        result.Result = "BadUserOrPassword";
                    }
                }
                else
                {
                    //user not active
                    //Todo - Log Something
                    TraceFileHelper.Warning("User Not Active");
                    _user = null;

                    result.Result = "NotActive";
                }
            }
            else
            {
                //User not found
                TraceFileHelper.Warning("User not found");
                result.Result = "BadUserOrPassword";
            }

            return result;
        }

    }
}