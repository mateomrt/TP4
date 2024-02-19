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
using TP4.Models.Repository;
using TP4.Models.DataManager;
using Moq;
using static System.Net.Mime.MediaTypeNames;

namespace TP4.Controllers.Tests
{
    [TestClass()]
    public class UtilisateursControllerTests
    {
        private UtilisateursController controller;
        private FilmRatingsDBContext ctx;
        private IDataRepository<Utilisateur> dataRepository;
        [TestInitialize]
        public void UtilisateursControllerTestsConstructor()
        {
            var builder = new DbContextOptionsBuilder<FilmRatingsDBContext>().UseNpgsql("Server=localhost;port=5432;Database=FilmRatingsDB; uid=postgres;password=postgres;");
            ctx = new FilmRatingsDBContext(builder.Options);
            dataRepository = new UtilisateurManager(ctx);
            controller = new UtilisateursController(dataRepository);

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

        /*[TestMethod()]
        public void GetUtilisateurByIdTest()
        {
            Utilisateur expectedUser = ctx.Utilisateurs.FirstOrDefault(c => c.UtilisateurId == 1);

            Task<ActionResult<Utilisateur>> zz = controller.GetUtilisateurById(1) ;
            ActionResult<Utilisateur> resultat = zz.Result;
            Utilisateur user = resultat.Value;

            Assert.AreEqual(expectedUser, user);

            var result = controller.GetUtilisateurById(1000);
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult<Utilisateur>>));
            ActionResult<Utilisateur> utilisateur = (ActionResult<Utilisateur>)result.Result;
            var serieResult = utilisateur.Result;


            Assert.IsInstanceOfType(serieResult, typeof(NotFoundResult));
        }*/

        [TestMethod]
        public void GetUtilisateurById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Utilisateur user = new Utilisateur
            {
                 UtilisateurId = 1,
                 Nom = "Calida",
                 Prenom = "Lilley",
                 Mobile = "0653930778",
                 Mail = "clilleymd@last.fm",
                 Pwd = "Toto12345678!",
                 Rue = "Impasse des bergeronnettes",
                 CodePostal = "74200",
                 Ville = "Allinges",
                 Pays = "France",
                 Latitude = 46.344795F,
                 Longitude = 6.4885845F
            };
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(user);
            var userController = new UtilisateursController(mockRepository.Object);
            // Act
            var actionResult = userController.GetUtilisateurById(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(user, actionResult.Value as Utilisateur);
        }

        [TestMethod]
        public void GetUtilisateurById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            var userController = new UtilisateursController(mockRepository.Object);
            // Act
            var actionResult = userController.GetUtilisateurById(1000).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        /*[TestMethod()]
        public void GetUtilisateurByEmailTest()
        {
            Utilisateur expectedUser = ctx.Utilisateurs.FirstOrDefault(c => c.Mail == "clilleymd@last.fm");

            Task<ActionResult<Utilisateur>> zz = controller.GetUtilisateurByEmail("clilleymd@last.fm");
            ActionResult<Utilisateur> resultat = zz.Result;
            Utilisateur user = resultat.Value;


            var result = controller.GetUtilisateurByEmail("zpfo ezzefohzfiuhzef izuef izehf zeoi");
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult<Utilisateur>>));
            ActionResult<Utilisateur> utilisateur = (ActionResult<Utilisateur>)result.Result;
            var serieResult = utilisateur.Result;


            Assert.IsInstanceOfType(serieResult, typeof(NotFoundResult));

            Assert.AreEqual(expectedUser, user);
        }*/

        [TestMethod]
        public void GetUtilisateurByEmail_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Utilisateur user = new Utilisateur
            {
                UtilisateurId = 1,
                Nom = "Calida",
                Prenom = "Lilley",
                Mobile = "0653930778",
                Mail = "clilleymd@last.fm",
                Pwd = "Toto12345678!",
                Rue = "Impasse des bergeronnettes",
                CodePostal = "74200",
                Ville = "Allinges",
                Pays = "France",
                Latitude = 46.344795F,
                Longitude = 6.4885845F
            };
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            mockRepository.Setup(x => x.GetByStringAsync("clilleymd@last.fm").Result).Returns(user);
            var userController = new UtilisateursController(mockRepository.Object);
            // Act
            var actionResult = userController.GetUtilisateurByEmail("clilleymd@last.fm").Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(user, actionResult.Value as Utilisateur);
        }

