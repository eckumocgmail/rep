
public class Login
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Captcha { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
    public string Result { get; set; }
}
public class UserLogin: EventsTable<Login>
{
         
}
 