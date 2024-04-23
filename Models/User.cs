using FinalWork.Models.Enums;

namespace FinalWork.Models;

public class User
{
    public UserType UserType { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}