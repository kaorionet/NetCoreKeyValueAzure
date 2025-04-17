using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BackendTecTest.Core;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;

namespace BackendTecTest.Controllers
{
    /// <summary>
    /// ControllRegisterAccount.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterAccount : ControllerBase
    {
        public IConfiguration configuration;
        public IConfigurationBuilder configurationBuilder;

        /// <summary>
        /// Constructor RegisterAccount.
        /// </summary>
        public RegisterAccount(IConfiguration _configuration, IConfigurationBuilder _configurationBuilder)
        {
            this.configuration = _configuration;
            this.configurationBuilder = _configurationBuilder;
        }

        /// <summary>
        /// Endpoint para regitros.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> RegisterAsync()
        {
            const string secretName = "ConnectionString";
            Global global = new Global(configuration, configurationBuilder);
            var kvUri = $"https://{global.globalVariables.KeyVaultName}.vault.azure.net";
            
            var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
            var secret = await client.GetSecretAsync(secretName);

            var response = global.GetConnection();
            return Ok(response);
        }

        /// <summary>
        /// Endpoint que devuelve una imagen.
        /// </summary>
        /// <returns></returns>
        public IActionResult List()
        {
            return null;
        }
    }
}
