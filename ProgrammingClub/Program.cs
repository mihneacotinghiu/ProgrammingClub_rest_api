using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProgrammingClub.DataContext;
using ProgrammingClub.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProgrammingClubDataContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IMembersService, MembersService>();
builder.Services.AddTransient<ICodeSnippetsService, CodeSnippetsService>();
builder.Services.AddTransient<IAnnouncementsService, AnnouncementsService>();
builder.Services.AddTransient<IMembershipsService, MembershipsService>();
builder.Services.AddTransient<IMembershipTypesService, MembershipTypesService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<IEventsService, EventsService>();
builder.Services.AddTransient<IEventTypeService, EventTypeService>();
builder.Services.AddTransient<IPricingModelsService, PricingModelsService>();  
builder.Logging.AddLog4Net("log4net.config");
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
