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
    [Route("api/historys")]
    public class HistoryController : BaseController<History>
    {
        public HistoryController(IDataService<History> dataService) : base(dataService, Config.HistoryRoute, Config.HistorysRoute)
        {
        }

        // GET api/values
        [HttpGet(Name = Config.HistorysRoute)]
        public IActionResult Get(int page = 0, int pagesize = Config.DefaultPageSize)
        {
            var historyList = DataService.GetList(page, pagesize)
                .Select(a => ModelFactory.Map(a, Url));
            var total = DataService.Count();

            var result = new
            {
                total = total,
                prev = GetPrevUrl(Url, page, pagesize),
                next = GetNextUrl(Url, page, pagesize, total),
                historyList = historyList
            };

            return Ok(result);
        }




        // GET api/values/5
        [HttpGet("{id}", Name = Config.HistoryRoute)]
        public IActionResult Get(int id)
        {
            var history = DataService.Get(id);
            if (history == null) return NotFound();
            return Ok(ModelFactory.Map(history, Url));
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] HistoryModel model)
        {
            var history = ModelFactory.Map(model);
            DataService.Add(history);
            return Ok(ModelFactory.Map(history, Url));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] HistoryModel model)
        {
            var history = ModelFactory.Map(model);
            history.HistoryId = id;
            if (!DataService.Update(history))
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
