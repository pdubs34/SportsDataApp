using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportsDataApp.Data;
using SportsDataApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using SportsDataApp.Data;
using SportsDataApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using System.Security.Permissions;

namespace SportsDataApp.Controllers
{
    public class InsertDataFromCSVController : Controller
    {
        private readonly SportsDataAppContext _context;

        public InsertDataFromCSVController(SportsDataAppContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> AddSports()
        {
            // Create a list of Sport objects to add to the database
            var sportsToAdd = new List<Sport>
            {
                new Sport { Name = "MLB" },
                new Sport { Name = "NFL" },
                new Sport { Name = "NBA" },
                new Sport { Name = "NCAA Volleyball"}
                // Add more sports as needed
            };

            // Add the sports to the database context
            _context.Sport.AddRange(sportsToAdd);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Redirect to the Index action of the SportsController
            return RedirectToAction("Index", "Sports");
        }

        public async Task<IActionResult> AddTeams()
        {
            var teamToAdd = new List<Team> { };
            var path = @"./PulledData/MLBData/teams.csv"; 
            using (TextFieldParser csvParser = new TextFieldParser(path))
            {
                csvParser.SetDelimiters(new string[] { "," });
                string dataSport = csvParser.ReadLine();
                int? sportId = _context.Sport.
                    Where(sport => sport.Name == dataSport).
                    Select(sport => sport.Id).
                    FirstOrDefault();

                while (!csvParser.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.
                    string[] fields = csvParser.ReadFields();
                    string Abbrev = fields[0];
                    string TName = fields[1];
                    if(sportId != null)
                    {
                        teamToAdd.Add(new Team { Name = TName, Abbreviation = Abbrev, SportId = sportId.Value});
                    }
                }
            }
            var path2 = @"./PulledData/NFLData/NFLteams.csv";
            using (TextFieldParser csvParser = new TextFieldParser(path2))
            {
                csvParser.SetDelimiters(new string[] { "," });
                string dataSport = csvParser.ReadLine();
                int? sportId = _context.Sport.
                    Where(sport => sport.Name == dataSport).
                    Select(sport => sport.Id).
                    FirstOrDefault();

                while (!csvParser.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.
                    string[] fields = csvParser.ReadFields();
                    string Abbrev = fields[0];
                    string TName = fields[1];
                    if (sportId != null)
                    {
                        teamToAdd.Add(new Team { Name = TName, Abbreviation = Abbrev, SportId = sportId.Value });
                    }
                }
            }
            _context.Team.AddRange(teamToAdd);

            // Save changes to the database
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Teams");
        }

        public async Task<IActionResult> AddSeasons()
        {
            // Create a list of Sport objects to add to the database
            var seasonToAdd = new List<Season> { };
            var path = @"./PulledData/MLBData/seasons.csv";
            using (TextFieldParser csvParser = new TextFieldParser(path))
            {
                csvParser.SetDelimiters(new string[] { "," });

                while (!csvParser.EndOfData)
                {
                    // Read current line fields, pointer moves to the next 
                    string[] fields = csvParser.ReadFields();
                    if(fields.Length > 1)
                    {
                        int year = int.Parse(fields[0]);
                        string Abbrev = fields[1];
                        int Wins = int.Parse(fields[2]);
                        int Losses = int.Parse(fields[3]);
                        double winPercent = double.Parse(fields[4]);
                        string Outcome = fields[5];
                        int? teamId = _context.Team
                            .Where(team => team.Abbreviation == Abbrev)
                            .Select(team => team.Id)
                            .FirstOrDefault();
                        if (teamId != null)
                        {
                            seasonToAdd.Add(new Season { year = year, wins = Wins, losses = Losses, winPercent = winPercent,outcome = Outcome, TeamId = teamId.Value });
                        }
                    }
                   
                }
            }
            _context.Season.AddRange(seasonToAdd);

            // Save changes to the database
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Seasons");
        }
    }
}

