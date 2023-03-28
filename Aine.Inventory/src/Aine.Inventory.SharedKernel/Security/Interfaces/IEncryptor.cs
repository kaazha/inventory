namespace Aine.Inventory.SharedKernel.Security.Interfaces;

public interface IEncryptor
{
    string Encrypt(string plainText);
    string Decrypt(string encryptedText);
}

