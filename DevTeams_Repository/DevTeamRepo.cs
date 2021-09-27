using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeams_Repository
{
    public class DevTeamRepo
    {
        protected readonly List<DevTeam> _devTeamDirectory = new List<DevTeam>();

        public DevTeam CreateTeam(string teamName)
        {
            DevTeam devTeam = new DevTeam(teamName);
            _devTeamDirectory.Add(devTeam);
            return devTeam;
        }

        public bool AddDeveloperToTeam(Developer developer, DevTeam team)
        {
            int currentCount = team._members.Count;
            team._members.Add(developer);
            developer._teams.Add(team);
            return (team._members.Count == currentCount + 1);
        }

        public List<DevTeam> GetAllTeams()
        {
            return _devTeamDirectory;
        }

        public DevTeam GetTeamByName(string teamName)
        {
            foreach (DevTeam team in _devTeamDirectory)
            {
                if (team.Name.ToLower() == teamName.ToLower())
                {
                    return team;
                }
            }
            return null;
        }

        public bool RemoveTeam(DevTeam devTeam)
        {
            return _devTeamDirectory.Remove(devTeam);
        }

        public bool RemoveDeveloperFromTeam(Developer developer, DevTeam team)
        {
            int currentCount = team._members.Count;
            team._members.Remove(developer);
            developer._teams.Remove(team);
            return (team._members.Count == currentCount - 1);
        }
    }
}
