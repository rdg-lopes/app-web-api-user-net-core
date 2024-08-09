using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using UserWebApi.Persistence;
using UserWebApi.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUserPersistence, UserPersistence>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddDbContext<UserPersistence>(options => {
    options.UseSqlServer(builder.Configuration["ConnectionString"]);
});

// Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("default",
        policy => 
        {
            policy.AllowAnyOrigin().AllowAnyHeader();
        }
    );
});


builder.Services.AddControllers().ConfigureApiBehaviorOptions(options => {
    options.SuppressModelStateInvalidFilter = true;    
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Description = "JWT Authorization header using the Bearer",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Configurações JWT
var secret = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtConfigurations:Secret").Value);
builder.Services.AddAuthentication(x =>{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x => {
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secret),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
    
}

app.UseHttpsRedirection();

app.UseCors("default");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
