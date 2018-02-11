using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Billing.Dto
{
   public class BillItemModel
    {
        public BillDto Bill { get; set; }
        public ICollection<BillItemDto> Items { get; set; }
    }
}
