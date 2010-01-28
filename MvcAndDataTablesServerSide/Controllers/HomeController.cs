using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace MvcAndDataTablesServerSide.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult GetPersons(int iDisplayStart, int iDisplayLength, 
            string sSearch, bool bEscapeRegex, int iColumns,
            int iSortingCols, int iSortCol_0, string sSortDir_0,
            int sEcho)
        {

            var list = GetPersons();

            var filteredlist =
                list
                    .Select(x => new[] {x.Name, x.Number.ToString(), x.Date.ToShortDateString()})
                    .Where(x => string.IsNullOrEmpty(sSearch) || x.Any(y => y.IndexOf(sSearch, StringComparison.InvariantCultureIgnoreCase) >= 0));
            
            var orderedlist = filteredlist
                    .OrderByWithDirection(x => (x[iSortCol_0]).Parse(), sSortDir_0 == "desc")
                    .Skip(iDisplayStart)
                    .Take(iDisplayLength);

            var model = new
                            {
                                aaData = orderedlist,
                                iTotalDisplayRecords = filteredlist.Count(),
                                iTotalRecords = list.Count(),
                                sEcho = sEcho.ToString()
                            };
            
            return Json(model);
        }

        private IEnumerable<Person> GetPersons()
        {
            var list = new List<Person>();
            for (int i = 0; i < 10000; i++)
                list.Add(new Person { Name = "Gaute" + i, Number = i, Date = DateTime.Now.AddDays(-i) });
            return list;
        }
    }

    internal class Person   
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }
    }

    public static class QueryHelpers
    {
        public static IOrderedEnumerable<TSource> OrderByWithDirection<TSource, TKey>
            (this IEnumerable<TSource> source,
             Func<TSource, TKey> keySelector,
             bool descending)
        {
            return descending
                       ? source.OrderByDescending(keySelector)
                       : source.OrderBy(keySelector);
        }

        public static IOrderedQueryable<TSource> OrderByWithDirection<TSource, TKey>
            (this IQueryable<TSource> source,
             Expression<Func<TSource, TKey>> keySelector,
             bool descending)
        {
            return descending ? source.OrderByDescending(keySelector)
                              : source.OrderBy(keySelector);
        }
        public static object Parse(this string s)
        {
            int i;
            DateTime d;
            if (int.TryParse(s, out i))
                return i;
            if (DateTime.TryParse(s, out d))
                return d;
            return s;
        }
        
    }
}
