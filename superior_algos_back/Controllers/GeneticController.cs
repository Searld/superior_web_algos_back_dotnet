using Microsoft.AspNetCore.Mvc;
using superior_algos_back.GeneticAlgo;

namespace superior_algos_back.Controllers
{
    [Route("[controller]")]
    public class GeneticController : Controller
    {
        [HttpPost]
        public IActionResult GetRoute([FromBody]List<VertexDTO> source)
        {
            GeneticAlgorithm algorithm = new GeneticAlgorithm();
            var route = algorithm.GetRoute(VertexMapper.ToVertex(source));
            return Ok(route);
        }
    }
}
