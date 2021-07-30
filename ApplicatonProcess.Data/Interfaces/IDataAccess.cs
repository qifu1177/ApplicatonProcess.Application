using ApplicatonProcess.Data.Models;
using System.Collections.Generic;


namespace ApplicatonProcess.Data.Interfaces
{
    public interface IDataAccess
    {
        IEnumerable<User> GetUsers();
        User GetUser(string email);
        object InserUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        IEnumerable<Asset> GetAssets(string email);
        Asset GetAsset(int id);
        object InserAsset(string email, Asset asset);

        void UpdateAsset(Asset asset);

        void DeleteAsset(string email, Asset asset);
        
    }
}
