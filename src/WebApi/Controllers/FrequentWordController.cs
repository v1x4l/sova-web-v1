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
    
    
    [Route("api/frequentwords")]
    public class FrequentWordController : BaseController<FrequentWord>
    {
        public FrequentWordController(IDataService<FrequentWord> dataService) : base(dataService, Config.SearchResultRoute, Config.SearchResultsRoute)
        {

        }

        [HttpGet("{search}&{qs}", Name = Config.FrequentWordsRoute)]
        public IActionResult Get(string search, bool qs, int page = 0, int pagesize = Config.DefaultPageSize)
        {
            bool questionSearch = qs;
            string w1, w2, w3;
            string[] searchWord = search.Split(new char[] { '_', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            int totalSearchWords = searchWord.Length;

            if (searchWord.Length < 1)
            {
                w1 = "";
                w2 = "";
                w3 = "";

            }
            else if (searchWord.Length < 2)
            {
                w1 = searchWord[0];
                w2 = "";
                w3 = "";
            }
            else if (searchWord.Length < 3)
            {
                w1 = searchWord[0];
                w2 = searchWord[1];
                w3 = "";
            }
            else
            {
                w1 = searchWord[0];
                w2 = searchWord[1];
                w3 = searchWord[2];
            }



            var frequentWordList = DataService.GetProcedureList(page, pagesize, w1, w2, w3, questionSearch)
                .Select(fw => ModelFactory.Map(fw, Url));

            var total = frequentWordList.Count();

            var result = new
            {
                total = total,
                prev = GetPrevUrl(Url, page, pagesize),
                next = GetNextUrl(Url, page, pagesize, total),
                frequentWordList = frequentWordList
            };

            return Ok(result);
        }
    }
    
}
