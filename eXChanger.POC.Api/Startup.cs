using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using eXChanger.POC.Brokers.Sheets;
using eXChanger.POC.Brokers.Storages;
using eXChanger.POC.Services.Coordinations;
using eXChanger.POC.Services.Foundations.ExternalPersons;
using eXChanger.POC.Services.Foundations.Persons;
using eXChanger.POC.Services.Foundations.Pets;
using eXChanger.POC.Services.Orchestrations.ExternalPersons;
using eXChanger.POC.Services.Processings.Pets;
using eXChanger.POC.Services.Orchestrations.PersonPets;
using eXChanger.POC.Services.Processings.Persons;
using eXChanger.POC.Services.Processings.ExternalPersons;

namespace eXChanger.POC
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{

			services.AddControllers();
			AddBrokers(services);
			AddFoundationServices(services);
			AddProcessingServices(services);
			AddOrchestrationServices(services);
			AddCoordinationServices(services);

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc(
					name: "v1",
					info: new OpenApiInfo
					{
						Title = "eXChanger.POC",
						Version = "v1"
					});
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();

				app.UseSwaggerUI(config =>
					config.SwaggerEndpoint(
						url: "/swagger/v1/swagger.json",
						name: "eXChanger.POC v1"));
			}

			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
				endpoints.MapControllers());
		}

		private static void AddBrokers(IServiceCollection services)
		{
			services.AddTransient<IStorageBroker, StorageBroker>();
			services.AddTransient<ISheetBroker, SheetBroker>();
		}

		private static void AddFoundationServices(IServiceCollection services)
		{
			services.AddTransient<IPersonService, PersonService>();
			services.AddTransient<IPetService, PetService>();
			services.AddTransient<IExternalPersonService, ExternalPersonService>();
		}

		private static void AddProcessingServices(IServiceCollection services)
		{
			services.AddTransient<IPersonProcessingService, PersonProcessingService>();
			services.AddTransient<IPetProcessingService, PetProcessingService>();
			services.AddTransient<IExternalPersonProcessingService, ExternalPersonProcessingService>();
		}

		private static void AddOrchestrationServices(IServiceCollection services)
		{
			services.AddTransient<IPersonPetOrchestrationService, PersonPetOrchestrationService>();
			services.AddTransient<IExternalPersonOrchestrationService, ExternalPersonOrchestrationService>();
		}

		private static void AddCoordinationServices(IServiceCollection services)
		{
			services.AddTransient<IExternalPersonWithPetsCoordinationService, ExternalPersonWithPetsCoordinationService>();
		}
	}
}
