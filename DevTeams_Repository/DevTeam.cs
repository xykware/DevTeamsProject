using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveveloperTeams_Repository
{
    public class DevTeam
    {
        public List<Developer> _members = new List<Developer>();

        public Guid _id = Guid.NewGuid();

        public DevTeam() { }

        public DevTeam(string teamName)
        {
            Name = teamName;
        }

        public Guid ID
        {
            get
            {
                return _id;
            }
        }

        public string Name { get; set; }
    }
}
