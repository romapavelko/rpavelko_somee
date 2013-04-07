namespace rpavelko.Data.Core
{
    public class ContextFactory : Disposable, IContextFactory
    {
        private Context _dataContext;
        private readonly string _cnStringName;

        public ContextFactory(string cnStringName)
        {
            _cnStringName = cnStringName;
        }

        public IContext Get()
        {
            return _dataContext ?? (_dataContext = new Context(_cnStringName));
        }

        protected override void DisposeCore()
        {
            if (_dataContext != null)
            {
                _dataContext.Dispose();
            }
        }
    }
}
