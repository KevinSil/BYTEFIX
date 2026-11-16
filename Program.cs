using ByteFix.Data;
using Microsoft.EntityFrameworkCore;

// Configuração: prepara os serviços, a base de dados e o encaminhamento da aplicação.
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ByteFix")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // Aplica as migrações pendentes antes de a aplicação começar a receber pedidos.
    dbContext.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    // Em produção, apresenta uma página de erro genérica e ativa as proteções HTTPS.
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Encaminha URLs para ações MVC; sem segmentos, abre o dashboard.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
