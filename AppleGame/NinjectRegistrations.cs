using AppleGame.Misc;
using AppleGame.ViewModels;
using Caliburn.Micro;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
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
            // Objects for Caliburn.Micro.
            Bind<IWindowManager>().To<WindowManager>()
                                  .InSingletonScope();
            Bind<IEventAggregator>().To<EventAggregator>()
                                    .InSingletonScope();

            // UserControls
            Bind<InventoryViewModel>().ToConstructor(opt => new InventoryViewModel(opt.Inject<IKernel>()))
                                      .InThreadScope();
            Bind<ItemsSourceViewModel>().ToConstructor(opt => new ItemsSourceViewModel())
                                        .InThreadScope();
            Bind<InventoryCellViewModel>().ToConstructor(opt => new InventoryCellViewModel(opt.Inject<IMediaPlayerWrapper>()))
                                      .InTransientScope();

            Bind<IMediaPlayerWrapper>().To<MediaPlayerWrapper>()
                                       .InSingletonScope();
        }
    }
}
