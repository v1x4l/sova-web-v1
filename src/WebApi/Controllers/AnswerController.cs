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
    [Route("api/answers")]
    public class AnswerController : BaseController<Answer>
    {
        public AnswerController(IDataService<Answer> dataService) : base(dataService, Config.AnswerRoute, Config.AnswersRoute)
        {
        }
        
        
        [HttpGet(Name = Config.AnswersRoute)]
        public IActionResult Get(int page = 0, int pagesize = Config.DefaultPageSize)
        {
            var answerList = DataService.GetList(page, pagesize)
                .Select(a => ModelFactory.Map(a, Url));
            var total = DataService.Count();

            var result = new
            {
                total = total,
                prev = GetPrevUrl(Url, page, pagesize),
                next = GetNextUrl(Url, page, pagesize, total),
                answerList = answerList
            };

            return Ok(result);
        }




        
        [HttpGet("{id}", Name = Config.AnswerRoute)]
        public IActionResult Get(int id)
        {
            var answer = DataService.Get(id);
            if (answer == null) return NotFound();
            return Ok(ModelFactory.Map(answer, Url));
        }

        
        [HttpPost]
        public IActionResult Post([FromBody] AnswerModel model)
        {
            var answer = ModelFactory.Map(model);
            DataService.Add(answer);
            return Ok(ModelFactory.Map(answer, Url));
        }

        
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] AnswerModel model)
        {
            var answer = ModelFactory.Map(model);
            answer.AnswerId = id;
            if (!DataService.Update(answer))
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
