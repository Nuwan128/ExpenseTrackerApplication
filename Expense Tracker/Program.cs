using DataAccessLibrary.Data;
using DataAccessLibrary.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Expense_Tracker
{
    internal static class Program
    {
        
        [STAThread]
        static void Main()
        {
            
            var builder = new HostBuilder()
                .ConfigureAppConfiguration((config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                    config.AddUserSecrets(typeof(Program).Assembly);


                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddTransient<IUserServices, UserServices>();
                    services.AddTransient<IExpenseServices, ExpenseServices>();



                    services.AddTransient<ExpenseDBContext>();

                    services.AddTransient<Register>();
                    services.AddTransient<LogIn>();
                    



                });

            var host = builder.Build();

            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;


            try
            {

                ApplicationConfiguration.Initialize();
                var frm = services.GetRequiredService<LogIn>();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex}");
            }
           

        }

    }
    
}