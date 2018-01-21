using Misitu.FinancialYears;
using Misitu.Registration;
using Misitu.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Misitu.Web.Areas.Client.Helpers
{
    public  class ApplicantConstrait
    {
       private static IDealerAppService _dealerAppService;
       private static IFinancialYearAppService _financialYearAppService;
       private static IUserAppService _userAppService;
       private static UserManager _userManager;

        public ApplicantConstrait(IDealerAppService dealerAppService,
             IUserAppService userAppService,
             UserManager userManager,
            IFinancialYearAppService financialYearAppService)
        {
            _dealerAppService = dealerAppService;
            _financialYearAppService = financialYearAppService;
            _userAppService = userAppService;
            _userManager = userManager;
        }

        public static bool IsRegistered(int UserId)
        {

            //var user = _userManager.Users.FirstOrDefault(x => x.Id == UserId);
           // var user1 = _userAppService.Get(UserId);            

            if (_dealerAppService.IsRegistered(9, _financialYearAppService.GetActiveFinancialYear()))
            {
                return true;
            }else
            {
                return false;
            }
        }

    }
}