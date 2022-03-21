using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using WebAPIContadorError.ContagemLogging;
using WebAPIContadorError.Models;

namespace WebAPIContadorError.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContadorController : ControllerBase
    {
        private static readonly Contador _CONTADOR = new Contador();
        private readonly ILogger<ContadorController> _logger;
        private readonly IConfiguration _configuration;
        public ContadorController(ILogger<ContadorController> logger,
       IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public ResultadoContador Get()
        {

            int valorAtualContador;

            lock (_CONTADOR)
            {
                _CONTADOR.Incrementar();
                valorAtualContador = _CONTADOR.ValorAtual;
            }

            _logger.LogValorAtual(valorAtualContador);

            if (valorAtualContador % 2 == 0)
            {
                _logger.LogError("Simulando falha...");
                throw new Exception("Simulação de falha!");
            }

            return new ResultadoContador()
            {
                ValorAtual = valorAtualContador,
                Producer = _CONTADOR.Local,
                Kernel = _CONTADOR.Kernel,
                Framework = _CONTADOR.Framework,
                Mensagem = _configuration["MensagemVariavel"]
            };
        }
    }
}
