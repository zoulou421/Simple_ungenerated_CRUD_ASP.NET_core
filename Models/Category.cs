using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MonAppCRUD.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name{ get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public virtual ICollection<Produit>? Produits { get; set; }

    }
}
