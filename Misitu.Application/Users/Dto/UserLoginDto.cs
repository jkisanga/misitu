using Misitu.Stations.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Users.Dto
{
    public class UserLoginDto
    {
        public UserDto user { get; set; }

        public StationDto station { get; set; }
    }
}
