﻿using Abp.Domain.Entities.Auditing;
using Misitu.Billing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.TransitPasses
{
  public  class BillTransitPass : FullAuditedEntity
    {
      

        public virtual int TransitPassId { get; set; }
        public virtual int BillId { get; set; }
        public virtual string AdditionInformation { get; set; }
        

        [ForeignKey("TransitPassId")]
        public virtual TransitPass TransitPass { get; set; }
        [ForeignKey("BillId")]
        public virtual Bill Bill { get; set; }
        


    }
}
