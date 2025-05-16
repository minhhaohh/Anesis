using Anesis.ApiService.Domain.Entities;
using Anesis.ApiService.EntityFrameworkCore;
using Anesis.ApiService.EntityFrameworkCore.EntityFrameworkCore;
using Anesis.ApiService.Infrastructures;
using Anesis.ApiService.Services.IServices;
using Anesis.ApiService.Services.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

#region Swagger

builder.Services.AddSwaggerGen();

#endregion

#region Authentication & Authorization

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//})
//.AddCookie();

//builder.Services.AddAuthorization();

#endregion

#region Identity

builder.Services.Configure<PasswordHasherOptions>(options =>
{
    options.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV2;
});

builder.Services.AddIdentity<Account, Role>()
    .AddUserStore<AccountStore>()
    .AddRoleStore<RoleStore>()
    .AddDefaultTokenProviders();

#endregion

#region UserContext

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContext, UserContext>();

#endregion

#region Services

builder.Services.AddDbContext<AnesisContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<DbContext, AnesisContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IBankTransactionService, BankTransactionService>();
builder.Services.AddScoped<IBatchTransactionService, BatchTransactionService>();

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICreditTransactionService, CreditTransactionService>();
builder.Services.AddScoped<IChangeLogService, ChangeLogService>();

builder.Services.AddScoped<IDepositService, DepositService>();
builder.Services.AddScoped<IDeviceService, DeviceService>();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddScoped<IInsuranceService, InsuranceService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();

builder.Services.AddScoped<ILocationService, LocationService>();

builder.Services.AddScoped<IMediaFileService, MediaFileService>();

builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IProposalService, ProposalService>();
builder.Services.AddScoped<IProcedureService, ProcedureService>();
builder.Services.AddScoped<IProviderService, ProviderService>();

builder.Services.AddScoped<IReconciliationService, ReconciliationService>();

builder.Services.AddScoped<ISiteSettingService, SiteSettingService>();
builder.Services.AddScoped<ISurgeryCaseService, SurgeryCaseService>();

builder.Services.AddScoped<ITimetableService, TimetableService>();

builder.Services.AddFluentValidationClientsideAdapters();

#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    //app.MapScalarApiReference();
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();

app.Run();