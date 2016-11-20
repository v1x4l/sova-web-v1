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
    [Route("api/questions")]
    public class QuestionController : BaseController<Question>
    {
        public QuestionController(IDataService<Question> dataService) : base(dataService, Config.QuestionRoute, Config.QuestionsRoute)
        {
        }

        // GET api/values
        [HttpGet(Name = Config.QuestionsRoute)]
        public IActionResult Get(int page = 0, int pagesize = Config.DefaultPageSize)
        {
            var questionList = DataService.GetList(page, pagesize)
                .Select(a => ModelFactory.Map(a, Url));
            var total = DataService.Count();

            var result = new
            {
                total = total,
                prev = GetPrevUrl(Url, page, pagesize),
                next = GetNextUrl(Url, page, pagesize, total),
                questionList = questionList
            };

            return Ok(result);
        }




        // GET api/values/5
        [HttpGet("{id}", Name = Config.QuestionRoute)]
        public IActionResult Get(int id)
        {
            var question = DataService.Get(id);
            if (question == null) return NotFound();
            return Ok(ModelFactory.Map(question, Url));
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] QuestionModel model)
        {
            var question = ModelFactory.Map(model);
            DataService.Add(question);
            return Ok(ModelFactory.Map(question, Url));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] QuestionModel model)
        {
            var question = ModelFactory.Map(model);
            question.QuestionId = id;
            if (!DataService.Update(question))
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
