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
    [Route("api/comments")]
    public class CommentController : BaseController<Comment>
    {
        public CommentController(IDataService<Comment> dataService) : base(dataService, Config.CommentRoute, Config.CommentsRoute)
        {
        }

        
        [HttpGet(Name = Config.CommentsRoute)]
        public IActionResult Get(int page = 0, int pagesize = Config.DefaultPageSize)
        {
            var commentList = DataService.GetList(page, pagesize)
                .Select(a => ModelFactory.Map(a, Url));
            var total = DataService.Count();

            var result = new
            {
                total = total,
                prev = GetPrevUrl(Url, page, pagesize),
                next = GetNextUrl(Url, page, pagesize, total),
                commentList = commentList
            };

            return Ok(result);
        }




        
        [HttpGet("{id}", Name = Config.CommentRoute)]
        public IActionResult Get(int id)
        {
            var comment = DataService.Get(id);
            if (comment == null) return NotFound();
            return Ok(ModelFactory.Map(comment, Url));
        }

        
        [HttpPost]
        public IActionResult Post([FromBody] CommentModel model)
        {
            var comment = ModelFactory.Map(model);
            DataService.Add(comment);
            return Ok(ModelFactory.Map(comment, Url));
        }

        
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CommentModel model)
        {
            var comment = ModelFactory.Map(model);
            comment.CommentId = id;
            if (!DataService.Update(comment))
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
