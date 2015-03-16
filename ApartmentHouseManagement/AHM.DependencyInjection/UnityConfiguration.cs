using AHM.BusinessLayer;
using AHM.BusinessLayer.Interfaces;
using AHM.BusinessLayer.Services;
using AHM.DataLayer;
using AHM.DataLayer.Interfaces;
using Microsoft.Practices.Unity;
using Owin;

namespace AHM.DependencyInjection
{
    public class UnityConfiguration
    {
        public static void Run(IUnityContainer container)
        {
            container.RegisterType<IUnitOfWork, UnitOfWork>();

            container.RegisterType<ILocationService, LocationService>();
            container.RegisterType<IApartmentService, ApartmentService>();
            container.RegisterType<IPackageTypeService, PackageTypeService>();
            container.RegisterType<IOccupantService, OccupantService>();
            container.RegisterType<IPackageService, PackageService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IUtilitiesClauseService, UtilitiesClauseService>();
            container.RegisterType<IBillService, BillService>();
            container.RegisterType<IUtilitiesItemService, UtilitiesItemService>();
            container.RegisterType<IBuildingService, BuildingService>();
            container.RegisterType<IBillPdfGenerator, BillPdfGenerator>();
            container.RegisterType<IInstructionsService, InstructionsService>();
            container.RegisterType<IJournalService, JournalService>();
        }

        public static void ConfigureUserManager(IAppBuilder app)
        {
            app.CreatePerOwinContext(AhmContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
        }
    }
}