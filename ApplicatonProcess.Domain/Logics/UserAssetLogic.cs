using ApplicatonProcess.Data;
using ApplicatonProcess.Data.Interfaces;
using ApplicatonProcess.Data.Models;
using ApplicatonProcess.Domain.Interfaces;
using ApplicatonProcess.Domain.Models;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicatonProcess.Domain.Logics
{
    public class UserAssetLogic : AbstractLogic<AssetRequest, AssetResponse>
    {
        public UserAssetLogic(IJsonStringLocalizer stringLocalizer, IDataAccess dataAccess) :base(stringLocalizer, dataAccess)
        {           
        }
        public override IEnumerable<AssetResponse> LoadResponses(object parentId)
        {
            try
            {
                List<AssetResponse> list = _dataAccess.GetAssets(parentId.ToString())
                    .Select(item => new AssetResponse { Id = item.Id, Rank = item.Rank, Name = item.Name, Supply = item.Supply, MarketCapUsd = item.MarketCapUsd, MaxSupply = item.MaxSupply, PriceUsd = item.PriceUsd, VolumeUsd24Hr = item.VolumeUsd24Hr }).ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(_stringLocalizer[_currentLanguage, "loadAssetsError"], ex);
            }
        }
        public override AssetResponse LoadResponse(object id)
        {
            try
            {
                Asset item = _dataAccess.GetAsset(Convert.ToInt32(id));
                return new AssetResponse { Id = item.Id, Rank = item.Rank, Name = item.Name, Supply = item.Supply, MarketCapUsd = item.MarketCapUsd, MaxSupply = item.MaxSupply, PriceUsd = item.PriceUsd, VolumeUsd24Hr = item.VolumeUsd24Hr };
            }
            catch (Exception ex)
            {
                throw new Exception(_stringLocalizer[_currentLanguage, "loadAssetError"], ex);
            }
        }

        public override object Insert(AssetRequest request, object parentId)
        {
            try
            {
                return _dataAccess.InserAsset(parentId.ToString(), new Asset { Id = request.Id, Rank = request.Rank, Name = request.Name, Supply = request.Supply, MarketCapUsd = request.MarketCapUsd, MaxSupply = request.MaxSupply, PriceUsd = request.PriceUsd, VolumeUsd24Hr = request.VolumeUsd24Hr });
            }
            catch (Exception ex)
            {
                throw new Exception(_stringLocalizer[_currentLanguage, "saveAssetError"], ex);
            }
        }
        public override void Update(object id, AssetRequest request)
        {

            Asset asset = _dataAccess.GetAsset(Convert.ToInt32(id));
            if (asset == null)
                throw new ArgumentException(_stringLocalizer[_currentLanguage, "assetNotFound"]);
            try
            {
                asset.Rank = request.Rank;
                asset.Name = request.Name;
                asset.Supply = request.Supply;
                asset.MarketCapUsd = request.MarketCapUsd;
                asset.MaxSupply = request.MaxSupply;
                asset.PriceUsd = request.PriceUsd;
                asset.VolumeUsd24Hr = request.VolumeUsd24Hr;
                _dataAccess.UpdateAsset(asset);
            }
            catch (Exception ex)
            {
                throw new Exception(_stringLocalizer[_currentLanguage, "updateAssetError"], ex);
            }
        }
        public override void Delete(object id, object parentId)
        {
            Asset asset = _dataAccess.GetAsset(Convert.ToInt32(id));
            if (asset == null)
                throw new ArgumentException(_stringLocalizer[_currentLanguage, "assetNotFound"]);
            try
            {
                _dataAccess.DeleteAsset(parentId.ToString(), asset);
            }
            catch (Exception ex)
            {
                throw new Exception(_stringLocalizer[_currentLanguage, "delteAssetError"], ex);
            }
        }
    }
}
