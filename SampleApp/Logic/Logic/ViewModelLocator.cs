namespace SampleApp.Logic
{
    using System.ComponentModel;
    using System.Windows;
    using CommunityToolkit.Mvvm.DependencyInjection;
    using Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using Services;

    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            var services = new ServiceCollection();

            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                //services.AddSingleton<ITimeService, DesignTimeService>();
            }
            else
            {
                services.AddSingleton<ITimeService, TimeService>();
            }

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<ProgressViewModel>();
            Ioc.Default.ConfigureServices(services.BuildServiceProvider());
        }

        public static MainViewModel MainViewModel => Ioc.Default.GetService<MainViewModel>();

        public static HomeViewModel HomeViewModel => Ioc.Default.GetService<HomeViewModel>();

        public static ProgressViewModel ProgressViewModel => Ioc.Default.GetService<ProgressViewModel>();
    }
}