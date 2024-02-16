using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TP4.Models.EntityFramework
{
    [Table("t_j_notation_not")]
    public partial class Notation
    {
        public Notation()
        {

        }

        [Key]
        [ForeignKey("Utilisateur")]
        [InverseProperty("UtilisateurId")]
        [Column("utl_id")]
        public int UtilisateurId { get; set; }
        
        [Key]
        [ForeignKey("Film")]
        [InverseProperty("FilmId")]
        [Column("flm_id")]
        [Required]
        public int FilmId { get; set; }

        [Column("not_note")]
        [Required]
        public string Resume { get; set; }

        [InverseProperty("Utilisateur")]
        public virtual ICollection<Utilisateur> UtilisateurNotant { get; set; }
        
        [InverseProperty("Film")]
        public virtual ICollection<Film> FilmNote { get; set; }

    }
}
