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
    [Route("api/users")]
    public class UserController : BaseController<User>
    {
        public UserController(IDataService<User> dataService) : base(dataService, Config.UserRoute, Config.UsersRoute)
        {
        }

        
        [HttpGet(Name = Config.UsersRoute)]
        public IActionResult Get(int page = 0, int pagesize = Config.DefaultPageSize)
        {
            var userList = DataService.GetList(page, pagesize)
                .Select(u => ModelFactory.Map(u, Url));
            var total = DataService.Count();

            var result = new
            {
                total = total,
                prev = GetPrevUrl(Url, page, pagesize),
                next = GetNextUrl(Url, page, pagesize, total),
                userList = userList
            };

            return Ok(result);
        }




        
        [HttpGet("{id}", Name = Config.UserRoute)]
        public IActionResult Get(int id)
        {
            var user = DataService.Get(id);
            if (user == null) return NotFound();
            return Ok(ModelFactory.Map(user, Url));
        }

        
        [HttpPost]
        public IActionResult Post([FromBody] UserModel model)
        {
            var user = ModelFactory.Map(model);
            DataService.Add(user);
            return Ok(ModelFactory.Map(user, Url));
        }

        
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserModel model)
        {
            var user = ModelFactory.Map(model);
            user.UserId = id;
            if (!DataService.Update(user))
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
