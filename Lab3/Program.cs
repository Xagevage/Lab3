using Lab3;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Регистрируем сервисы MVC с поддержкой представлений
builder.Services.AddControllersWithViews();

// Регистрируем DbContext с подключением к SQLite
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlite("Data Source=books.db"));

var app = builder.Build();

// Конфигурация конвейера обработки запросов
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // HTTPS Strict Transport Security для безопасности
}

app.UseHttpsRedirection();  // Перенаправление с HTTP на HTTPS
app.UseStaticFiles();       // Поддержка статических файлов (css, js и т.д.)

app.UseRouting();           // Маршрутизация

app.UseAuthorization();    // Авторизация (если требуется)

// Настраиваем маршрут по умолчанию: HomeController -> Index action
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
