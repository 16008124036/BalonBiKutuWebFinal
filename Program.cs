using Microsoft.EntityFrameworkCore;
using HediyelikEsya.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Veritabanı Servisi
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Gelişmiş Güvenlik Ayarı
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", config =>
    {
        config.Cookie.Name = "AdminGirisCerezi";
        config.LoginPath = "/Account/Login";
        config.AccessDeniedPath = "/Home/Index";
        config.ExpireTimeSpan = TimeSpan.FromHours(1); // 1 saat sonra otomatik çıkış yapar
        config.SlidingExpiration = true; // İşlem yaptıkça süreyi uzatır
    });

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Klasik statik dosyalar için

app.UseRouting();

// SIRALAMA KRİTİKTİR: Routing'den sonra, Map'ten önce gelmeli
app.UseAuthentication(); 
app.UseAuthorization();

app.MapStaticAssets(); // .NET 9 yeni özelliği

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();