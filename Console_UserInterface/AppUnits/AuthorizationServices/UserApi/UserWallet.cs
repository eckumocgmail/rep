public class UserWallet: BaseEntity
{
    public int UserId { get; set; }

    public double Amount { get; set; } = 0;

    public string Url { get; set; } = "http";
    public int Changes { get; set; } = 0;
    public DateTime Updated { get; set; } = DateTime.Now;
}
