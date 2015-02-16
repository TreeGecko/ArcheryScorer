using System;

namespace TreeGecko.Archery.Server.JsonObjects
{
    public abstract class BaseObject
    {
        protected BaseObject()
        {
            
        }

        protected BaseObject(Guid _guid)
        {
            Guid = _guid.ToString();
        }


        public string Guid { get; set; }
    }
}