using Aine.Inventory.SharedKernel.Security;
using Aine.Inventory.SharedKernel.Security.Interfaces;

namespace Aine.Inventory.UnitTests;

public class EncrypterTests
{
  private readonly IEncryptor _encrypter;

  public EncrypterTests()
  {
    _encrypter = new DefaultAesEncrypter();
  }

  [Fact]
  public void EncryptsText()
  {
    var encrypted = _encrypter.Encrypt("kaaz1234");
    Assert.NotEmpty(encrypted);
  }

  [Fact]
  public void DecryptsText()
  {
    var decrypted = _encrypter.Decrypt("rE4FMhPdc5ZAv1Ebj/hFmg==");
    Assert.NotEmpty(decrypted);
  }
}

