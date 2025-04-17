using Azure.Core;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();

    using var x509Store = new X509Store(StoreLocation.CurrentUser);

    x509Store.Open(OpenFlags.ReadOnly);

    var x509Certificate = x509Store.Certificates
        .Find(
            X509FindType.FindByThumbprint,
            builder.Configuration["AzureADCertThumbprint"],
            validOnly: false)
        .OfType<X509Certificate2>()
        .Single();

    //builder.Configuration.AddAzureKeyVault(
    //    new Uri($"https://{builder.Configuration["KeyVaultName"]}.vault.azure.net"),
    //    new ClientCertificateCredential(
    //        builder.Configuration["AzureADDirectoryId"],
    //        builder.Configuration["AzureADApplicationId"],
    //        x509Certificate));

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
