using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP4.Models.EntityFramework;
using TP4.Models.Repository;

namespace TP4.Models.DataManager
{
    public class UtilisateurManager : IDataRepository<Utilisateur>
    {
        readonly FilmRatingsDBContext? filmsDbContext;
        public UtilisateurManager() { }
        public UtilisateurManager(FilmRatingsDBContext context)
        {
            filmsDbContext = context;
        }
        public ActionResult<IEnumerable<Utilisateur>> GetAll()
        {
            return filmsDbContext.Utilisateurs.ToList();
        }
        public async Task<ActionResult<Utilisateur>> GetById(int id)
        {
            return await filmsDbContext.Utilisateurs.FirstOrDefault(u => u.UtilisateurId == id);
        }
        public async Task<ActionResult<Utilisateur>> GetByStringAsync(string mail)
        {
            return await filmsDbContext.Utilisateurs.FirstOrDefaultAsync(u => u.Mail.ToUpper() == mail.ToUpper());
        }
        public void Add(Utilisateur entity)
        {
            filmsDbContext.Utilisateurs.Add(entity);
            filmsDbContext.SaveChanges();
        }
        public void Update(Utilisateur utilisateur, Utilisateur entity)
        {
            filmsDbContext.Entry(utilisateur).State = EntityState.Modified;
            utilisateur.UtilisateurId = entity.UtilisateurId;
            utilisateur.Nom = entity.Nom;
            utilisateur.Prenom = entity.Prenom;
            utilisateur.Mail = entity.Mail;
            utilisateur.Rue = entity.Rue;
            utilisateur.CodePostal = entity.CodePostal;
            utilisateur.Ville = entity.Ville;
            utilisateur.Pays = entity.Pays;
            utilisateur.Latitude = entity.Latitude;
            utilisateur.Longitude = entity.Longitude;
            utilisateur.Pwd = entity.Pwd;
            utilisateur.Mobile = entity.Mobile;
            utilisateur.NotesUtilisateur = entity.NotesUtilisateur;
            filmsDbContext.SaveChanges();
        }
        public void Delete(Utilisateur utilisateur)
        {
            filmsDbContext.Utilisateurs.Remove(utilisateur);
            filmsDbContext.SaveChanges();
        }
    }
}
