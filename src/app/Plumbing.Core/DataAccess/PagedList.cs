using System.Collections.Generic;
using System.Linq;

namespace Plumbing.DataAccess
{
    public class PagedList<T>
    {
        readonly IQueryable<PagedQuery<T>> pagedQuery;

        public PagedList(IOrderedQueryable<T> query, int page, int pageSize)
        {
            var skip = (page - 1)*pageSize;
            pagedQuery = from p in query
                         select new PagedQuery<T>
                         {
                             Count = query.Count(),
                             Records = query.Skip(skip).Take(pageSize)
                         };
        }

        int? count;

        public int Count
        {
            get
            {
                if (count == null) LoadData();
                return count.Value;
            }
        }

        List<T> records;

        public List<T> Records
        {
            get
            {
                if (records == null) LoadData();
                return records;
            }
        }

        void LoadData()
        {
            var page = pagedQuery.First();
            count = page.Count;
            records = page.Records.ToList();
        }
    }

    public class PagedQuery<T>
    {
        public int Count { get; set; }
        public IQueryable<T> Records { get; set; }
    }
}