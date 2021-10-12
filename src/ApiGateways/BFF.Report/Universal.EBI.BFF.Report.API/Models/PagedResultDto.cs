﻿using System.Collections.Generic;

namespace Universal.EBI.BFF.Report.API.Models
{
    public class PagedResultDto<T> where T : class
    {
        public IEnumerable<T> List { get; set; }
        public int TotalResults { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Query { get; set; }
    }
}
