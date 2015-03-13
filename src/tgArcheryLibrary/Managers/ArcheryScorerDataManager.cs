using System;
using TreeGecko.Library.Archery.Objects;
using TreeGecko.Library.Net.Managers;

namespace TreeGecko.Library.Archery.Managers
{
    public class ArcheryScorerDataManager : AbstractCoreManager
    {
        public ArcheryScorerDataManager()
            : base("AS")
        {
        }

        public void BuildData()
        {
            ArcheryScorerManager manager = new ArcheryScorerManager();

            BuildOrganizations(manager);
        }

        private void BuildOrganizations(ArcheryScorerManager _manager)
        {
            Organization organization = new Organization
            {
                Guid = new Guid("4f7588aa-558a-460e-b71e-d9bf99ed5201"),
                Name = "JOAD",
                Description = "Junior Olympic Archery Development"
            };
            _manager.Persist(organization);
        
            organization = new Organization
            {
                Guid = new Guid("a17736b8-3514-420f-9938-ef884584ed20"),
                Name = "NFAA",
                Description = "National Field Archery Association"
            };
            _manager.Persist(organization);
        }


    }
}
