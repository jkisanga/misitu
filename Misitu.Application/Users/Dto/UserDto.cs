using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Misitu.Stations.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Users.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserDto: FullAuditedEntityDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public bool IsActive { get; set; }
        public int StationId { get; set; }

        public string[] Roles { get; set; }


    }
}
