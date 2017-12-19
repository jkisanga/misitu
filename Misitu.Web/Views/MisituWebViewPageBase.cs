using Abp.Web.Mvc.Views;

namespace Misitu.Web.Views
{
    public abstract class MisituWebViewPageBase : MisituWebViewPageBase<dynamic>
    {

    }

    public abstract class MisituWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected MisituWebViewPageBase()
        {
            LocalizationSourceName = MisituConsts.LocalizationSourceName;
        }
    }
}