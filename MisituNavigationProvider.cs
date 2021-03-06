﻿using Abp.Application.Navigation;
using Abp.Localization;
using Misitu.Authorization;

namespace Misitu.Web
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See Views/Layout/_TopMenu.cshtml file to know how to render menu.
    /// </summary>
    public class MisituNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        "Home",
                        L("Dashboard"),
                        url: "",
                        icon: "icon-home"
                        //requiresAuthentication: true
                        )
                ).AddItem(
                      new MenuItemDefinition(
                    "Registration",
                    L("Registration"),
                    url: "/dealers/Dashboard",
                    icon: "icon-user",
                    requiredPermissionName: PermissionNames.Pages_Registration
                    ).AddItem(
                        new MenuItemDefinition(
                            "Dashboard",
                            new LocalizableString("Dashboard", "Misitu"),
                            url: "/Dealers/Dashboard",
                            icon: "icon-desktop"
                            )
                   ).AddItem(
                        new MenuItemDefinition(
                            "New Dealer",
                            new LocalizableString("NewDealer", "Misitu"),
                            url: "/Dealers/Create",
                            icon: "icon-plus"
                            )
                   ).AddItem(
                        new MenuItemDefinition(
                            "Selected Candidates",
                            new LocalizableString("SelectedCandidates", "Misitu"),
                            url: "/Candidates",
                            icon: "icon-ok"
                            )
                   ).AddItem(
                        new MenuItemDefinition(
                            "Pending Registration",
                            new LocalizableString("PendingRegistration", "Misitu"),
                            url: "/Dealers",
                            icon: "icon-th"
                            )
                   ).AddItem(
                        new MenuItemDefinition(
                            "Registered Dealers",
                            new LocalizableString("RegisteredDealers", "Misitu"),
                            url: "/Dealers/Registered",
                            icon: "icon-th"
                            )
                   )
                   )
            .AddItem(
                      new MenuItemDefinition(
                    "Plot Scalling",
                    new LocalizableString("PlotScalling", "Misitu"),
                      url: "/Compartments/Dashboard",
                    icon: "icon-pencil",
                       requiredPermissionName: PermissionNames.Pages_Plot_Scalling
                    ).AddItem(
                        new MenuItemDefinition(
                            "Dashboard",
                            new LocalizableString("Dashboard", "Misitu"),
                            url: "/Compartments/Dashboard",
                            icon: "icon-desktop"
                            )
                   ).AddItem(
                        new MenuItemDefinition(
                            "New Compartment",
                            new LocalizableString("NewCompartment", "Misitu"),
                            url: "/Compartments/Create",
                            icon: "icon-plus"
                            )
                    ).AddItem(
                        new MenuItemDefinition(
                            "Compartments",
                            new LocalizableString("Compartments", "Misitu"),
                            url: "/Compartments",
                            icon: "icon-th"
                            )
                    ).AddItem(
                        new MenuItemDefinition(
                            "Compartments",
                            new LocalizableString("TalliedCompartments", "Misitu"),
                            url: "/Compartments/TalliedCompartments",
                            icon: "icon-list"
                            )
                    )
                 ).AddItem(
                      new MenuItemDefinition(
                    "Harvest Licensing",
                    new LocalizableString("HarvestLicensing", "Misitu"),
                        url: "/License/Dashboard",
                        icon: "icon-file-text",
                       requiredPermissionName: PermissionNames.Pages_Harvest_Licensing
                    ).AddItem(
                        new MenuItemDefinition(
                            "Dashboard",
                            new LocalizableString("Dashboard", "Misitu"),
                            url: "/License/Dashboard",
                            icon: "icon-desktop"
                            )
                   ).AddItem(
                        new MenuItemDefinition(
                            "Licenses",
                            new LocalizableString("PlotAllocation", "Misitu"),
                            url: "/Compartments/Tallied",
                            icon: "icon-th"
                            )
                    ).AddItem(
                        new MenuItemDefinition(
                            "Licenses",
                            new LocalizableString("PendingLicense", "Misitu"),
                            url: "/License/Pending",
                            icon: "icon-th"
                            )
                    ).AddItem(
                        new MenuItemDefinition(
                            "Licenses",
                            new LocalizableString("IssuedLicense", "Misitu"),
                            url: "/License",
                            icon: "icon-th"
                            )
                    )
                 ).AddItem(
                      new MenuItemDefinition(
                    "Billing",
                    new LocalizableString("Billing", "Misitu"),
                    icon: "icon-money",
                     url: "/Bills/Dashboard",
                     requiredPermissionName: PermissionNames.Pages_Billing
                    ).AddItem(
                        new MenuItemDefinition(
                            "Dashboard",
                            new LocalizableString("Dashboard", "Misitu"),
                            url: "/Bills/Dashboard",
                            icon: "icon-desktop"
                            )
                   ).AddItem(
                        new MenuItemDefinition(
                            "Create New",
                            new LocalizableString("CreateNew", "Misitu"),
                            url: "/Bills/CreateNew",
                            icon: "icon-plus"
                            )
                   ).AddItem(
                        new MenuItemDefinition(
                            "Pending Bills",
                            new LocalizableString("PendingBills", "Misitu"),
                            url: "/Bills",
                            icon: "icon-th"
                            )
                    ).AddItem(
                        new MenuItemDefinition(
                            "Paid Bills",
                            new LocalizableString("PaidBills", "Misitu"),
                            url: "/Bills/Paid",
                            icon: "icon-th"
                            )
                    )
                 ).AddItem(
                      new MenuItemDefinition(
                    "Transit Passes",
                    new LocalizableString("TransitPasses", "Misitu"),
                    icon: "icon-truck",
                     url: "/TransitPass/Dashboard",
                     requiredPermissionName: PermissionNames.Pages_Transit_Passes
                    ).AddItem(
                        new MenuItemDefinition(
                            "Dashboard",
                            new LocalizableString("Dashboard", "Misitu"),
                            url: "/TransitPass/Dashboard",
                            icon: "icon-desktop"
                            )
                   )
                   ).AddItem(
                      new MenuItemDefinition(
                    "Setup",
                    new LocalizableString("Setup", "Misitu"),
                    icon: "icon-gear",
                     url: "/Users",
                       requiredPermissionName: PermissionNames.Pages_Setup
                    ).AddItem(
                        new MenuItemDefinition(
                            "Users",
                            new LocalizableString("Users", "Misitu"),
                            url: "/Users",
                            icon: "icon-user"
                            )
                    ).AddItem(
                        new MenuItemDefinition(
                            "Zones",
                            new LocalizableString("Zones", "Misitu"),
                            url: "/Zones",
                            icon: "icon-star"
                            )
                    ).AddItem(
                        new MenuItemDefinition(
                            "FinancialYears",
                            new LocalizableString("FinancialYears", "Misitu"),
                            url: "/FinancialYears",
                            icon: "icon-calendar"
                            )
                    ).AddItem(
                        new MenuItemDefinition(
                            "Regions",
                            new LocalizableString("Regions", "Misitu"),
                            url: "/Regions",
                            icon: "icon-th"
                            )
                    ).AddItem(
                        new MenuItemDefinition(
                            "Stations",
                            new LocalizableString("Stations", "Misitu"),
                            url: "/Stations",
                            icon: "icon-th"
                            )
                    ).AddItem(
                        new MenuItemDefinition(
                            "Divisions",
                            new LocalizableString("Divisions", "Misitu"),
                            url: "/Divisions",
                            icon: "icon-th"
                            )
                    ).AddItem(
                        new MenuItemDefinition(
                            "Ranges",
                            new LocalizableString("Ranges", "Misitu"),
                            url: "/Ranges",
                            icon: "icon-th"
                            )
                    ).AddItem(
                        new MenuItemDefinition(
                            "Tariffs",
                            new LocalizableString("Tariffs", "Misitu"),
                            url: "/Tariffs",
                            icon: "icon-th"
                            )
                    ).AddItem(
                        new MenuItemDefinition(
                            "Activities",
                            new LocalizableString("Activities", "Misitu"),
                            url: "/Activities",
                            icon: "icon-th"
                            )
                    ).AddItem(
                        new MenuItemDefinition(
                            "Revenue Sources",
                            new LocalizableString("RevenueSources", "Misitu"),
                            url: "/RevenueSources",
                            icon: "icon-th"
                            )
                    ).AddItem(
                        new MenuItemDefinition(
                            "Specie Categories",
                            new LocalizableString("SpecieCategories", "Misitu"),
                            url: "/SpecieCategories",
                            icon: "icon-th"
                            )
                    ).AddItem(
                        new MenuItemDefinition(
                            "Species",
                            new LocalizableString("Species", "Misitu"),
                            url: "/Species",
                            icon: "icon-th"
                            )
                    ).AddItem(
                        new MenuItemDefinition(
                            "Harvesting Plan",
                            new LocalizableString("HarvestingPlan", "Misitu"),
                            url: "/HarvestingPlan",
                            icon: "icon-th"
                            )
                    )
            );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, MisituConsts.LocalizationSourceName);
        }
    }
}
