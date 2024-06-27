using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

public class UserService : IUserService
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;

    public UserService(IConfiguration configuration, ApplicationDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public bool ValidateCredentials(string email, string password)
    {
        var user = _context.Users.SingleOrDefault(u => u.Email == email && u.Senha == password);
        return user != null;
    }

   public string GenerateToken(string email)
{
    var tokenHandler = new JwtSecurityTokenHandler();

    // Obter a chave JWT do IConfiguration
    var jwtKey = _configuration["Jwt:Key"];

    // Verificar se a chave JWT não é nula ou vazia
    if (string.IsNullOrEmpty(jwtKey))
    {
        throw new ApplicationException("JWT key is missing or empty in configuration.");
    }

    // Converter a chave JWT para bytes
    var key = Encoding.ASCII.GetBytes(jwtKey);

    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, email) }),
        Expires = DateTime.UtcNow.AddHours(1),
        Issuer = _configuration["Jwt:Issuer"],
        Audience = _configuration["Jwt:Audience"],
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };

    // Gerar o token JWT
    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
}

}
