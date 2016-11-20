using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseService;
using Microsoft.AspNetCore.Mvc;
using WebApi.JsonModels;
using DomainModel;

namespace WebApi.Controllers
{
    [Route("api/markeds")]
    public class MarkedController : BaseController<Marked>
    {
        public MarkedController(IDataService<Marked> dataService) : base(dataService, Config.MarkedRoute, Config.MarkedsRoute)
        {
        }

        // GET api/values
        [HttpGet(Name = Config.MarkedsRoute)]
        public IActionResult Get(int page = 0, int pagesize = Config.DefaultPageSize)
        {
            var markedList = DataService.GetList(page, pagesize)
                .Select(a => ModelFactory.Map(a, Url));
            var total = DataService.Count();

            var result = new
            {
                total = total,
                prev = GetPrevUrl(Url, page, pagesize),
                next = GetNextUrl(Url, page, pagesize, total),
                markedList = markedList
            };

            return Ok(result);
        }




        // GET api/values/5
        [HttpGet("{id}", Name = Config.MarkedRoute)]
        public IActionResult Get(int id)
        {
            var marked = DataService.Get(id);
            if (marked == null) return NotFound();
            return Ok(ModelFactory.Map(marked, Url));
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] MarkedModel model)
        {
            var marked = ModelFactory.Map(model);
            DataService.Add(marked);
            return Ok(ModelFactory.Map(marked, Url));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] MarkedModel model)
        {
            var marked = ModelFactory.Map(model);
            marked.MarkedId = id;
            if (!DataService.Update(marked))
            {
                return NotFound();
            }
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!DataService.Delete(id))
            {
                return NotFound();
            }

            return Ok();
        }



    }
}
