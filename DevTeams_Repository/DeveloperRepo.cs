using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeams_Repositories
{
    public class DeveloperRepo
    {
        protected readonly List<Developer> _developerDirectory = new List<Developer>();

        public Developer CreateDeveloper(string name)
        {
            Developer developer = new Developer(name);
            _developerDirectory.Add(developer);
            return developer;
        }

        public Developer CreateDeveloper(string name, bool hasPluralsightLicense)
        {
            Developer developer = new Developer(name, hasPluralsightLicense);
            _developerDirectory.Add(developer);
            return developer;
        }

        public List<Developer> GetAllDevelopers()
        {
            return _developerDirectory;
        }

        public Developer GetDeveloperByName(string name)
        {
            foreach(Developer developer in _developerDirectory)
            {
                if(developer.Name.ToLower() == name.ToLower())
                {
                    return developer;
                }
            }
            return null;
        }

        public bool RemoveDeveloper(Developer developer)
        {
            return _developerDirectory.Remove(developer);
        }
    }
}
