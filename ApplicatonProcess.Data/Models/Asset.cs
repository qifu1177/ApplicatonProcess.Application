using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ApplicatonProcess.Data.Models
{
    public class Asset
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Rank { get; set; }
        public string Name { get; set; }
        public double Supply { get; set; }
        public double MaxSupply { get; set; }
        public double MarketCapUsd { get; set; }
        public double VolumeUsd24Hr { get; set; }
        public double PriceUsd { get; set; }
    }
}
