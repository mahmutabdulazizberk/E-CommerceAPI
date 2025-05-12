namespace Service.DTOs;

public class AuthenticationDTO
{
    public string? Key { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
}