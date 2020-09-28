using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YildirimHoldingTest.Core.Data.Factory;
using YildirimHoldingTest.Core.Data.Interfaces;
using YildirimHoldingTest.Core.Log;
using YildirimHoldingTest.Domain.Customer.Cache;
using YildirimHoldingTest.Domain.Customer.Interfaces;
using YildirimHoldingTest.Domain.Customer.Manager;
using YildirimHoldingTest.Domain.InvoiceDomain.Interfaces;
using YildirimHoldingTest.Domain.InvoiceDomain.Manager;
using YildirimHoldingTest.Domain.Users.Cache;
using YildirimHoldingTest.Domain.Users.Interfaces;
using YildirimHoldingTest.Domain.Users.Manager;

namespace YildirimHoldingTest
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            #region Register
            services.AddControllers();
            Register(services);
            services.AddMvc();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors("AllowAllOrigins");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private void Register(IServiceCollection services)
        {
            #region Base
            services.AddScoped<IConnectionFactory, ConnectionFactory>();
            services.AddScoped<IDbFactory, DbFactory>();
            services.AddScoped<IMongoFactory, MongoFactory>();
            services.AddScoped<ILogFactory, LogFactory>();
            #endregion

            #region Domains
            services.AddScoped<ICustomerInfoCache, CustomerInfoCache>();
            services.AddScoped<ICustomerInfoManager, CustomerInfoManager>();

            services.AddScoped<IUsersInfoCache, UsersInfoCache>();
            services.AddScoped<IUsersInfoManager, UsersInfoManager>();

            services.AddScoped<IInvoiceHeaderManager, InvoiceHeaderManager>();
            #endregion
        }
    }
}
