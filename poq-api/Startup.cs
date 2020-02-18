using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using poq_api.Business.Products;
using poq_api.Configuration;
using RestEase;

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
            var productClient = RestClient.For<IProductClient>(endpointConfig.ProductsUrl);
            // if you need authorization to the mocky.io 
            //var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
            //productClient.Authorization = new AuthenticationHeaderValue("Basic", credentials);
            services.AddSingleton<IProductClient>(productClient);

            services.AddScoped<IProductService, ProductService>();

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
