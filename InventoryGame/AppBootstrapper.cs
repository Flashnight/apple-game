using InventoryGame.Database;
using InventoryGame.ViewModels;
using Caliburn.Micro;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Windows;

namespace InventoryGame
{
    /// <summary>
    /// Configurator for Caliburn.Micro.
    /// </summary>
    public class AppBootstrapper : BootstrapperBase
    {
        /// <summary>
        /// IoC container.
        /// </summary>
        private IKernel _kernel;

        /// <summary>
        /// Configurator for Caliburn.Micro.
        /// </summary>
        public AppBootstrapper()
        {
            Initialize();
        }

        /// <summary>
        /// Configures the Caliburn.Micro framework and setups the Ninject IoC container.
        /// </summary>
        protected override void Configure()
        {
            NinjectModule registrations = new NinjectRegistrations();
            _kernel = new StandardKernel(registrations);
        }

        /// <summary>
        /// Provides an IoC specific implementation of GetInstance.
        /// </summary>
        /// <param name="service">The service to locate.</param>
        /// <param name="key">The key to locate.</param>
        /// <returns>Instance.</returns>
        protected override object GetInstance(Type service, string key)
        {
            if (service == null)
                throw new ArgumentNullException("Service");

            return _kernel.Get(service);
        }

        /// <summary>
        ///     Provides an IoC specific implementation of GetAllInstance.
        /// </summary>
        /// <param name="service">The service to locate.</param>
        /// <returns>A list of instances.</returns>
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _kernel.GetAll(service);
        }

        /// <summary>
        /// Specific implementation of BuildUp.
        /// </summary>
        /// <param name="instance">The instance to perform injection on.</param>
        protected override void BuildUp(object instance)
        {
            _kernel.Inject(instance);
        }

        /// <summary>
        /// Specific implementation of OnExit.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        protected override void OnExit(object sender, EventArgs e)
        {
            _kernel.Dispose();
            base.OnExit(sender, e);
        }

        /// <summary>
        /// Override this to add custom behavior to execute after the application starts.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The args.</param>
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            IDatabaseMaker databaseMaker = _kernel.Get<IDatabaseMaker>();
            databaseMaker.CreateDatabaseAsync();

            DisplayRootViewForAsync<ShellViewModel>();
        }
    }
}
