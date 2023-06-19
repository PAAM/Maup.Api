using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maup.Core.Entities
{
    public class PageList<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalRows { get; set; }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public int NextPageNumber => HasNextPage ? PageIndex += 1 : 0;
        public int PreviousPageNumber => HasPreviousPage ? PageIndex - 2 : 0;

        public PageList(List<T> items, int count, int pageNumber, int pageSize)
        {
            PageIndex = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            PageSize = pageSize;
            TotalRows = count;
            AddRange(items);
        }

        public static PageList<T> Create(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PageList<T>(items, count, pageNumber, pageSize);
        }

    }
}
