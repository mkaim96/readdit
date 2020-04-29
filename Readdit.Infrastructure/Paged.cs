using System;
using System.Collections.Generic;
using System.Text;

namespace Readdit.Infrastructure
{
    public class Paged<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalNumberOfPages { get; set; }
        public int TotalNumberOfRecords { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
