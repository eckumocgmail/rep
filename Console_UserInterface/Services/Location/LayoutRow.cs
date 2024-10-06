namespace Console_UserInterface.Services.Location
{




    public class LayoutRow : BaseEntity
    {
        private LocationDbContext.PageComponent[] children;

        public LayoutRow(LocationDbContext.PageComponent[] children)
        {
            this.children = children;
        }
    }
}