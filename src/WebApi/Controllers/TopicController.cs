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
    [Route("api/topics")]
    public class TopicController : BaseController<Topic>
    {
        public TopicController(IDataService<Topic> dataService) : base(dataService, Config.TopicRoute, Config.TopicsRoute)
        {
        }

        
        [HttpGet(Name = Config.TopicsRoute)]
        public IActionResult Get(int page = 0, int pagesize = Config.DefaultPageSize)
        {
            var topicList = DataService.GetList(page, pagesize)
                .Select(a => ModelFactory.Map(a, Url));
            var total = DataService.Count();

            var result = new
            {
                total = total,
                prev = GetPrevUrl(Url, page, pagesize),
                next = GetNextUrl(Url, page, pagesize, total),
                topicList = topicList
            };

            return Ok(result);
        }




        
        [HttpGet("{id}", Name = Config.TopicRoute)]
        public IActionResult Get(int id)
        {
            var topic = DataService.Get(id);
            if (topic == null) return NotFound();
            return Ok(ModelFactory.Map(topic, Url));
        }

        
        [HttpPost]
        public IActionResult Post([FromBody] TopicModel model)
        {
            var topic = ModelFactory.Map(model);
            DataService.Add(topic);
            return Ok(ModelFactory.Map(topic, Url));
        }

        
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TopicModel model)
        {
            var topic = ModelFactory.Map(model);
            topic.TopicId = id;
            if (!DataService.Update(topic))
            {
                return NotFound();
            }
            return Ok();
        }

        
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
