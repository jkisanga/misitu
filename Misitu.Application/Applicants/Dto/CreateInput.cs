using Abp.AutoMapper;
using Misitu.FinancialYears;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Applicants.Dto
{
    [AutoMapFrom(typeof(Applicant))]
    class CreateInput
    {
        [Required]
        public virtual int Type { get; set; }
        [Required]
        public virtual string Name { get; set; }
        [Required]
        public virtual string Adress { get; set; }
        [Phone, Required]
        public virtual string Phone { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public virtual string Email { get; set; }
        [Required]
        public virtual bool IsTanzanian { get; set; }
        [Required]
        public virtual string IDtype { get; set; }
        //Replace DoB here fill ID number for Individual and Reg Number for Company
        [Required]
        public virtual string IDNumber { get; set; }
        //dropdown selection from reference table 
        public virtual string IDIssuePlace { get; set; }
        [Required]
        public virtual DateTime IDExpiryDate { get; set; }
        public virtual int FinancialYearId { get; set; }

        [ForeignKey("FinancialYearId")]
        public virtual FinancialYear FinancialYear { get; set; }
    }
}
