using ApplicatonProcess.Data.Models;


namespace ApplicatonProcess.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> UserRepository { get; }
        IRepository<Asset> AssetRepository { get; }
        IRepository<UserAsset> UserAssetRepository { get; }

        void Commit();
    }
}
