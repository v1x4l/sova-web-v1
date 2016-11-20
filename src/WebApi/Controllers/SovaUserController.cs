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
    [Route("api/sovaUsers")]
    public class SovaUserController : BaseController<SovaUser>
    {
        public SovaUserController(IDataService<SovaUser> dataService) : base(dataService, Config.SovaUserRoute, Config.SovaUsersRoute)
        {
        }

        // GET api/values
        [HttpGet(Name = Config.SovaUsersRoute)]
        public IActionResult Get(int page = 0, int pagesize = Config.DefaultPageSize)
        {
            var sovaUserList = DataService.GetList(page, pagesize)
                .Select(u => ModelFactory.Map(u, Url));
            var total = DataService.Count();

            var result = new
            {
                total = total,
                prev = GetPrevUrl(Url, page, pagesize),
                next = GetNextUrl(Url, page, pagesize, total),
                sovaUserList = sovaUserList
            };

            return Ok(result);
        }




        // GET api/values/5
        [HttpGet("{id}", Name = Config.SovaUserRoute)]
        public IActionResult Get(int id)
        {
            var sovaUser = DataService.Get(id);
            if (sovaUser == null) return NotFound();
            return Ok(ModelFactory.Map(sovaUser, Url));
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] SovaUserModel model)
        {
            var sovaUser = ModelFactory.Map(model);
            DataService.Add(sovaUser);
            return Ok(ModelFactory.Map(sovaUser, Url));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] SovaUserModel model)
        {
            var sovaUser = ModelFactory.Map(model);
            sovaUser.SovaUserId = id;
            if (!DataService.Update(sovaUser))
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
