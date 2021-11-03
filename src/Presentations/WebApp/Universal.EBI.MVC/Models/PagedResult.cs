using System;
using System.Collections.Generic;
using Universal.EBI.MVC.Services.Interfaces;

namespace Universal.EBI.MVC.Models
{
    public class PagedResult<T> : IPagedList where T : class
    {
        public string ReferenceAction { get; set; }
        public IEnumerable<T> List { get; set; }
        public int TotalResults { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Query { get; set; }
        public double TotalPages => Math.Ceiling((double)TotalResults / PageSize);
    }
}
