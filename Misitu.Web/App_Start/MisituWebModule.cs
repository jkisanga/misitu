using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp.Zero.Configuration;
using Abp.Modules;
using Abp.Web.Mvc;
using Abp.Web.SignalR;
using Misitu.Api;
using Abp.Application.Services;
using Misitu;
using Abp.Configuration.Startup;

namespace Misitu.Web
{
    [DependsOn(
        typeof(MisituDataModule),
        typeof(MisituApplicationModule),
        typeof(MisituWebApiModule),
        typeof(AbpWebSignalRModule),
        //typeof(AbpHangfireModule), - ENABLE TO USE HANGFIRE INSTEAD OF DEFAULT JOB MANAGER
        typeof(AbpWebMvcModule))]
    public class MisituWebModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Enable database based localization
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            //Configure navigation/menu
            Configuration.Navigation.Providers.Add<MisituNavigationProvider>();

            //Configure email
            Configuration.Settings.Providers.Add<MisituSettingProvider>();

            //Configure Hangfire - ENABLE TO USE HANGFIRE INSTEAD OF DEFAULT JOB MANAGER
            //Configuration.BackgroundJobs.UseHangfire(configuration =>
            //{
            //    configuration.GlobalConfiguration.UseSqlServerStorage("Default");
            //});
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);           
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
            //  .ForAll<IApplicationService>(Assembly.GetAssembly(typeof(MisituApplicationModule)), "app")
            //  .Build();
        }
    }
}
