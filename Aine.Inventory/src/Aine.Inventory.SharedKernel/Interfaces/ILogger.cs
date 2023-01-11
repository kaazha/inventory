namespace Aine.Inventory.SharedKernel.Interfaces;

public interface ILogger
{
  void Trace(string message);
  void Debug(string message);
  void Info(string message);
  void Warn(string message);
  void Warn(string format, params object[] args);
  void Error(string message, Exception exception = default!);
}

public interface ILogger<T> : ILogger { }
