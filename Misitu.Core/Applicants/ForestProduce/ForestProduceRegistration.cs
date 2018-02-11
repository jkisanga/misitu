﻿using Abp.Domain.Entities.Auditing;
using Misitu.FinancialYears;
using Misitu.Regions;
using Misitu.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Applicants
{
   public class ForestProduceRegistration : FullAuditedEntity
    {
     
        //A: MASHARTI  KWA MWOMBAJI
        [Required]
        public virtual string RequirementTitle { get; set; }
        [Required]
        public virtual string RequirementDescription { get; set; }

        //B :MAELEZO BINAFSI / KAMPUNI/ KIKUNDI
        public virtual int ApplicantId { get; set; }
        public virtual int FinancialYearId { get; set; }
        public virtual int DistrictId { get; set; }
        public virtual bool HasForestBusinessLicense { get; set; }
        public virtual string TIN { get; set; }
        public virtual string BussinessLicenseNo { get; set; }
        public virtual string RegNumber { get; set; }

        //C: MAELEZO YA VITENDEA KAZI
        public virtual bool HasSawmill { get; set; }
        public virtual bool IsSawmillInstalled { get; set; }
        public virtual string SawmillInstalledLocation { get; set; }
        public virtual string  InstallationPermitNo { get; set; }
        public virtual string TypeOfSawmill { get; set; }
        public virtual string SawmillCapacityPerYear { get; set; }

        public virtual bool HasTrucks { get; set; }
        public virtual string TrucksOwnerAttachment { get; set; }
        public virtual bool SawmillHasLoadingBench { get; set; }
        public virtual bool SawmillHasBreakDownSaw { get; set; }
        public virtual bool SawmillHasReceivingBench { get; set; }

        //D: MAELEZO YA WATAALAMU WA KIWANDA NA MAHALI UNAPOOMBA KWENDA KUVUNA
        // uchaguzi wa maombi uchaguzi wa mashamba unaenda kwenye table ForestProduceAppliedForest
        public virtual bool HasChainSawTechnician { get; set; }
        public virtual bool HasTrainedOperator { get; set; }
        public virtual string OperatorInstitutionName { get; set; }
        public virtual string OperatorCertificateAttachment { get; set; }
        public virtual bool HasSawmillOperator { get; set; }

        //F: VIAMBATISHO NA MUDA WA KEREJESHA FOMU
        public virtual string TaxClearance { get; set; }
        public virtual string CertifiedAudted { get; set; }


        //Upande wa Afisa wa TFS
      
        public virtual bool CertificatePrinted { get; set; }
      
        public virtual string Status { get; set; }
        public virtual string Resoan { get; set; }

        public virtual bool IsSubmitted { get; set; }
        public virtual bool IsApproved { get; set; }
        public virtual bool IsRejected { get; set; }
        public virtual int? ApprovedUserId { get; set; }
        public virtual string Remark { get; set; }


        [ForeignKey("ApplicantId")]
        public virtual Applicant Applicant { get; set; }
        [ForeignKey("FinancialYearId")]
        public virtual FinancialYear FinancialYear { get; set; }
        [ForeignKey("DistrictId")]
        public virtual District District { get; set; }
       
    }
}
