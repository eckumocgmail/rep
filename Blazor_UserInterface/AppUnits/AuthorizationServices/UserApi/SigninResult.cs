public class SigninResult
{
    public bool IsLockedOut { get; set; }
    public bool RequiresTwoFactor { get; set; }
    public bool Succeeded { get; set; }
}