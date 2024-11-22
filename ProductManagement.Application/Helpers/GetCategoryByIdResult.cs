using ProductManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Helpers
{
    public class GetCategoryByIdResult
    {
        public bool CategoryExists { get; set; }
        public Category Category { get; set; }
    }
}
