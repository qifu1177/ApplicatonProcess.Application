export class AssetObject {
    constructor(id:number,rank:number,name:string,supply:number,maxSupply:number,marketCapUsd:number,volumeUsd24Hr:number,priceUsd:number){
        this.Id=id;
        this.Rank=rank;
        this.Name=name;
        this.Supply=supply;
        this.MaxSupply=maxSupply;
        this.MarketCapUsd=marketCapUsd;
        this.VolumeUsd24Hr=volumeUsd24Hr;
        this.PriceUsd=priceUsd;
    }
    Id: number;
    Rank: number;
    Name: string;
    Supply: number;
    MaxSupply: number;
    MarketCapUsd: number;
    VolumeUsd24Hr: number;
    PriceUsd: number;
}