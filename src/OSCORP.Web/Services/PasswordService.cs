using Microsoft.AspNetCore.Identity;

namespace OSCORP.Web.Services;

// Acepta temporalmente las contraseñas antiguas del SQL y las migra a hash al iniciar sesión.
public class PasswordService
{
    private readonly PasswordHasher<string> _hasher = new();

    public string Hash(string password) => _hasher.HashPassword("OSCORP", password);

    public bool Verify(string storedValue, string password, out bool needsUpgrade)
    {
        needsUpgrade = false;
        if (string.IsNullOrWhiteSpace(storedValue)) return false;

        if (storedValue == password)
        {
            needsUpgrade = true;
            return true;
        }

        var result = _hasher.VerifyHashedPassword("OSCORP", storedValue, password);
        return result != PasswordVerificationResult.Failed;
    }
}
