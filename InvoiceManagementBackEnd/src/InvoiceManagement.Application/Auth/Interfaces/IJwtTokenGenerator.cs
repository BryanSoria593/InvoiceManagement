using System.Collections.Generic;

namespace InvoiceManagement.Application.Auth.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(int userId, string username, string? email, IEnumerable<KeyValuePair<string, string>>? extraClaims = null);
}
