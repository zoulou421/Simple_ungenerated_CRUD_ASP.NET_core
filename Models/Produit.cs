using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonAppCRUD.Models
{
    public class Produit
    {
        [Key]
        public int ProduitID { get; set; }

        [Required]
        [StringLength(50)]
        [MinLength(4), MaxLength(50)]
        public string Designation { get; set; }

        [Range(10, 1000000)]
        public double Prix { get; set; }
        public int Quantite { get; set; }
        public int Id { get; set; }
        public virtual Category Categorie { get; set; }
    }

    
}
