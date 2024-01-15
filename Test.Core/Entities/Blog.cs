using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Common;

namespace Test.Core.Entities
{
    public class Blog : BaseAuditableEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ImgUrl { get; set; }
    }
}
