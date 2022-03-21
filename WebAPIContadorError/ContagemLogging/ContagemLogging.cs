using Microsoft.Extensions.Logging;

namespace WebAPIContadorError.ContagemLogging
{
    public static class ContagemLogging
    {

        //[LoggerMessage(EventId = 1, Level = LogLevel.Information,
        //    Message = "Contador - Valor atual: {valorAtual}")]
        //public static partial void LogValorAtual(
        //    this ILogger logger, int valorAtual);

        public static void LogValorAtual(this ILogger logger, int valorAtual)
        {
            logger.LogInformation($"Contador - Valor atual: {valorAtual}");
        }
    }
}
