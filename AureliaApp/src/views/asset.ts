
import { DataEDit } from '../uibases/dataedit';
import {ValidationRules} from 'aurelia-validation';
import { AssetObject } from 'models/assetobject';


export class Asset extends DataEDit<AssetObject>{
    activate(params) {
        //this.routeName='user';
        //this.parentRouteName='users';
        this.controlName = 'UserAssets';
        if (params.id) {
            this.currentId = params.id;
            this.isNew = false;
        } else {
            this.isNew = true;
            this.currentId = 0;
        }
        if(params.email){
            this.parentId=params.email;
        }
        
    }
    override createNewData(): void {
        this.data = new AssetObject(0, 0, '', 0,0,0,0,0);
        this.backUpData = new AssetObject(0, 0, '', 0,0,0,0,0);
    }
    override updateData(data: any): void {
        this.data.MarketCapUsd = data.marketCapUsd;
        this.data.MaxSupply = data.maxSupply;
        this.data.Name = data.name;
        this.data.PriceUsd = data.priceUsd;
        this.data.Rank = data.rank;
        this.data.Supply = data.supply;
        this.data.VolumeUsd24Hr = data.volumeUsd24Hr;
    }
    override validate(): boolean {
        let b: boolean = this.data.Name!=='';
        return b;
    }    
    override copyData(source: AssetObject, target: AssetObject): void {
       
        target.MarketCapUsd = source.MarketCapUsd;
        target.MaxSupply = source.MaxSupply;
        target.Name = source.Name;
        target.PriceUsd = source.PriceUsd;
        target.Rank = source.Rank;
        target.Supply = source.Supply;
        target.VolumeUsd24Hr = source.VolumeUsd24Hr;

    }
    override isChanged(): boolean {
        let b: boolean = this.data.MarketCapUsd != this.backUpData.MarketCapUsd
            || this.data.MaxSupply != this.backUpData.MaxSupply
            || this.data.Name != this.backUpData.Name
            || this.data.PriceUsd != this.backUpData.PriceUsd
            || this.data.Rank != this.backUpData.Rank
            || this.data.Supply!=this.backUpData.Supply
            || this.data.VolumeUsd24Hr!=this.backUpData.VolumeUsd24Hr;
        return b;
    }
    override setValidation() {
        ValidationRules
            .ensure('MarketCapUsd').displayName('marketcapusd').required()
            .ensure('MaxSupply').displayName('maxsupply').required()
            .ensure('Name').displayName('name').required()
            .ensure('PriceUsd').displayName('priceusd').required()
            .ensure('Rank').displayName('rank').required()
            .ensure('Supply').displayName('supply').required()
            .ensure('VolumeUsd24Hr').displayName('volumeusd24hr').required()
            .on(this.data);
    }
}
