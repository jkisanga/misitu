using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Localization;
using Misitu.EntityFramework;

namespace Misitu.Migrations.SeedData
{
    public class DefaultLanguagesTextsCreator
    {
        public static List<ApplicationLanguageText> InitialLanguageTexts { get; private set; }

        private readonly MisituDbContext _context;

        static DefaultLanguagesTextsCreator()
        {
            InitialLanguageTexts = new List<ApplicationLanguageText>
            {
                new ApplicationLanguageText {
                    LanguageName = "en",Source="Misitu",Key="CreateNew",Value="Create New",CreationTime=DateTime.Now
                },
                 new ApplicationLanguageText {
                    LanguageName = "en",Source="Misitu",Key="Dashboard",Value="Dashboard",CreationTime=DateTime.Now
                },
                  new ApplicationLanguageText {
                    LanguageName = "en",Source="Misitu",Key="Registration",Value="Registration",CreationTime=DateTime.Now
                },
                   new ApplicationLanguageText {
                    LanguageName = "en",Source="Misitu",Key="Billing",Value="Billing",CreationTime=DateTime.Now
                },
                    new ApplicationLanguageText {
                    LanguageName = "en",Source="Misitu",Key="Setup",Value="Setup",CreationTime=DateTime.Now
                },
                     new ApplicationLanguageText {
                    LanguageName = "en",Source="Misitu",Key="PlotScalling",Value="Plot Scalling",CreationTime=DateTime.Now
                },
                    new ApplicationLanguageText {
                    LanguageName = "en",Source="Misitu",Key="HarvestLicensing",Value="Harvest Licensing",CreationTime=DateTime.Now
                },
                    new ApplicationLanguageText {
                    LanguageName = "en",Source="Misitu",Key="TransitPasses",Value="Transit Passes",CreationTime=DateTime.Now
                },
                     new ApplicationLanguageText {
                    LanguageName = "en",Source="Misitu",Key="NewDealer",Value="New Dealer",CreationTime=DateTime.Now
                },
                    new ApplicationLanguageText {
                    LanguageName = "en",Source="Misitu",Key="SelectedCandidates",Value="Selected Candidates",CreationTime=DateTime.Now
                },
                  new ApplicationLanguageText {
                    LanguageName = "en",Source="Misitu",Key="PendingRegistration",Value="Pending Registration",CreationTime=DateTime.Now
                },
                  new ApplicationLanguageText {
                    LanguageName = "en",Source="Misitu",Key="RegisteredDealers",Value="Registered Dealers",CreationTime=DateTime.Now
                },
                    new ApplicationLanguageText {
                    LanguageName = "en",Source="Misitu",Key="NewCompartment",Value="New Compartment",CreationTime=DateTime.Now
                },
                      new ApplicationLanguageText {
                    LanguageName = "en",Source="Misitu",Key="Compartments",Value="Compartments",CreationTime=DateTime.Now
                },
                    new ApplicationLanguageText {
                    LanguageName = "en",Source="Misitu",Key="PlotAllocation",Value="Plot Allocation",CreationTime=DateTime.Now
                },
                 new ApplicationLanguageText {LanguageName = "en",Source="Misitu",Key="PendingLicense",Value="Pending License",CreationTime=DateTime.Now},
                 new ApplicationLanguageText {LanguageName = "en",Source="Misitu",Key="IssuedLicense",Value="Issued License",CreationTime=DateTime.Now},
                 new ApplicationLanguageText {LanguageName = "en",Source="Misitu",Key="PendingBills",Value="Pending Bills",CreationTime=DateTime.Now},
                 new ApplicationLanguageText {LanguageName = "en",Source="Misitu",Key="PaidBills",Value="Paid Bills",CreationTime=DateTime.Now},
                 new ApplicationLanguageText {LanguageName = "en",Source="Misitu",Key="WellcomeMessage",Value="Wellcome Message",CreationTime=DateTime.Now},
                 new ApplicationLanguageText {LanguageName = "en",Source="Misitu",Key="FormIsNotValidMessage",Value="Form is not valid. Please check and fix errors.",CreationTime=DateTime.Now},
                 new ApplicationLanguageText {LanguageName = "en",Source="Misitu",Key="UserNameOrEmail",Value="User name or email",CreationTime=DateTime.Now},
                 new ApplicationLanguageText {LanguageName = "en",Source="Misitu",Key="Password",Value="Password",CreationTime=DateTime.Now},
                 new ApplicationLanguageText {LanguageName = "en",Source="Misitu",Key="LogIn",Value="Log in",CreationTime=DateTime.Now},
                 new ApplicationLanguageText {LanguageName = "en",Source="Misitu",Key="LoginFailed",Value="Login Failed",CreationTime=DateTime.Now},
                 new ApplicationLanguageText {LanguageName = "en",Source="Misitu",Key="DatabaseConnectionString",Value="Database connection string",CreationTime=DateTime.Now},
                 new ApplicationLanguageText {LanguageName = "en",Source="Misitu",Key="FullName",Value="Full name",CreationTime=DateTime.Now},


            };
        }

        public DefaultLanguagesTextsCreator(MisituDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateLanguages();
        }

        private void CreateLanguages()
        {
            foreach (var text in InitialLanguageTexts)
            {
                AddLanguageIfNotExists(text);
            }
        }

        private void AddLanguageIfNotExists(ApplicationLanguageText text)
        {
            if (_context.LanguageTexts.Any(l => l.TenantId == text.TenantId && l.Key == text.Key))
            {
                return;
            }

            _context.LanguageTexts.Add(text);

            _context.SaveChanges();
        }
    }
}
