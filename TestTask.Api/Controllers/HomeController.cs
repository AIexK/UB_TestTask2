using Microsoft.AspNetCore.Mvc;
using TestTask.Application.Common.Interfaces;
using TestTask.Domain;

namespace TestTask.Api.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : Controller
    {
        private readonly IEntityService _entityService;

        public HomeController(IEntityService entityService)
        {
            _entityService = entityService;
        }


        /// <summary>
        /// Add a new entity
        /// </summary>
        /// <remarks>
        /// Request example:
        ///
        /// POST http://localhost:5157?insert={"id":"cfaa0d3f-7fea-4423-9f69-ebff826e2f89","operationDate":"2019-04-02T13:10:20.0263632+03:00","amount":23.05}
        /// </remarks>
        /// <param name="insert">JSON data</param>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Insert([FromQuery] string insert)
        {
            _entityService.InsertEntity(insert);
            return Ok();
        }


        /// <summary>
        /// Get an entity by ID
        /// </summary>
        /// <remarks>
        /// Request example:
        ///
        /// GET http://localhost:5157?get=cfaa0d3f-7fea-4423-9f69-ebff826e2f89
        /// </remarks>
        /// <param name="get">GUID</param>
        [HttpGet]
        [ProducesResponseType(typeof(Entity), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Get([FromQuery] string get)
        {
            var entity = _entityService.GetEntity(Guid.Parse(get));
            return Ok(entity);
        }

    }
}
