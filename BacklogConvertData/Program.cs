using BacklogConvertData.App.Api;
using BacklogConvertData.App.Handle;
using BacklogConvertData.App.Interface.IHandle;
using BacklogConvertData.App.Service;
using BacklogConvertData.Classes;
using BacklogConvertData.Classes.Handle;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

namespace BacklogImportData
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(serviceProvider.GetRequiredService<MainForm>());
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<MainForm>();

            //services.AddTransient<ICommonApi, CommonApi>();
            //services.AddTransient<IComponentApi, ComponentApi>();
            //services.AddTransient<IRateLimitApi, RateLimitApi>();
            //services.AddTransient<IResourceApi, ResourceApi>();

            //services.AddTransient<IApiUrlHandle, ApiUrlHandle>();
            //services.AddTransient<IExcelHandle, ExcelHandle>();
            //services.AddTransient<IMainHandle, MainHandle>();
            //services.AddTransient<IQueueHandle, QueueHandle>();
            //services.AddTransient<IResponseHandle, ResponseHandle>();

            //services.AddTransient<ICategoryVersionService, CategoryVersionService>();
            //services.AddTransient<IIssueService, IssueService>();
            //services.AddTransient<IWikiService, WikiService>();
        }
    }
}
