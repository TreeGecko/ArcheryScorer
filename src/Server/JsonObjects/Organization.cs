using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace TreeGecko.Archery.Server.JsonObjects
{
    public class Organization : BaseObject
    {
        public string Name { get; set; }

        public Organization()
        {
            
        }

        public Organization(Library.Archery.Objects.Organization _organization)
        {
            Guid = _organization.Guid.ToString();
            Name = _organization.Name;
        }
    }
}