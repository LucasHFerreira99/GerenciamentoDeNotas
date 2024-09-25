using GerenciamentoDeNotas.Helper;
using GerenciamentoDeNotas.Data;
using GerenciamentoDeNotas.Helper;
using GerenciamentoDeNotas.Services.Professor;
using GerenciamentoDeNotas.Services.Usuario;
using Microsoft.EntityFrameworkCore;
using GerenciamentoDeNotas.Services.Aluno;
using GerenciamentoDeNotas.Services.Materia;
using GerenciamentoDeNotas.Services.Turma;
using GerenciamentoDeNotas.Services.MateriaProfessor;
using GerenciamentoDeNotas.Services.Nota;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("pt-BR"), // ou outra cultura desejada
        new CultureInfo("en-US")
    };

    options.DefaultRequestCulture = new RequestCulture("pt-BR");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IProfessorService, ProfessorService>();
builder.Services.AddScoped<IMateriaService, MateriaService>();
builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<ITurmaService, TurmaService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IMateriaProfessorService, MateriaProfessorService>();
builder.Services.AddScoped<INotaService, NotaService>();
builder.Services.AddScoped<ISessao, Sessao>();
builder.Services.AddScoped<IEmail, Email>();

builder.Services.AddSession(o =>
{
    o.Cookie.HttpOnly = true;
    o.Cookie.IsEssential = true;
});


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

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
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
