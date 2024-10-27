public class HelpServiceTest: TestingElement 
{
    public HelpServiceTest( ) : this(AppProviderService.GetInstance())
    {
       
    }
    public HelpServiceTest(IServiceProvider provider): base(provider)
    {
        this.Name = this.GetType().GetLabel();
        this.provider = provider;
    }
    public override void OnTest()
    {
        AssertService<HelpService>(help =>
        {
            if (help.GetContents().Count() == 0)
                return false;
            if (help.GetArticles().Count() == 0)
                return false;             
            return true;
        }, 
        "Получение справочной информармации из файла не работает",
        "Получение справочной информармации из файла работает корректно");
    }
}