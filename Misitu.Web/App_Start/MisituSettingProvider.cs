using Abp.Configuration;
using Abp.Net.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Misitu.Web
{
    public class MisituSettingProvider: SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
                   {
                new SettingDefinition(EmailSettingNames.Smtp.Host, "dallas114.mysitehosted.com", scopes: SettingScopes.Application | SettingScopes.Tenant),
                new SettingDefinition(EmailSettingNames.Smtp.Port, "465",scopes: SettingScopes.Application | SettingScopes.Tenant),
                new SettingDefinition(EmailSettingNames.Smtp.UserName, "admin@nature-reserves.go.tz", scopes: SettingScopes.Application | SettingScopes.Tenant),
                new SettingDefinition(EmailSettingNames.Smtp.Password, "admin123", scopes: SettingScopes.Application | SettingScopes.Tenant),
                new SettingDefinition(EmailSettingNames.Smtp.Domain, string.Empty, scopes: SettingScopes.Application | SettingScopes.Tenant),
                new SettingDefinition(EmailSettingNames.Smtp.EnableSsl, "true", scopes: SettingScopes.Application | SettingScopes.Tenant),
                new SettingDefinition(EmailSettingNames.Smtp.UseDefaultCredentials, "false", scopes: SettingScopes.Application | SettingScopes.Tenant),
                new SettingDefinition(EmailSettingNames.DefaultFromAddress, "admin@nature-reserves.go.tz", scopes: SettingScopes.Application | SettingScopes.Tenant),
                new SettingDefinition(EmailSettingNames.DefaultFromDisplayName, "TFS", scopes: SettingScopes.Application | SettingScopes.Tenant)
            };
        }
    }
}