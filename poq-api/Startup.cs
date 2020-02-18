using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using poq_api.Business;
using poq_api.Business.Products;
using poq_api.Configuration;

namespace poq_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var endpointConfig = new EndpointConfiguration();
            Configuration.GetSection("EndpointConfiguration").Bind(endpointConfig);
            var productClient = ServiceFactory.CreateProductClient(endpointConfig.ProductsUrl, null, null);
            services.AddSingleton<IProductClient>(productClient);

            services.AddSwaggerDocument(configure =>
            {
                configure.Title = "Filter Products API";
                configure.Version = "v1";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseMvc();
        }
    }
}
