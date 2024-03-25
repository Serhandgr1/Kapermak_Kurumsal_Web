using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer
{
    public class RequestParameters
    {
        const int maxPageSize = 8;

        public int PageNumber { get; set; }

        private int _pageSize;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > maxPageSize ? maxPageSize : value; }
        }

        //public string? OrderBy { get; set; }
        //public string? Fields { get; set; }

    }
}
