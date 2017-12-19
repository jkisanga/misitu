using Abp.AutoMapper;
using Misitu.RefereneceTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.RefTables.Dto
{
    [AutoMapFrom(typeof(RefServiceCategory))]
    class CreateRefServiceCategoryInput
    {
        [Required]
        public virtual string Name { get; set; }
        [Required]
        public virtual string Description { get; set; }
    }
}
