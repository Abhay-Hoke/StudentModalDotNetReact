using Microsoft.EntityFrameworkCore;
using StudentAdmissionPortal.Data;
using StudentAdmissionPortal.Mapper;
using StudentAdmissionPortal.Repositories.Abstract;
using StudentAdmissionPortal.Repositories.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IFamilyMembersRepository, FamilyMembersRepository>();
builder.Services.AddScoped<INationalityRepository, NationalityRepository>();


builder.Services.AddDbContext<StudentModalDbContext>(options =>
                        options.UseSqlServer(builder.Configuration.GetConnectionString("StudentModalDbContext")));
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.UseCors();
app.Run();
