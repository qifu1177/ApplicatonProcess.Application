using Autofac;
using ApplicatonProcess.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApplicatonProcess.Data
{
    public class DataModule : Module
    {
        private DataContext _context;
        public DataModule(string dbName)
        {
            var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName: dbName).Options;
            _context = new DataContext(options);
        }
        protected override void Load(ContainerBuilder builder)
        {
            //base.Load(builder);
            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IRepository<>)).WithParameter("context", _context).InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().WithParameter("context", _context);
            builder.RegisterType<DataAccess>().As<IDataAccess>();
        }
    }
}
