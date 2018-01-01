using Abp.Application.Services;
using Misitu.Applicants.Dto;
using Misitu.RefTables.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Applicants.Interface
{
  public  interface IApplicantService : IApplicationService
    {



        int CreateAsync(CreateInput input);

        ApplicantDto GetApplicantById(int id);



    }
}
