namespace Console_UserInterface.Location
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