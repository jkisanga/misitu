using Abp.AutoMapper;
using Misitu.TransitPasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.POSUser.Dto
{
    [AutoMapFrom(typeof(CheckpointUser))]
    public class CreateCheckpointUser
    {

        public int UserId { get; set; }
        public int CheckpointId { get; set; }
        public string CheckpointName { get; set; }
        public string OfficerName { get; set; }
        public string POSId { get; set; }
        public bool IsActive { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Addtioninfo { get; set; }

       
    }
}
