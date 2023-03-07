using System;
using Aine.Inventory.SharedKernel;

namespace Aine.Inventory.Api;

[Inject]
public class Logger : Aine.Inventory.SharedKernel.Interfaces.ILogger
{
  private readonly Serilog.ILogger _logger;

  public Logger(Serilog.ILogger logger)
  {
    _logger = logger;
  }

  public void Debug(string message) => _logger.Debug(message);

  public void Error(string message, Exception? exception = null) => _logger.Error(message, exception);

  public void Info(string message) => _logger.Information(message);

  public void Trace(string message) => _logger.Verbose(message);

  public void Warn(string message) => _logger.Warning(message);

  public void Warn(string format, params object[] args) => _logger.Warning(format, args);
}

