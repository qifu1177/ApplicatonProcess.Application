using ApplicatonProcess.Domain.Interfaces;


namespace ApplicatonProcess.Domain.Models.Examples
{

    public class AssetRequestExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new AssetRequest
            {
                Id = 1,
                Rank = 1,
                Name = "Bitcoin",
                Supply = 18743737.000,
                MaxSupply = 21000000.00000,
                MarketCapUsd = 594947501468.31051,
                VolumeUsd24Hr = 10634210764.65155780,
                PriceUsd = 31741.135797429857
            };
        }
    }
}
