using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Business.ViewModels.Common
{
    public record BaseAuditableEntityVm : BaseEntityVm
    {
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
