using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ApplicatonProcess.Data.Models
{
    public class UserAsset
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int AssetId { get; set; }
        public string UserEmail { get; set; }
        [ForeignKey("UserEmail")]
        public User User { get; set; }
        [ForeignKey("AssetId")]
        public Asset Asset { get; set; }
    }
}
