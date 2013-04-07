namespace rpavelko.Data.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IContextFactory _contextFactory;
        private IContext _context;

        public UnitOfWork(IContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        protected IContext DataContext
        {
            get { return _context ?? (_context = _contextFactory.Get()); }
        }

        public void Commit()
        {
            DataContext.Commit();
        }
    }
}
