using Misitu.Applicants.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misitu.Applicants.Dto;
using Abp.Domain.Repositories;
using Misitu.RefereneceTables;
using Abp.AutoMapper;

namespace Misitu.Applicants.Services
{
    public class ApplicationTypeService : IApplicationTypeService
    {
        private readonly IRepository<RefApplicantType> _repositoryApplicantType;

        public ApplicationTypeService(IRepository<RefApplicantType> repositoryApplicantType)
        {
            _repositoryApplicantType = repositoryApplicantType;
        }

        public List<ApplicationTypeDto> GetRefApplicationTypes()
        {
            var refApplicationTypes = _repositoryApplicantType
                 .GetAll()
                 .OrderBy(p => p.Name)
                 .ToList();

            return new List<ApplicationTypeDto>(refApplicationTypes.MapTo<List<ApplicationTypeDto>>());
        }
    }
}
