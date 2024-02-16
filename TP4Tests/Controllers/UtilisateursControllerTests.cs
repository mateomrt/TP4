using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP4.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TP4.Models.EntityFramework;

namespace TP4.Controllers.Tests
{
    [TestClass()]
    public class UtilisateursControllerTests
    {
        public UtilisateursController controller;
        [TestInitialize]
        public void UtilisateursControllerTestsConstructor()
        {
            var builder = new DbContextOptionsBuilder<FilmRatingsDBContext>().UseNpgsql("Server=localhost;port=5432;Database=FilmRatingsDB; uid=postgres; password=postgres;"); // Chaine de connexion à mettre dans les ( )
            FilmRatingsDBContext context = new FilmRatingsDBContext(builder.Options);
            controller = new UtilisateursController(context);

        }
        [TestMethod()]
        public void UtilisateursControllerTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetUtilisateursTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetUtilisateurByIdTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetUtilisateurByEmailTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PutUtilisateurTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PostUtilisateurTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteUtilisateurTest()
        {
            Assert.Fail();
        }
    }
}