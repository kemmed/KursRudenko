using System.Configuration;
using System.Data;
using System.Windows;
using Kurs2.Database;
using Kurs2.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Kurs2
{
    
    /// Interaction logic for App.xaml
    
    /// 

    public partial class App : Application
    {
        //Scaffold-DbContext "Data Source=KEMMED\SQLEXPRESS;Initial Catalog=Kurs2Rudenko;Integrated Security=True;Connect Timeout=30;Encrypt=False;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

        private readonly ServiceProvider _serviceProvider;

        public static User currentUser = null;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<Kurs2RudenkoContext>();

            serviceCollection.AddScoped<AuthorizationWindow>();

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var authWindow = _serviceProvider.GetRequiredService<AuthorizationWindow>();

            authWindow.Show();

            base.OnStartup(e);
        }
    }
}