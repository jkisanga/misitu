using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using Misitu.Applicants.Interface;
using Abp.Application.Services;

namespace Misitu
{
    [DependsOn(typeof(MisituCoreModule), typeof(AbpAutoMapperModule))]
    public class MisituApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(mapper =>
            {
                //Add your custom AutoMapper mappings here...
                //mapper.CreateMap<,>()
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            
        }
    }
}
