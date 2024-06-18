using DevoraLimeTest.Data;
using Microsoft.AspNetCore.Mvc;

namespace DevoraLimeTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private Context _dbContext;

        public HomeController(Context context,ILogger<HomeController> logger)
        {
            _logger = logger;
            _dbContext = context;
        }

        [HttpPost(Name = "RandomHeroGenerator")]
        public int RandomHeroGenerator(int NumOfHeroes)
        {

            return 1;
        }
    }
}
