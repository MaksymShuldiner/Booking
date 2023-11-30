using Booking.Application.Services;
using Booking.Application.Services.Abstractions;
using Booking.Web.Authentication;
using Booking.Web.Settings;
using Polly;
using Polly.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
RegisterServices(builder);
RegisterHttpClients(builder);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

void RegisterHttpClients(WebApplicationBuilder webApplicationBuilder)
{
    webApplicationBuilder.Services.AddHttpClient<ITimeSlotService, TimeSlotService>(client =>
        {
            var settings = new TimeSlotServiceSettings();
            builder.Configuration.GetSection("TimeSlotService").Bind(settings);
            settings.ThrowIfInvalid();

            client.BaseAddress = new Uri(settings.BaseUrl);
            client.DefaultRequestHeaders.Authorization = new BasicAuthenticationHeaderValue(settings.UserName, settings.Password);
        })
        .AddPolicyHandler(GetRetryPolicy());

    static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.RequestTimeout ||
                             msg.StatusCode == System.Net.HttpStatusCode.GatewayTimeout)
            .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
}

void RegisterServices(WebApplicationBuilder webApplicationBuilder)
{
    webApplicationBuilder.Services.AddScoped<ITimeSlotService, TimeSlotService>();
}
