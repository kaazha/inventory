namespace Aine.Inventory.SharedKernel.Guard;

public class ModelValidationException : ApplicationException
{
  public ModelValidationException(string message) : base(message) { }
}
