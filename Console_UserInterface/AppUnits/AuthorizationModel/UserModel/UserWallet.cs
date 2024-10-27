[Label("Кошелек польщователя")]
public class UserWallet: BaseEntity
{
    [Label("Ссылка на пользователя")]
    public int UserId { get; set; }

    [Label("Сумма на счету")]
    public double Amount { get; set; } = 0;

    [Label("Адрес кашелька")]
    public string Url { get; set; } = "http";

    [Label("Кол-во операций")]
    [NotInput]
    public int Changes { get; set; } = 0;

    [Label("Время последнего изменения")]
    [NotInput]
    public DateTime Updated { get; set; } = DateTime.Now;
}
