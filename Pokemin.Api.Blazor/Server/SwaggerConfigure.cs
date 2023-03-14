using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Pokemon.Api.Docs.Attributes;
using Pokemon.Api.Docs.Filters;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Pokemon.Api
{
    public static class SwaggerConfigure
    {
        public static void AddSwaggerSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSwaggerGen(s =>
            {
                s.UseOneOfForPolymorphism();
                s.SelectDiscriminatorNameUsing((baseType) => "TypeName");
                s.SelectDiscriminatorValueUsing((subType) => subType.Name);
                s.DocumentFilter<OperationsOrderingFilter>();

                ConfigureDocumentation(configuration, s);
                ConfigureSchemas(s);

                s.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}");
                s.ExampleFilters();
                s.OperationFilter<SwaggerExcludeFilter>();
                s.OperationFilter<JsonIgnoreQueryOperationFilter>();

                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "App Cidadão - API",
                    Description = "Api que alimenta com dados o aplicativo ECO-Pass. \n </br></br> Coleção de Testes do postman: https://www.getpostman.com/collections/5e80bbfcdb06e173b0e1 </br> Environment de hml: https://drive.google.com/file/d/1ifL_wmumaClR42myHUvkAN5t7wVQK2Yv/view?usp=sharing",
                    Contact = new OpenApiContact
                    {
                        Name = "Equipe (Back-End: Felipe)",
                        Email = string.Empty,
                        Url = new Uri("https://www.linkedin.com/in/felipepassion/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://mit.com/license"),
                    }
                });
            });

            services.AddSwaggerExamplesFromAssemblies(Assembly.GetExecutingAssembly());

            services.AddSwaggerGenNewtonsoftSupport();
            services.AddMvcCore().AddApiExplorer();
        }

        public static void UseSwaggerSetup(this IApplicationBuilder app, IConfiguration configuration)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseSwagger(c =>
            {
                string basePath = "/";

                c.RouteTemplate = "{documentName}/swagger.json";
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Servers = new List<OpenApiServer>
                        { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}{basePath}" } };
                });
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/v1/swagger.json", "V1 Docs");
            });
        }

        /// <summary>
        /// Swagger schemsa and OperationFilters configuration
        /// </summary>
        /// <param name="options"></param>
        private static void ConfigureSchemas(SwaggerGenOptions options)
        {
            options.DescribeAllParametersInCamelCase();
        }

        /// <summary>
        /// Documentation XML path's and README configuration
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="options"></param>
        private static void ConfigureDocumentation(IConfiguration configuration, SwaggerGenOptions options)
        {
            var path = PlatformServices.Default.Application.ApplicationBasePath;

            options.IncludeXmlComments(Path.Combine(path, $"Pokemon.Api.xml"), true);
            options.IncludeXmlComments(Path.Combine(path, $"Pokemon.Application.DTO.xml"), true);
            options.EnableAnnotations();
        }
    }
}
