using ApplicatonProcess.Data;
using ApplicatonProcess.Data.Interfaces;
using ApplicatonProcess.Data.Models;
using ApplicatonProcess.Domain.Interfaces;
using ApplicatonProcess.Domain.Models;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ApplicatonProcess.Domain.Logics
{
    public class UserLogic : AbstractLogic<UserRequest, UserResponse>
    {
        public UserLogic(IJsonStringLocalizer stringLocalizer, IDataAccess dataAccess) : base(stringLocalizer, dataAccess)
        {
        }
        public override IEnumerable<UserResponse> LoadResponses(object parentId = null)
        {
            try
            {
                List<UserResponse> list = _dataAccess.GetUsers()
                .Select(item => new UserResponse { Address = item.Address, Email = item.Email, Age = item.Age, FirstName = item.FirstName, LastName = item.LastName }).ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(_stringLocalizer[_currentLanguage, "loadUsersError"], ex);
            }
        }

        public override UserResponse LoadResponse(object id)
        {
            try
            {
                User item = _dataAccess.GetUser(id.ToString());
                return new UserResponse { Address = item.Address, Email = item.Email, Age = item.Age, FirstName = item.FirstName, LastName = item.LastName };
            }
            catch (Exception ex)
            {
                throw new Exception(_stringLocalizer[_currentLanguage, "loadUserError"], ex);
            }
        }

        public override object Insert(UserRequest request, object parentId = null)
        {
            try
            {
                return _dataAccess.InserUser(new User { Address = request.Address, Email = request.Email, Age = request.Age, FirstName = request.FirstName, LastName = request.LastName });
            }
            catch (Exception ex)
            {
                throw new Exception(_stringLocalizer[_currentLanguage, "saveUserError"], ex);
            }
        }

        public override void Update(object id, UserRequest request)
        {
            User user = _dataAccess.GetUser(id.ToString());
            if (user == null)
                throw new ArgumentException(_stringLocalizer[_currentLanguage, "emailNotFound"]);
            try
            {
                user.Address = request.Address;
                user.Age = request.Age;
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                _dataAccess.UpdateUser(user);
            }
            catch (Exception ex)
            {
                throw new Exception(_stringLocalizer[_currentLanguage, "updateUserError"], ex);
            }
        }

        public override void Delete(object id, object parentId = null)
        {
            User user = _dataAccess.GetUser(id.ToString());
            if (user == null)
                throw new ArgumentException(_stringLocalizer[_currentLanguage, "emailNotFound"]);
            try
            {
                _dataAccess.DeleteUser(user);
            }
            catch (Exception ex)
            {
                throw new Exception(_stringLocalizer[_currentLanguage, "deleteUserError"], ex);
            }
        }
    }
}
