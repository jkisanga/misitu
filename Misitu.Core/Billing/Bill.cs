using Abp.Domain.Entities.Auditing;
using Misitu.FinancialYears;
using Misitu.Registration;
using Misitu.Stations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Billing
{
    [Table("Bills")]
    public class Bill:FullAuditedEntity
    {
        public Bill()
        {
            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[12];
                rng.GetBytes(tokenData);              
                ControlNumber = Convert.ToString(BitConverter.ToUInt32(tokenData, 0));
            }
            
        }

       
        public virtual int DealerId { get; set; }
        public virtual int StationId { get; set; }
        public virtual int FinancialYearId { get; set; }
        public virtual DateTime IssuedDate { get; set; }
        public virtual DateTime ExpiredDate { get; set; }
        public virtual string ControlNumber { get; set; }
        public virtual Double BillAmount { get; set; }
        public virtual Double PaidAmount { get; set; }
        public virtual string Currency { get; set; }
        public virtual DateTime? PaidDate { get; set; }
        public virtual Boolean IsCanceled { get; set; }
        public virtual string Description { get; set; }
        public virtual string Reason { get; set; }

        [ForeignKey("DealerId")]
        public virtual Dealer Dealer { get; set; }
        [ForeignKey("StationId")]
        public virtual Statiton Station { get; set; }
        [ForeignKey("FinancialYearId")]
        public virtual FinancialYear FinancialYear { get; set; }
    }
}
