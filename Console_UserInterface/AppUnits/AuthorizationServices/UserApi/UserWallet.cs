public class UserWallet: BaseEntity
{
    public int UserId { get; set; } 
 
    public double Amount { get; set; }

    public string Url { get; set; }
    public int Changes { get; set; }
    public DateTime Updated { get; set; }
}
