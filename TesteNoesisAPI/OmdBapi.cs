using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TesteNoesisAPI
{
    [TestClass]
    public class OmdBapi : BaseClass
    {

        private string idMovie = "tt1285016";

        [TestMethod]
        public void ConsultarFilmeExistente()
        {
            Consulta(idMovie, "movie");

            Assert.IsTrue(ValidaTitulo("The Social Network"));
            Assert.IsTrue(ValidaAno("2010"));
            Assert.IsTrue(ValidaIdioma("English, French"));

        }

        [TestMethod]
        public void ConsultarFilmeInexistente()
        {
            Consulta("65dsaf", "movie");
            
            Assert.IsTrue(ValidaConsultaFilmeInexistente("False", "Incorrect IMDb ID."));
        }
    }
}
