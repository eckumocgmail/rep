[Label("Операция перевода денежных средств")]
public class TransferTransaction: BaseEntity
{

    [Label("Ссылка на источник списания")]
    public int SourceWalletId { get; set; }

    [Label("Ссылка на объект пополнения")]
    public int TargetWalletId { get; set; }

    [InputPositiveInt]
    [InputDecimal]
    [Label("Сумма перевода")]
    public double TransactionAmount { get; set; }       
}
