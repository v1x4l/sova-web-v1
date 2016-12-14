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
    [Route("api/searchResults")]
    public class SearchResultController : BaseController<SearchResult>
    {
        public SearchResultController(IDataService<SearchResult> dataService) : base(dataService, Config.SearchResultRoute, Config.SearchResultsRoute)
        {

        }

        [HttpGet("{search}", Name = Config.SearchResultsRoute)]
        public IActionResult Get(string search, int page = 0, int pagesize = Config.DefaultPageSize)
        {
            string w1, w2, w3;
            string[] searchWord = search.Split(new char[] { '_', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            int totalSearchWords = searchWord.Length;

            if (searchWord.Length < 1) {
                w1 = "";
                w2 = "";
                w3 = "";

            }
            else if (searchWord.Length < 2) {
                w1 = searchWord[0];
                w2 = "";
                w3 = "";
            }
            else if (searchWord.Length < 3) {
                w1 = searchWord[0];
                w2 = searchWord[1];
                w3 = "";
            }
            else {
                w1 = searchWord[0];
                w2 = searchWord[1];
                w3 = searchWord[2];
            }

            

            var searchResultList = DataService.GetProcedureList(page, pagesize, w1, w2, w3)
                .Select(u => ModelFactory.Map(u, Url));

            var total = searchResultList.Count();

            var result = new
            {
                total = total,
                prev = GetPrevUrl(Url, page, pagesize),
                next = GetNextUrl(Url, page, pagesize, total),
                searchResultList = searchResultList
            };

            return Ok(result);
        }

    }
}
