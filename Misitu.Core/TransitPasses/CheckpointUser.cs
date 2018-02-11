using Abp.Domain.Entities.Auditing;
using Misitu.Stations;
using Misitu.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.TransitPasses
{
 public  class CheckpointUser : FullAuditedEntity
    {
        public CheckpointUser()
        {
            IsActive = true;
        }
        public int UserId { get; set; }
        public int CheckpointId { get; set; }
        public string CheckpointName { get; set; }
        public string OfficerName { get; set; }
        public string POSId { get; set; }
        public bool IsActive { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Addtioninfo { get; set; }

        [ForeignKey("CheckpointId")]
        public Statiton Statiton { get; set; }
    }
}
