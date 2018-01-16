using Abp.AutoMapper;
using Misitu.RevenueSources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Billing.Dto
{
    [AutoMapFrom(typeof(BillItem))]
    public class CreateBillItemInput
    {
        public virtual int BillId { get; set; }
        public virtual int ActivityId { get; set; }
        [Required]
        public virtual string Description { get; set; }
        public virtual double Loyality { get; set; }
        public virtual double TFF { get; set; }
        public virtual double LMDA { get; set; }
        public virtual double VAT { get; set; }
        public virtual double CESS { get; set; }
        public virtual double TP { get; set; }
        public virtual double DataSheet { get; set; }
        public virtual double Others { get; set; }
        public virtual double Total { get; set; }

        [ForeignKey("BillId")]
        public virtual Bill Bill { get; set; }
   
        [ForeignKey("RevenueResourceId")]
        public virtual RevenueSource RevenueResource { get; set; }
    }
}
