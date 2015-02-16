using TreeGecko.Library.Net.Objects;

namespace TreeGecko.Archery.Server.JsonObjects
{
    public class User : BaseObject
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }

        public User()
        {
            
        }

        public User(TGUser _user)
            : base(_user.Guid)
        {
            Username = _user.Username;
            FirstName = _user.GivenName;
            LastName = _user.FamilyName;
            EmailAddress = _user.EmailAddress;
        }

    }
}