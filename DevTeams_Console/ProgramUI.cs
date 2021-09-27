using DevTeams_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeams_Console
{
    class ProgramUI
    {
        private DevTeamRepo _devTeamRepo = new DevTeamRepo();
        private DeveloperRepo _developerRepo = new DeveloperRepo();

        public void Run()
        {
            SeedData();
            RunMenu();
        }

        private void RunMenu()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Title = "Komodo Insurance";
                Console.Clear();
                Console.WriteLine
                    (
                        "1. Create a new dev team\n" +
                        "2. Create a new developer\n" +
                        "3. Add developers to a dev team\n" +
                        "4. Show all dev teams\n" +
                        "5. Show all developers\n" +
                        "6. Find a specific dev team\n" +
                        "7. Find a specific developer\n" +
                        "8. Show all developers for a specific dev team\n" +
                        "9. Show all developers without a Pluralsight license\n" +
                        "10. Remove a dev team\n" +
                        "11. Remove a developer\n" +
                        "12. Remove developers from a dev team\n" +
                        "0. Exit\n" +
                        "\nEnter the number of your selection:"
                    );

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        CreateDevTeam();
                        break;
                    case "2":
                        CreateDeveloper();
                        break;
                    case "3":
                        AddDevelopersToTeam();
                        break;
                    case "4":
                        ShowDevTeams();
                        break;
                    case "5":
                        ShowDevelopers();
                        break;
                    case "6":
                        FindDevTeam();
                        break;
                    case "7":
                        FindDeveloper();
                        break;
                    case "8":
                        ShowDevelopersOfDevTeam();
                        break;
                    case "9":
                        ShowDevelopersWithoutPluralsight();
                        break;
                    case "10":
                        RemoveDevTeam();
                        break;
                    case "11":
                        RemoveDeveloper();
                        break;
                    case "12":
                        RemoveDevelopersFromDevTeam();
                        break;
                    case "0":
                        isRunning = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine
                            ("Please enter a valid number" +
                            "\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void CreateDevTeam()
        {
            Console.Title = "Komodo Insurance - Create Team";
            Console.Clear();
            Console.WriteLine("\nEnter the team's name:");
            _devTeamRepo.CreateTeam(Console.ReadLine());
        }

        private void CreateDeveloper()
        {
            Console.Title = "Komodo Insurance - Create Developer";
            Console.Clear();
            Console.WriteLine("\nEnter the developer's name:");
            _developerRepo.CreateDeveloper(Console.ReadLine());
        }

        private void AddDevelopersToTeam()
        {
            Console.Title = "Komodo Insurance - Add Developers to Team";
            Console.Clear();
            int i = 1;

            foreach (DevTeam team in _devTeamRepo.GetAllTeams())
            {
                Console.WriteLine($"{i}. {team.Name}");
                i++;
            }

            Console.WriteLine("0. Cancel");
            Console.WriteLine("\nWhich team are you trying to add developers to?");
            int numberChoice = int.Parse(Console.ReadLine());

            if (numberChoice == 0)
            {
                return;
            }

            DevTeam teamChoice = new DevTeam();

            i = 1;

            foreach (DevTeam team in _devTeamRepo.GetAllTeams())
            {
                if (i == numberChoice)
                {
                    teamChoice = team;
                }

                i++;
            }

            while (true)
            {
                Console.Clear();
                i = 1;

                foreach (Developer developer in _developerRepo.GetAllDevelopers())
                {
                    bool isDeveloperInTeam = false;

                    foreach (Developer developerInTeam in teamChoice._members)
                    {
                        if(developer == developerInTeam)
                        {
                            isDeveloperInTeam = true;
                        }
                    }

                    if (!isDeveloperInTeam)
                    {
                        Console.WriteLine($"{i}. {developer.Name}");
                        i++;
                    }
                }

                Console.WriteLine("0. Cancel");
                Console.WriteLine($"\nWhich developer are you trying to add to \"{teamChoice.Name}\"?");
                numberChoice = int.Parse(Console.ReadLine());

                if (numberChoice == 0)
                {
                    return;
                }

                Developer developerChoice = new Developer();

                i = 1;

                foreach (Developer developer in _developerRepo.GetAllDevelopers())
                {
                    bool isDeveloperInTeam = false;

                    foreach (Developer developerInTeam in teamChoice._members)
                    {
                        if (developer == developerInTeam)
                        {
                            isDeveloperInTeam = true;
                        }
                    }

                    if (i == numberChoice)
                    {
                        developerChoice = developer;
                    }

                    if (!isDeveloperInTeam)
                    {
                        i++;
                    }
                }

                bool success = _devTeamRepo.AddDeveloperToTeam(developerChoice, teamChoice);
                Console.Clear();

                if (success)
                {
                    Console.WriteLine($"{developerChoice.Name} has been added to team: {teamChoice.Name}");
                }
                else
                {
                    Console.WriteLine($"Failed to add {developerChoice.Name} to team: {teamChoice.Name}");
                }

                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
            }
        }

        private void ShowDevTeams()
        {
            Console.Title = "Komodo Insurance - Teams";
            Console.Clear();

            foreach (DevTeam team in _devTeamRepo.GetAllTeams())
            {
                Console.WriteLine
                    ($"Team: {team.Name}\n" +
                    $"ID: {team.ID}\n" +
                    $"Number of members: {team._members.Count}\n");
            }

            Console.WriteLine("\nPress enter to continue...");
            Console.ReadLine();
        }

        private void ShowDevelopers()
        {
            Console.Title = "Komodo Insurance - Developers";
            Console.Clear();

            foreach (Developer developer in _developerRepo.GetAllDevelopers())
            {
                Console.WriteLine
                    ($"Developer: {developer.Name}" +
                    $"\nID: {developer.ID}" +
                    $"\nHas Pluralsight license: {(developer.HasPluralsightLicense ? "Yes" : "No")}");

                string teamNames = "Teams: ";

                foreach (DevTeam team in developer._teams)
                {
                    teamNames += $" {team.Name} |";
                }

                Console.WriteLine(teamNames + "\n");
            }

            Console.WriteLine("\nPress enter to continue...");
            Console.ReadLine();
        }

        private void FindDevTeam()
        {
            Console.Title = "Komodo Insurance - Find Team";
            Console.Clear();
            Console.WriteLine("Enter a team name to search for:");
            string searchName = Console.ReadLine();
            Console.Clear();

            bool success = false;

            foreach (DevTeam team in _devTeamRepo.GetAllTeams())
            {
                if (team.Name.ToLower() == searchName.ToLower())
                {
                    Console.WriteLine
                        ($"Team: {team.Name}" +
                        $"\nID: {team.ID}" +
                        $"\nNumber of members: {team._members.Count}");
                    success = true;
                }
            }

            if(!success)
            {
                Console.WriteLine($"Failed to find a team named: {searchName}");
            }

            Console.WriteLine("\nPress enter to continue...");
            Console.ReadLine();
        }

        private void FindDeveloper()
        {
            Console.Title = "Komodo Insurance - Find Developer";
            Console.Clear();
            Console.WriteLine("Enter a developer name to search for:");
            string searchName = Console.ReadLine();
            Console.Clear();

            bool success = false;

            foreach (Developer developer in _developerRepo.GetAllDevelopers())
            {
                if (developer.Name.ToLower() == searchName.ToLower())
                {
                    Console.WriteLine
                        ($"Developer: {developer.Name}" +
                        $"\nID: {developer.ID}" +
                        $"\nHas Pluralsight license: {(developer.HasPluralsightLicense ? "Yes" : "No")}");

                    string teamNames = "Teams: ";

                    foreach (DevTeam team in developer._teams)
                    {
                        teamNames += $" {team.Name} |";
                    }

                    Console.WriteLine(teamNames + "\n");
                    success = true;
                }
            }

            if (!success)
            {
                Console.WriteLine($"Failed to find a developer named: {searchName}");
            }

            Console.WriteLine("\nPress enter to continue...");
            Console.ReadLine();
        }

        private void ShowDevelopersOfDevTeam()
        {
            Console.Title = "Komodo Insurance - Find Team's Developers";
            Console.Clear();
            int i = 1;

            foreach (DevTeam team in _devTeamRepo.GetAllTeams())
            {
                Console.WriteLine($"{i}. {team.Name}");
                i++;
            }

            Console.WriteLine("0. Cancel");
            Console.WriteLine("\nWhich team are you trying to display developers from?");
            int numberChoice = int.Parse(Console.ReadLine());

            if (numberChoice == 0)
            {
                return;
            }

            DevTeam teamChoice = new DevTeam();

            i = 1;

            foreach (DevTeam team in _devTeamRepo.GetAllTeams())
            {
                if (i == numberChoice)
                {
                    teamChoice = team;
                }

                i++;
            }

            Console.Clear();

            foreach (Developer developer in teamChoice._members)
            {
                Console.WriteLine
                    ($"Developer: {developer.Name}" +
                    $"\nID: {developer.ID}" +
                    $"\nHas Pluralsight license: {(developer.HasPluralsightLicense ? "Yes" : "No")}");

                string teamNames = "Teams: ";

                foreach (DevTeam team in developer._teams)
                {
                    teamNames += $" {team.Name} |";
                }

                Console.WriteLine(teamNames + "\n");
            }

            Console.WriteLine("\nPress enter to continue...");
            Console.ReadLine();
        }

        private void ShowDevelopersWithoutPluralsight()
        {
            Console.Title = "Komodo Insurance - Find Developers Without Pluralsight";
            Console.Clear();
            int developerCount = 0;

            foreach (Developer developer in _developerRepo.GetAllDevelopers())
            {
                if (!developer.HasPluralsightLicense)
                {
                    Console.WriteLine
                        ($"Developer: {developer.Name}" +
                        $"\nID: {developer.ID}" +
                        $"\nHas Pluralsight license: {(developer.HasPluralsightLicense ? "Yes" : "No")}");

                    string teamNames = "Teams: ";

                    foreach (DevTeam team in developer._teams)
                    {
                        teamNames += $" {team.Name} |";
                    }

                    Console.WriteLine(teamNames + "\n");
                    developerCount++;
                }
            }

            if(developerCount == 0)
            {
                Console.WriteLine("Could not find any developers without a Pluralsight license.\n");
            }

            Console.WriteLine("\nPress enter to continue...");
            Console.ReadLine();
        }

        private void RemoveDevTeam()
        {
            Console.Title = "Komodo Insurance - Remove Team";
            Console.Clear();
            int i = 1;

            foreach (DevTeam team in _devTeamRepo.GetAllTeams())
            {
                Console.WriteLine($"{i}. {team.Name}");
                i++;
            }

            Console.WriteLine("0. Cancel");
            Console.WriteLine("\nWhich team are you trying to remove?");
            int numberChoice = int.Parse(Console.ReadLine());

            if (numberChoice == 0)
            {
                return;
            }

            DevTeam teamChoice = new DevTeam();

            i = 1;

            foreach (DevTeam team in _devTeamRepo.GetAllTeams())
            {
                if (i == numberChoice)
                {
                    teamChoice = team;
                }

                i++;
            }

            foreach(Developer developer in teamChoice._members)
            {
                developer._teams.Remove(teamChoice);
            }

            _devTeamRepo.RemoveTeam(teamChoice);
        }

        private void RemoveDeveloper()
        {
            Console.Title = "Komodo Insurance - Remove Developer";
            Console.Clear();
            int i = 1;

            foreach (Developer developer in _developerRepo.GetAllDevelopers())
            {
                Console.WriteLine($"{i}. {developer.Name}");
                i++;
            }

            Console.WriteLine("0. Cancel");
            Console.WriteLine("\nWhich developer are you trying to remove?");
            int numberChoice = int.Parse(Console.ReadLine());

            if (numberChoice == 0)
            {
                return;
            }

            Developer developerChoice = new Developer();

            i = 1;

            foreach (Developer developer in _developerRepo.GetAllDevelopers())
            {
                if (i == numberChoice)
                {
                    developerChoice = developer;
                }

                i++;
            }

            foreach (DevTeam team in _devTeamRepo.GetAllTeams())
            {
                _devTeamRepo.RemoveDeveloperFromTeam(developerChoice, team);
            }

            _developerRepo.RemoveDeveloper(developerChoice);
        }

        private void RemoveDevelopersFromDevTeam()
        {
            Console.Title = "Komodo Insurance - Remove Developers from Team";
            Console.Clear();
            int i = 1;

            foreach (DevTeam team in _devTeamRepo.GetAllTeams())
            {
                Console.WriteLine($"{i}. {team.Name}");
                i++;
            }

            Console.WriteLine("0. Cancel");
            Console.WriteLine("\nWhich team are you trying to remove developers from?");
            int numberChoice = int.Parse(Console.ReadLine());

            if (numberChoice == 0)
            {
                return;
            }

            DevTeam teamChoice = new DevTeam();

            i = 1;

            foreach (DevTeam team in _devTeamRepo.GetAllTeams())
            {
                if (i == numberChoice)
                {
                    teamChoice = team;
                }

                i++;
            }

            while (true)
            {
                Console.Clear();
                i = 1;

                foreach (Developer developer in teamChoice._members)
                {
                    Console.WriteLine($"{i}. {developer.Name}");
                    i++;
                }

                Console.WriteLine("0. Cancel");
                Console.WriteLine($"\nWhich developer are you trying to remove from \"{teamChoice.Name}\"?");
                numberChoice = int.Parse(Console.ReadLine());

                if (numberChoice == 0)
                {
                    return;
                }

                Developer developerChoice = new Developer();

                i = 1;

                foreach (Developer developer in teamChoice._members)
                {
                    if (i == numberChoice)
                    {
                        developerChoice = developer;
                    }

                    i++;
                }

                bool success = _devTeamRepo.RemoveDeveloperFromTeam(developerChoice, teamChoice);
                Console.Clear();

                if (success)
                {
                    Console.WriteLine($"{developerChoice.Name} has been removed from team: {teamChoice.Name}");
                }
                else
                {
                    Console.WriteLine($"Failed to remove {developerChoice.Name} from team: {teamChoice.Name}");
                }

                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
            }
        }

        private void SeedData()
        {
            Developer ethan = _developerRepo.CreateDeveloper("Ethan Richardson", false);
            Developer joe = _developerRepo.CreateDeveloper("Joe Rambo", false);
            Developer john = _developerRepo.CreateDeveloper("John Safari", true);

            DevTeam softwareDev = _devTeamRepo.CreateTeam("Software Development");
            DevTeam graphicDesign = _devTeamRepo.CreateTeam("Graphic Design");

            _devTeamRepo.AddDeveloperToTeam(ethan, softwareDev);
            _devTeamRepo.AddDeveloperToTeam(joe, graphicDesign);
            _devTeamRepo.AddDeveloperToTeam(john, softwareDev);
            _devTeamRepo.AddDeveloperToTeam(john, graphicDesign);
        }
    }
}