        [TestMethod]
        public void GetUtilisateurByEmail_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            var userController = new UtilisateursController(mockRepository.Object);
            // Act
            var actionResult = userController.GetUtilisateurByEmail("oepzifhezif heziof hezçiof ze").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        // AVANT MOCK
        /*[TestMethod()]
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
            Utilisateur? userRecupere = ctx.Utilisateurs.FirstOrDefault(u => u.Mail.ToUpper() == userAtester.Mail.ToUpper()); // On récupère l'utilisateur créé directement dans la BD grace à son mail unique
            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            userAtester.UtilisateurId = userRecupere.UtilisateurId;
            Assert.AreEqual(userRecupere, userAtester, "Utilisateurs pas identiques");

            
        }*/
        [TestMethod]
        public void PostUtilisateurTest_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            var userController = new UtilisateursController(mockRepository.Object);
            Utilisateur user = new Utilisateur
            {
                Nom = "POISSON",
                Prenom = "Pascal",
                Mobile = "1",
                Mail = "poisson@gmail.com",
                Pwd = "Toto12345678!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };
            // Act
            var actionResult = userController.PostUtilisateur(user).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Utilisateur>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Utilisateur), "Pas un Utilisateur");
            user.UtilisateurId = ((Utilisateur)result.Value).UtilisateurId;
            Assert.AreEqual(user, (Utilisateur)result.Value, "Utilisateurs pas identiques");
        }

        /*[TestMethod()]
        public void PutUtilisateurTest()
        {
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);
            
            Utilisateur userAtester = new Utilisateur()
            {
                UtilisateurId = 14,
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
            var result = controller.PutUtilisateur(14 ,userAtester).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout
            // Assert
            
            Utilisateur? userRecupere = ctx.Utilisateurs.FirstOrDefault(u => u.UtilisateurId == 14); // On récupère l'utilisateur créé directement dans la BD grace à son mail unique
            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            userAtester.UtilisateurId = userRecupere.UtilisateurId;
            Assert.AreEqual(userRecupere, userAtester, "Utilisateurs pas identiques");
        }*/

        [TestMethod()]
        public void PutUtilisateurTest_AvecMoq()
        {
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);

            Utilisateur userAtester = new Utilisateur()
            {
                UtilisateurId = 14,
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
            var result = controller.PutUtilisateur(14, userAtester).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout
                                                                            // Assert

            Utilisateur? userRecupere = ctx.Utilisateurs.FirstOrDefault(u => u.UtilisateurId == 14); // On récupère l'utilisateur créé directement dans la BD grace à son mail unique
            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            userAtester.UtilisateurId = userRecupere.UtilisateurId;
            Assert.AreEqual(userRecupere, userAtester, "Utilisateurs pas identiques");
        }

        /*[TestMethod()]
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

            int id = ctx.Utilisateurs.FirstOrDefault(x => x.Mail == userAtester.Mail).UtilisateurId;

            var delete = controller.DeleteUtilisateur(id); // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            Thread.Sleep(1000);

            Utilisateur? result = ctx.Utilisateurs.FirstOrDefault(x => x.UtilisateurId == id);

            Assert.IsNull(result);
        }*/


        [TestMethod]
        public void DeleteUtilisateurTest_AvecMoq()
        {
            // Arrange
            Utilisateur user = new Utilisateur
            {
                UtilisateurId = 1,
                Nom = "Calida",
                Prenom = "Lilley",
                Mobile = "0653930778",
                Mail = "clilleymd@last.fm",
                Pwd = "Toto12345678!",
                Rue = "Impasse des bergeronnettes",
                CodePostal = "74200",
                Ville = "Allinges",
                Pays = "France",
                Latitude = 46.344795F,
                Longitude = 6.4885845F
            };
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(user);
            var userController = new UtilisateursController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteUtilisateur(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }



    }
}