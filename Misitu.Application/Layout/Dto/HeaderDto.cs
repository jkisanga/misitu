using Misitu.FinancialYears.Dto;
using Misitu.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Layout.Dto
{
    public class HeaderDto
    {
        public int dealers { get; set; }
        public int licenses { get; set; }
        public int bills { get; set; }
        public FinancialYearDto financialYear { get; set; }
        public UserLoginDto UserInfo  { get; set; }
    }
}
