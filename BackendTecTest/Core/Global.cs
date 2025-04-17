using BackendTecTest.Models;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.X509Certificates;
using Azure.Identity;
namespace BackendTecTest.Core
{
    public class Global
    {
        public GlobalVariables globalVariables;
        public IConfiguration configuration;
        public IConfigurationBuilder configurationBuilder;
        public Global(IConfiguration _configuration, IConfigurationBuilder _configurationBuilder)
        {
            this.configuration = _configuration;
            this.configurationBuilder = _configurationBuilder;
            this.globalVariables.KeyVaultName = configuration["KeyVaultName"];
            this.globalVariables.AzureADApplicationId = configuration["AzureADApplicationId"];
            this.globalVariables.AzureADCertThumbprint = configuration["AzureADCertThumbprint"];
            
        }
        public string GetConnection()
        {
            string response = "";
            response = this.configuration["ConnectionString"];

            return response;
        }

    }
}
