using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeams_Repositories
{
    public class Developer
    {
        public List<DevTeam> _teams = new List<DevTeam>();

        public Guid _id = Guid.NewGuid();

        public Developer() { }

        public Developer(string name)
        {
            Name = name;
        }

        public Developer(string name, bool hasPluralsightLicense)
        {
            Name = name;
            HasPluralsightLicense = hasPluralsightLicense;
        }

        public Guid ID 
        { 
            get
            {
                return _id;
            }
        }

        public string Name { get; set; }

        public bool HasPluralsightLicense { get; set; }
    }
}
