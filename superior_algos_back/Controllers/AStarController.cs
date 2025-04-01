using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using superior_algos_back.AStarAlgo;
using System.Text.Json;

namespace superior_algos_back.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class AStarController : ControllerBase
    {
        [HttpPost]
        public IActionResult FindRoute([FromBody]List<List<int>> field)
        {
            return Ok(AStar.FindRoute(field));
        }
    }
}
