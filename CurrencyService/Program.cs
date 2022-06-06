using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using CurrencyService;
using CurrencyService.Repository;
using CurrencyService.Options;
using CurrencyService.Jobs;
using CurrencyService.Repository.Dal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddOptions<CurrencyManagerOptions>()
    .Bind(builder.Configuration.GetSection("CurrencyManagerOptions"))
    .ValidateDataAnnotations();

builder.Services.AddOptions<CurrencyJobOptions>()
    .Bind(builder.Configuration.GetSection("CurrencyJobOptions"))
    .ValidateDataAnnotations();

builder.Services.AddSingleton<IRepository>(r =>
        new PgsqlRepository(builder.Configuration.GetConnectionString("PostgresConnString")
                ?? throw new ArgumentException("Connection string is not defined in appsettings")));

builder.Services.AddSingleton<ICurrencyDal, CurrencyDal>();
builder.Services.AddCurrencyServiceManager();

builder.Services.AddSingleton<IJobFactory, SingletonJobFactory>();
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

// Add our job
builder.Services.AddSingleton<CurrencyJob>();
builder.Services.AddSingleton(new JobSchedule(
    jobType: typeof(CurrencyJob),
    cronExpression: builder.Configuration.GetValue<string>("CronExpression")
    ?? throw new ArgumentException("CronExpression for CurrencyServiceJob is not defined in appsettings")));
builder.Services.AddHostedService<QuartzHostedService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
