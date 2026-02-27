using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace IdentityLib.Helpers.Cryptography;

/// <summary>
/// Helper для криптографических операций
/// </summary>
public static class CryptographyHelper
{
    /// <summary>
    /// Генерирует криптостойкую соль заданного размера (в байтах)
    /// </summary>
    /// <param name="saltSize">Размер соли в байтах (по умолчанию 16 байт = 128 бит)</param>
    /// <returns>Соль в формате Base64</returns>
    public static string GenerateSalt(int saltSize = 16)
    {
        var buffer = new byte[saltSize];
        RandomNumberGenerator.Fill(buffer);
        return Convert.ToBase64String(buffer);
    }
    
    /// <summary>
    /// Проверяет, соответствует ли пароль хэшу
    /// </summary>
    public static bool VerifyPassword(
        string password,
        string salt,
        string expectedHash,
        int hashSize = 32,
        int degreeOfParallelism = 4,
        int memorySize = 65536,
        int iterations = 4)
    {
        var computedHash = HashPassword(password, salt, hashSize, degreeOfParallelism, memorySize, iterations);
        return CryptographicOperations.FixedTimeEquals(Encoding.UTF8.GetBytes(computedHash), Encoding.UTF8.GetBytes(expectedHash));
    }
    
    /// <summary>
    /// Хэширует пароль с использованием Argon2id
    /// </summary>
    /// <param name="password">Пароль в открытом виде</param>
    /// <param name="salt">Соль в формате Base64</param>
    /// <param name="hashSize">Размер выходного хэша в байтах (по умолчанию 32)</param>
    /// <param name="degreeOfParallelism">Степень параллелизма (число потоков)</param>
    /// <param name="memorySize">Размер памяти в килобайтах</param>
    /// <param name="iterations">Количество итераций</param>
    /// <returns>Хэш в формате Base64</returns>
    private static string HashPassword(
        string password, 
        string salt, 
        int hashSize = 32,
        int degreeOfParallelism = 4,
        int memorySize = 65536,
        int iterations = 4)
    {
        var saltBytes = Convert.FromBase64String(salt);
        
        using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            DegreeOfParallelism = degreeOfParallelism,
            MemorySize = memorySize,
            Iterations = iterations,
            Salt = saltBytes
        };

        var hash = argon2.GetBytes(hashSize);
        return Convert.ToBase64String(hash);
    }
}