using System.Collections.Generic;
using TreeGecko.Archery.Server.JsonObjects;

namespace TreeGecko.Archery.Server.Helpers
{
    public static class JsonHelper
    {
        public static List<Shooter> ConvertShooters(List<Library.Archery.Objects.Shooter> _shooters)
        {
            List<Shooter> jShooters = new List<Shooter>();

            foreach (Library.Archery.Objects.Shooter shooter in _shooters)
            {
                Shooter jShooter = new Shooter(shooter);
                jShooters.Add(jShooter);
            }

            return jShooters;
        }


    }
}