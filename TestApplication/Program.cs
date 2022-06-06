using FluentValidation.AspNetCore;
using System.Text.Json.Serialization;
using TestApplication.Dal;
using TestApplication.Entities.Dto;
using TestApplication.Repository;
using FluentValidation;
using TestApplication.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IRepository>(r => 
        new PsqlRepository(builder.Configuration.GetConnectionString("PostgresConnString") 
                ?? throw new ArgumentException("Connection string is not defined in appsettings")));
builder.Services.AddTransient<IPersonDal, PersonDal>();
builder.Services.AddTransient<ICityDal, CityDal>();
builder.Services.AddMvc()
     .AddJsonOptions(options =>
     {
         options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
     })
     .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PersonRequestModelValidator>());

builder.Services.AddTransient<IValidator<PersonRequestModel>, PersonRequestModelValidator>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c => c.UseInlineDefinitionsForEnums());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
