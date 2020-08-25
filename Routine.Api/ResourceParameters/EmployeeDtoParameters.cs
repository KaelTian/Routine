using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.ResourceParameters
{
    public class EmployeeDtoParameters
    {
        const int maxPageSize = 20;
        public string Gender { get; set; }

        public string Q { get; set; }

        public int PageNumber { get; set; } = 1;

        private int _pageSize = 5;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }

        public string OrderBy { get; set; } = "Name";
    }
}
