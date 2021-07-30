using ApplicatonProcess.Data.Interfaces;
using ApplicatonProcess.Data.Models;


namespace ApplicatonProcess.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext _context;
        private IRepository<User> _userResitory;
        private IRepository<Asset> _assetResitory;
        private IRepository<UserAsset> _userAssetResitory;
        public UnitOfWork(DataContext context,IRepository<User> userResitory, IRepository<Asset> assetResitory, IRepository<UserAsset> userAssetResitory)
        {
            _context = context;
            _userResitory = userResitory;
            _assetResitory = assetResitory;
            _userAssetResitory = userAssetResitory;
        }

        public IRepository<User> UserRepository
        {
            get
            {                
                return _userResitory ;
            }
        }

        public IRepository<Asset> AssetRepository
        {
            get
            {
                return _assetResitory ;
            }
        }

        public IRepository<UserAsset> UserAssetRepository
        {
            get
            {
                return _userAssetResitory ;
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
