public class TransferTransaction: BaseEntity
{

    public int SourceWalletId { get; set; }
    public int TargetWalletId { get; set; }
    public double TransactionAmount { get; set; }       
}
