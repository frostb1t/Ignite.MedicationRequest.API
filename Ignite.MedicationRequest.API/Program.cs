using FluentValidation;
using Ignite.MedicationRequest.API.Data;
using Ignite.MedicationRequest.API.DTOs.Requests;
using Ignite.MedicationRequest.API.Extensions;
using Ignite.MedicationRequest.API.Interfaces;
using Ignite.MedicationRequest.API.Mappings;
using Ignite.MedicationRequest.API.Repositories;
using Ignite.MedicationRequest.API.Services;
using Ignite.MedicationRequest.API.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddSwagger(); // Adds API versioning to swagger documentation

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDb")));

builder.Services.AddScoped<IMedicationRequestService, MedicationRequestService>();
builder.Services.AddTransient<IValidator<CreateMedicationRequestRequest>, CreateMedicationRequestValidator>();

builder.Services.AddScoped<IMedicationRequestRepository, MedicationRequestRepository>();
builder.Services.AddScoped<IMedicationRepository, MedicationRepository>();
builder.Services.AddScoped<IClinicianRepository, ClinicianRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
