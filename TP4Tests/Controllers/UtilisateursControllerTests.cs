using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP4.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TP4.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace TP4.Controllers.Tests
{
    [TestClass()]
    public class UtilisateursControllerTests
    {
        public UtilisateursController controller;
        public FilmRatingsDBContext ctx;
        [TestInitialize]
        public void UtilisateursControllerTestsConstructor()
        {
            var builder = new DbContextOptionsBuilder<FilmRatingsDBContext>().UseNpgsql("Server=localhost;port=5432;Database=FilmRatingsDB; uid=postgres; password=postgres;"); // Chaine de connexion à mettre dans les ( )
            ctx = new FilmRatingsDBContext(builder.Options);
            controller = new UtilisateursController(ctx);

        }

        [TestMethod()]
        public void GetUtilisateursTest()
        {
            var ctx = new FilmRatingsDBContext();
            var expectedList = ctx.Utilisateurs.ToList();

            Task<ActionResult<IEnumerable<Utilisateur>>> listUser = controller.GetUtilisateurs();
            ActionResult<IEnumerable<Utilisateur>> resultat = listUser.Result;
            List<Utilisateur> listSerie = resultat.Value.ToList();


            CollectionAssert.AreEqual(expectedList, listSerie);
        }

        [TestMethod()]
        public void GetUtilisateurByIdTest()
        {
            Utilisateur expectedUser = ctx.Utilisateurs.Where(c => c.UtilisateurId == 1).FirstOrDefault();

            Task<ActionResult<Utilisateur>> zz = controller.GetUtilisateurById(1) ;
            ActionResult<Utilisateur> resultat = zz.Result;
            Utilisateur user = resultat.Value;


            var result = controller.GetUtilisateurById(1000);
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult<Utilisateur>>));
            ActionResult<Utilisateur> utilisateur = (ActionResult<Utilisateur>)result.Result;
            var serieResult = utilisateur.Result;


            Assert.IsInstanceOfType(serieResult, typeof(NotFoundResult));

            Assert.AreEqual(expectedUser, user);

        }

        [TestMethod()]
        public void GetUtilisateurByEmailTest()
        {
            Utilisateur expectedUser = ctx.Utilisateurs.Where(c => c.Mail == "clilleymd@last.fm").FirstOrDefault();

            Task<ActionResult<Utilisateur>> zz = controller.GetUtilisateurByEmail("clilleymd@last.fm");
            ActionResult<Utilisateur> resultat = zz.Result;
            Utilisateur user = resultat.Value;


            var result = controller.GetUtilisateurByEmail("zpfo ezzefohzfiuhzef izuef izehf zeoi");
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult<Utilisateur>>));
            ActionResult<Utilisateur> utilisateur = (ActionResult<Utilisateur>)result.Result;
            var serieResult = utilisateur.Result;


            Assert.IsInstanceOfType(serieResult, typeof(NotFoundResult));

            Assert.AreEqual(expectedUser, user);
        }

        [TestMethod()]
        public void PostUtilisateurTest()
        {
            // Arrange
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);
            // Le mail doit être unique donc 2 possibilités :
            // 1. on s'arrange pour que le mail soit unique en concaténant un random ou un timestamp
            // 2. On supprime le user après l'avoir créé. Dans ce cas, nous avons besoin d'appeler la méthode DELETE de l’API ou remove du DbSet.
            Utilisateur userAtester = new Utilisateur()
            {
                Nom = "MACHIN",
                Prenom = "Luc",
                Mobile = "0606070809",
                Mail = "machin" + chiffre + "@gmail.com",
                Pwd = "Toto1234!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };
            // Act
            var result = controller.PostUtilisateur(userAtester).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout
            // Assert
            Utilisateur? userRecupere = ctx.Utilisateurs.Where(u => u.Mail.ToUpper() == userAtester.Mail.ToUpper()).FirstOrDefault(); // On récupère l'utilisateur créé directement dans la BD grace à son mail unique
            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            userAtester.UtilisateurId = userRecupere.UtilisateurId;
            Assert.AreEqual(userRecupere, userAtester, "Utilisateurs pas identiques");

            
        }
        [TestMethod()]
        public void PutUtilisateurTest()
        {
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);
            
            Utilisateur userAtester = new Utilisateur()
            {
                UtilisateurId = 13,
                Nom = "MACHIN",
                Prenom = "Luc",
                Mobile = "0606070809",
                Mail = "machin" + chiffre + "@gmail.com",
                Pwd = "Toto1234!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };
            // Act
            var result = controller.PutUtilisateur(13 ,userAtester).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout
            // Assert
            Utilisateur? userRecupere = ctx.Utilisateurs.Where(u => u.UtilisateurId == 13).FirstOrDefault(); // On récupère l'utilisateur créé directement dans la BD grace à son mail unique
            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            userAtester.UtilisateurId = userRecupere.UtilisateurId;
            Assert.AreEqual(userRecupere, userAtester, "Utilisateurs pas identiques");
        }

        [TestMethod()]
        public void DeleteUtilisateurTest()
        {
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);

            Utilisateur userAtester = new Utilisateur()
            {
                Nom = "MARTIN",
                Prenom = "Matéo",
                Mobile = "0606070809",
                Mail = "machin" + chiffre + "@gmail.com",
                Pwd = "Toto1234!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };
            
            ctx.Utilisateurs.Add(userAtester);
            ctx.SaveChanges();

            int id = ctx.Utilisateurs.Where(x => x.Mail == userAtester.Mail).FirstOrDefault().UtilisateurId;

            var delete = controller.DeleteUtilisateur(id); // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            Thread.Sleep(1000);

            Utilisateur? result = ctx.Utilisateurs.Where(x => x.UtilisateurId == id).FirstOrDefault();

            Assert.IsNull(result);

        }



    }
}