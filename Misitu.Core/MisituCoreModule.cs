using System.Reflection;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using Abp.Zero;
using Abp.Zero.Configuration;
using Misitu.Authorization;
using Misitu.Authorization.Roles;
using Misitu.MultiTenancy;
using Misitu.Users;
using Abp.Localization;
using Abp.Localization.Sources.Resource;

namespace Misitu
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class MisituCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = false;
           

            //Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            //Remove the following line to disable multi-tenancy.
            Configuration.MultiTenancy.IsEnabled = MisituConsts.MultiTenancyEnabled;

            Configuration.Localization.Languages.Add(new LanguageInfo("en", "English", "famfamfam-flag-england", true));
            //Add / remove localization sources here

            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    MisituConsts.LocalizationSourceName,

                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(),
                        "Misitu.Localization.Source"
                        )
                    )
                );

         

            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Authorization.Providers.Add<MisituAuthorizationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
