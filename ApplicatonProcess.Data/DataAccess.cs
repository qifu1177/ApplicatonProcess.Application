using ApplicatonProcess.Data.Interfaces;
using ApplicatonProcess.Data.Models;
using System.Collections.Generic;


namespace ApplicatonProcess.Data
{
    public class DataAccess : IDataAccess
    {        
        private IUnitOfWork _unitOfWork;
       
        public DataAccess(IUnitOfWork unitOfWork)
        {           
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<User> GetUsers()
        {           
            return _unitOfWork.UserRepository.Get();           
        }

        public User GetUser(string email)
        {
            return _unitOfWork.UserRepository.GetByID(email);
        }

        public object InserUser(User user)
        {
            _unitOfWork.UserRepository.Insert(user);
            _unitOfWork.Commit();
            return user.Email;        }

        public void UpdateUser(User user)
        {
            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.Commit();
        }
        
        public void DeleteUser(User user)
        {
            IEnumerable<UserAsset> list = _unitOfWork.UserAssetRepository.Get((item) => item.UserEmail == user.Email);
            foreach (UserAsset userAsset in list)
                _unitOfWork.UserAssetRepository.Delete(userAsset);
            _unitOfWork.UserRepository.Delete(user);
            _unitOfWork.Commit();
        }

        public IEnumerable<Asset> GetAssets(string email)
        {
            IEnumerable<UserAsset> list= _unitOfWork.UserAssetRepository.Get(item => item.UserEmail == email,null, "Asset");
            List<Asset> assets = new List<Asset>();
            foreach (UserAsset userAsset in list)
                assets.Add(userAsset.Asset);
            return assets;
        }

        public Asset GetAsset(int id)
        {
            return _unitOfWork.AssetRepository.GetByID(id);
        }

        public object InserAsset(string email,Asset asset)
        {
            _unitOfWork.AssetRepository.Insert(asset);
            UserAsset userAsset = new UserAsset();
            userAsset.AssetId = asset.Id;
            userAsset.UserEmail = email;
            _unitOfWork.UserAssetRepository.Insert(userAsset);
            _unitOfWork.Commit();
            return asset.Id;
        }

        public void UpdateAsset(Asset asset)
        {
            _unitOfWork.AssetRepository.Update(asset);
            _unitOfWork.Commit();
        }

        public void DeleteAsset(string email,Asset asset)
        {
            IEnumerable<UserAsset> list=_unitOfWork.UserAssetRepository.Get((item) =>  (item.AssetId == asset.Id) && (item.UserEmail == email));
            foreach (UserAsset userAsset in list)
                _unitOfWork.UserAssetRepository.Delete(userAsset);
            _unitOfWork.AssetRepository.Delete(asset);
            _unitOfWork.Commit();
        }
    }
}
