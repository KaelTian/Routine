﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.Models
{
    public class CompanyFullDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Intruction { get; set; }

        public string Industry { get; set; }

        public string Product { get; set; }

        public string Country { get; set; }
    }
}
