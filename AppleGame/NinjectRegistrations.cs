using AppleGame.Database;
using AppleGame.Misc;
using AppleGame.ViewModels;
using Caliburn.Micro;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleGame
{
    // Dependencies settings for Ninject.
    public class NinjectRegistrations : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            int imageId = int.Parse(ConfigurationManager.AppSettings["imageId"]);

            // Objects for Caliburn.Micro.
            Bind<IWindowManager>().To<WindowManager>()
                                  .InSingletonScope();
            Bind<IEventAggregator>().To<EventAggregator>()
                                    .InSingletonScope();

            // UserControls
            Bind<MainMenuViewModel>().ToConstructor(opt => new MainMenuViewModel(opt.Inject<IEventAggregator>()))
                                     .InSingletonScope();
            Bind<InventoryViewModel>().ToConstructor(opt => new InventoryViewModel(opt.Inject<IKernel>(), opt.Inject<IEventAggregator>(), opt.Inject<IInventoryDbRepository>()))
                                      .InThreadScope();
            Bind<ItemsSourceViewModel>().ToConstructor(opt => new ItemsSourceViewModel(opt.Inject<ItemsSQLiteRepository>(), imageId))
                                        .InThreadScope();
            Bind<InventoryCellViewModel>().ToConstructor(opt => new InventoryCellViewModel(opt.Inject<IMediaPlayerWrapper>()))
                                      .InTransientScope();

            // Extra objects (from misc. folder).
            Bind<IMediaPlayerWrapper>().To<MediaPlayerWrapper>()
                                       .InSingletonScope();

            // Database objects
            Bind<IDatabaseMaker>().To<SQLiteDatabaseMaker>()
                                  .InTransientScope();

            Bind<ItemsDbRepository>().To<ItemsSQLiteRepository>()
                                   .InTransientScope();
            Bind<IInventoryDbRepository>().To<InventorySQLiteRepository>()
                                          .InTransientScope();
        }
    }
}
