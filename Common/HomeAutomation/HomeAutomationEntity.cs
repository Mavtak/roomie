using System;
using System.Xml.Linq;

namespace Roomie.Common.HomeAutomation
{
    //TODO: make the website use these models
    public abstract class HomeAutomationEntity
    {
        //TODO: allow names to be null
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    value = null;
                }
                name = value;
            }
        }

        protected string address;
        internal string Address_Hack
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }

        private string notes;
        public string Notes
        {
            get
            {
                return notes;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    value = null;
                }

                notes = value;
            }
        }

        internal XElement ToXElement(string nodeName)
        {
            var result = new XElement(nodeName);

            if (!String.IsNullOrWhiteSpace(address))
                result.Add(new XAttribute("Address", address));

            if (!String.IsNullOrWhiteSpace(Name))
                result.Add(new XAttribute("Name", Name));

            return result;
        }

        public virtual void FromXElement(XElement node)
        {
            Name = node.GetAttributeStringValue("Name");
            Notes = node.GetAttributeStringValue("Notes");
            address = node.GetAttributeStringValue("Address");
        }


        #region Object overrides

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return this.Equals((HomeAutomationEntity)obj);
        }

        public bool Equals(HomeAutomationEntity that)
        {
            if (that == null)
            {
                return false;
            }

            if (!String.Equals(this.address, that.address))
            {
                return false;
            }

            if (!String.Equals(this.Name, that.Name))
            {
                return false;
            }

            // don't compare on Notes

            return true;
        }

        public override int GetHashCode()
        {
            return address.GetHashCode();
        }

        #endregion
    }
}
