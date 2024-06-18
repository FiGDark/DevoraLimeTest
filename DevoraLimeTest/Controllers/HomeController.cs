using DevoraLimeTest.Data;
using DevoraLimeTest.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DevoraLimeTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private Context _dbContext;
        private Random _random = new Random();

        public HomeController(Context context, ILogger<HomeController> logger)
        {
            _logger = logger;
            _dbContext = context;
        }

        [HttpPost("RandomHeroGenerator")]
        public int RandomHeroGenerator(int NumOfHeroes)
        {
            var types = _dbContext.Types;
            Arena arena = new Arena();
            arena.Heroes = new List<Hero>();
            for (int i = 0; i < NumOfHeroes; i++)
            {
                int randvalue = _random.Next(1, 4);
                var type = types.Where(t => t.Id == randvalue).FirstOrDefault();
                arena.Heroes.Add(new Hero { TypeID = type.Id, HP = type.MaxHP });
            }
            _dbContext.Arenas.Add(arena);
            _dbContext.SaveChanges();
            return arena.Id;
        }

        [HttpPost("War")]
        public IActionResult War(int arenaID)
        {
            History history = new History { ArenaID = arenaID};
            history.Fights = new List<Fight>();
            List<Hero> alive = _dbContext.Heroes.Where(h => h.ArenaID == arenaID).Include(h => h.Type).ToList();
            while (alive.Count > 1)
            {
                history.NumOfFights++;
                int attackerpos = _random.Next(0, alive.Count);
                Hero attacker = alive.ElementAt(attackerpos);
                alive.Remove(attacker);
                int deffenderpos = _random.Next(0, alive.Count);
                Hero deffender = alive.ElementAt(deffenderpos);
                alive.Remove(deffender);
                history.Fights.Add(Fight(ref attacker, ref deffender));
                Heal(ref alive);


                if (attacker.HP > 0)
                    alive.Add(attacker);
                if (deffender.HP > 0)
                    alive.Add(deffender);

            }
            _dbContext.History.Add(history);
            _dbContext.SaveChanges();
            return Ok(history);
        }

        private Fight Fight(ref Hero attacker, ref Hero deffender)
        {
            Fight fight = new Fight();

            fight.Attacker = attacker;
            fight.AttackerID = attacker.Id;
            fight.AttackerStartHP = attacker.HP;
            fight.Deffender = deffender;
            fight.DeffenderID = deffender.Id;
            fight.DeffenderStartHP = deffender.HP;

            attacker.HP /= 2;
            deffender.HP /= 2;
            if (attacker.HP < attacker.Type.MaxHP / 4)
                attacker.HP = 0;

            if (deffender.HP < deffender.Type.MaxHP / 4)
                deffender.HP = 0;


            if (attacker.HP > 0 && deffender.HP > 0)
                Combat(ref attacker, ref deffender);

            fight.AttackerEndHP = attacker.HP;
            fight.DeffenderEndHP = deffender.HP;

            return fight;
        }
        private void Combat(ref Hero attacker, ref Hero deffender)
        {
            double margin = 40.0 / 100.0;
            switch (attacker.Type.Name)
            {
                case "Archer":
                    if(deffender.Type.Name == "Knight")
                    {
                        bool result = _random.NextDouble() <= margin? true : false;
                        if (result)
                        {
                            deffender.HP = 0;
                        }
                    }
                    else if (deffender.Type.Name == "SwordsMan" || deffender.Type.Name == "Archer")
                    {
                        deffender.HP = 0;
                    }
                    break;
                case "SwordsMan":
                    if (deffender.Type.Name == "SwordsMan" || deffender.Type.Name == "Archer")
                    {
                        deffender.HP = 0;
                    }
                    break;
                case "Knight":
                    if (deffender.Type.Name == "Knight" || deffender.Type.Name == "Archer")
                    {
                        deffender.HP = 0;
                    }
                    else if (deffender.Type.Name == "SwordsMan")
                    {
                        attacker.HP = 0;
                    }
                    break;
                default:
                    break;
            }
        }

        private void Heal(ref List<Hero> heroes)
        {
            foreach (Hero hero in heroes)
            {
                hero.HP += 10;
                hero.HP = hero.HP > hero.Type.MaxHP ? hero.Type.MaxHP : hero.HP;
            }
        }
    }
}
