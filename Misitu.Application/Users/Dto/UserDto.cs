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
    public class UserDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int StationId { get; set; }
    }
}
