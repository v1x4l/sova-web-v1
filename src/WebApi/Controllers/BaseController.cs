using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseService;
using Microsoft.AspNetCore.Mvc;
using DomainModel;

namespace WebApi.Controllers
{
    public class BaseController<T> : Controller
    {
        public IDataService<T> DataService { get; }
        public string valueRoute;
        public string valuesRoute;

        public BaseController(IDataService<T> dataService, string valueRoute, string valuesRoute)
        {
            DataService = dataService;
            this.valueRoute = valueRoute;
            this.valuesRoute = valuesRoute;
        }

        protected bool IsLastPage(int page, int pagesize, int total)
        {
            if (total - page * pagesize > 0)
            {
                return false;
            }
            return true;
        }

        protected static bool IsFirstPage(int page)
        {
            return page == 0;
        }

        protected string GetNextUrl(IUrlHelper url, int page, int pagesize, int total)
        {
            if (IsLastPage(page, pagesize, total)) return null;
            return url.Link(valuesRoute, new { page = page + 1, pagesize });
        }
        protected string GetPrevUrl(IUrlHelper url, int page, int pagesize)
        {
            if (IsFirstPage(page)) return null;
            return url.Link(valuesRoute, new { page = page - 1, pagesize });
        }
    }
}
