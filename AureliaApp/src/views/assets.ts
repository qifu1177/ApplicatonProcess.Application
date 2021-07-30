import { AssetObject } from '../models/assetobject';
import { DataGrid } from '../uibases/datagrid';
export class Assets extends DataGrid<AssetObject>{

    activate(params) {
        if (params.email) {
            this.parentId = params.email;
            this.routeName = `asset/${this.parentId}`;
        }
        this.controlName = 'UserAssets';
        this.parentRouteName = 'users';        
    }

    override updateDatas(data) {
        this.datas.splice(0,this.datas.length);
        for(let item of data){
            this.datas.push(new AssetObject(item.id,item.rank,item.name,item.supply,item.maxSupply,item.marketCapUsd,item.volumeUsd24Hr,item.priceUsd)) ;        
            
        }
    }
}