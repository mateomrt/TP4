using System.Reflection;
using TP4.Models.EntityFramework;

namespace TP4.Models.EntityFramework
{
    public partial class Utilisateur
    {
        public override bool Equals(object? obj)
        {
            return  obj is Utilisateur other &&
                    UtilisateurId == other.UtilisateurId &&
                   Nom == other.Nom &&
                   Prenom == other.Prenom &&
                   Mobile == other.Mobile &&
                   Mail == other.Mail &&
                   Pwd == other.Pwd &&
                   Rue == other.Rue &&
                   CodePostal == other.CodePostal &&
                   Ville == other.Ville &&
                   Pays == other.Pays &&
                   Latitude == other.Latitude &&
                   Longitude == other.Longitude;
        }
    }
}
