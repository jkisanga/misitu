﻿using Abp.AutoMapper;
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
   public class CreateInput
    {
        [Required]
        public virtual int Type { get; set; }
        [Required]
        public virtual string Name { get; set; }
        [Required]
        public virtual string Adress { get; set; }
        [Required]
        public virtual string Phone { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public virtual string Email { get; set; }

        public virtual string TIN { get; set; }

        public virtual int FinancialYearId { get; set; }

        public virtual bool IsTanzanian { get; set; }

        public virtual string IDtype { get; set; }

        //Replace DoB here fill ID number for Individual and Reg Number for Company

        public virtual string IDNumber { get; set; }

        public virtual string IDIssuePlace { get; set; }

        public virtual DateTime? IDExpiryDate { get; set; }

        [ForeignKey("FinancialYearId")]
        public virtual FinancialYear FinancialYear { get; set; }
    }
}
