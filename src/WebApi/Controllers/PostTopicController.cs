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
    [Route("api/postTopics")]
    public class PostTopicController : BaseController<PostTopic>
    {
        public PostTopicController(IDataService<PostTopic> dataService) : base(dataService, Config.PostTopicRoute, Config.PostTopicsRoute)
        {
        }

        // GET api/values
        [HttpGet(Name = Config.PostTopicsRoute)]
        public IActionResult Get(int page = 0, int pagesize = Config.DefaultPageSize)
        {
            var postTopicList = DataService.GetList(page, pagesize)
                .Select(a => ModelFactory.Map(a, Url));
            var total = DataService.Count();

            var result = new
            {
                total = total,
                prev = GetPrevUrl(Url, page, pagesize),
                next = GetNextUrl(Url, page, pagesize, total),
                postTopicList = postTopicList
            };

            return Ok(result);
        }




        // GET api/values/5
        [HttpGet("{id}", Name = Config.PostTopicRoute)]
        public IActionResult Get(int id)
        {
            var postTopic = DataService.Get(id);
            if (postTopic == null) return NotFound();
            return Ok(ModelFactory.Map(postTopic, Url));
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] PostTopicModel model)
        {
            var postTopic = ModelFactory.Map(model);
            DataService.Add(postTopic);
            return Ok(ModelFactory.Map(postTopic, Url));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PostTopicModel model)
        {
            var postTopic = ModelFactory.Map(model);
            postTopic.IdPostTopic = id;
            if (!DataService.Update(postTopic))
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
