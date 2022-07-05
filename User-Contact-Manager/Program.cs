using Contact_Manager_DataAccess;
using Contact_Manager_DataAccess.Interfaces;
using Contact_Manager_DataAccess.Repository;
using Contact_Manager_Domain.Contracts;
using Contact_Manager_Domain.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ContactDatabaseSettings>(
builder.Configuration.GetSection("ContactManagerDbSettings"));

// Add services to the container.
builder.Services.AddTransient<IContactRepository, ContactRepository>();  
builder.Services.AddTransient<IContactService, ContactService>();

builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddControllersWithViews();

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
    pattern: "{controller=ContactManager}/{action=Index}/{id?}");

app.Run();
