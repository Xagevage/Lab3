using Lab3;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ������������ ������� MVC � ���������� �������������
builder.Services.AddControllersWithViews();

// ������������ DbContext � ������������ � SQLite
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlite("Data Source=books.db"));

var app = builder.Build();

// ������������ ��������� ��������� ��������
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // HTTPS Strict Transport Security ��� ������������
}

app.UseHttpsRedirection();  // ��������������� � HTTP �� HTTPS
app.UseStaticFiles();       // ��������� ����������� ������ (css, js � �.�.)

app.UseRouting();           // �������������

app.UseAuthorization();    // ����������� (���� ���������)

// ����������� ������� �� ���������: HomeController -> Index action
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
