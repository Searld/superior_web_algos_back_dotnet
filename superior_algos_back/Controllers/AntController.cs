using Microsoft.AspNetCore.Mvc;
using superior_algos_back.AntAlgo;

namespace superior_algos_back.Controllers
{
    [Route("[controller]")]
    public class AntController : Controller
    {
        [HttpPost]
        public IActionResult GetRoute([FromBody]List<List<EdgeDTO>> edges)
        {
            AntAlgorithm antAlgorithm = new AntAlgorithm();
            return Ok(antAlgorithm.GetRoutes(edges));
        }
    }
}
