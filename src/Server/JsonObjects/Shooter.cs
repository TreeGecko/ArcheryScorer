namespace TreeGecko.Archery.Server.JsonObjects
{
    public class Shooter : BaseObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }

        public Shooter()
        {
            
        }

        public Shooter(Library.Archery.Objects.Shooter _shooter)
            : base(_shooter.Guid)
        {
            FirstName = _shooter.FirstName;
            LastName = _shooter.LastName;
            BirthDate = _shooter.BirthDate.ToString("d");
        }

    }
}