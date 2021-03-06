﻿using Abp.Application.Services;
using Misitu.RevenueSources.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.RevenueSources
{
   public interface IMainRevenueSuorce : IApplicationService
    {
        List<MainRevenueSourceDto> GetMainRevenueSuorces();

        int CreateMainRevenueSource(CreateMainRevenueSource input);

        MainRevenueSourceDto GetMainRevenueSource(int id);

        Task UpdateMainRevenueSource(MainRevenueSourceDto input);

        Task DeleteMainRevenueSourceAsync(MainRevenueSourceDto input);
    }
}
