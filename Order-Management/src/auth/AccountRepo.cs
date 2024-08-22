using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using order_management.database;
using Microsoft.EntityFrameworkCore;

namespace order_management.auth;

public class AccountRepo : IAccountRepo
{
    private readonly OrderManagementContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;

    public AccountRepo(OrderManagementContext context, IMapper mapper, IConfiguration config)
    {
        _context = context;
        _mapper = mapper;
        _config = config;
    }
    public async Task<LoginResponse> Login(LoginDTO loginDTO)
    {
        var user = await FindUserByEmail(loginDTO.Email);
        if (user != null)
        {
            bool verifyPassword = BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password);
            if (!verifyPassword)
                return new LoginResponse(false, null, "password incorrect");

            string token = GenerateToken(user);
            return new LoginResponse(true, token, null);
        }
        return new LoginResponse(false, null, "user does not exist");
    }

    public async Task<User> FindUserByEmail(string email)
    {
        email = email.ToLower();
        return await _context.Users.FirstOrDefaultAsync(_ => _.Email.ToLower() == email);
    }
    private string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var userClaims = new[]
        {
            new Claim("Fullname",user.Name),
            new Claim(ClaimTypes.Name,user.Email),
            new Claim(ClaimTypes.Email,user.Email),

        };
        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: userClaims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
            );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    public async Task<Response> Register(RegisterDTO registerDTO)
    {
        var user = await FindUserByEmail(registerDTO.Email);
        if (user != null)
            return new Response(false, "User already registered");
        var addUser = _mapper.Map<User>(registerDTO);
        addUser.Password = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password);
        _context.Users.Add(addUser);
        await _context.SaveChangesAsync();
        return new Response(true, "Created");

    }
}
