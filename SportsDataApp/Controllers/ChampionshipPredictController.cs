using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsDataApp.Data;
using SportsDataApp.Models;

namespace SportsDataApp.Controllers
{
    public class ChampionshipPredictController : Controller
    {
        private readonly SportsDataAppContext _context;

        public ChampionshipPredictController(SportsDataAppContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var baseballSeasons = new List<Season>();
            var footballSeasons = new List<Season>();

            int? sportId = _context.Sport.
                    Where(sport => sport.Name == "MLB").
                    Select(sport => sport.Id).
                    FirstOrDefault();
            baseballSeasons = await _context.Season
           .Where(s => s.Team.SportId == sportId)
           .ToListAsync();
            string[][] data = new string[baseballSeasons.Count][];
            for (var i = 0; i < baseballSeasons.Count; i++)
            {
                var winPercent = (float)baseballSeasons[i].wins / ((float)baseballSeasons[i].wins + (float)baseballSeasons[i].losses);
                data[i] = new string[] { winPercent.ToString(), baseballSeasons[i].outcome };
            }
            ViewBag.Data = data;

            return View();
        }
    }
}
