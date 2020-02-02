﻿using AppleGame.ViewModels;
using Caliburn.Micro;
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
            Bind<InventoryViewModel>().ToConstant(new InventoryViewModel())
                                      .InThreadScope();
            Bind<ItemsSourceViewModel>().ToConstant(new ItemsSourceViewModel())
                                        .InThreadScope();
        }
    }
}
