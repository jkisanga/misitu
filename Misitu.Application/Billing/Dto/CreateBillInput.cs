﻿using Abp.AutoMapper;
using Misitu.FinancialYears;
using Misitu.Registration;
using Misitu.Stations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Billing.Dto
{
    [AutoMapFrom(typeof(Bill))]
    public class CreateBillInput
    {
        public virtual int DealerId { get; set; }
        public virtual int StationId { get; set; }
        public virtual int FinancialYearId { get; set; }
        public virtual Double BillAmount { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime IssuedDate { get; set; }
        public virtual DateTime ExpiredDate { get; set; }
        public virtual string Currency { get; set; }

        [ForeignKey("DealerId")]
        public virtual Dealer Dealer { get; set; }
        [ForeignKey("StationId")]
        public virtual Statiton Station { get; set; }
        [ForeignKey("FinancialYearId")]
        public virtual FinancialYear FinancialYear { get; set; }
    }
}
