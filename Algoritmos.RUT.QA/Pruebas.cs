using NUnit.Framework;
using System.Windows.Forms;

namespace Algoritmos.RUT.QA
{
    [TestFixture]
    public class Pruebas
    {
        [Test]
        [TestCase("19078446-0", true)]
        [TestCase("15E125-7", true)]
        [TestCase("1457031.7", false)]
        [TestCase("275168-7-9",false)]
        [TestCase("19078446-05", false)]
        public static void IngresarFormato(string rut, bool esperado)
        {
            Assert.AreEqual(esperado, Program.FormatoVálido(rut));
            if (esperado == Program.FormatoVálido(rut))
                MessageBox.Show("Formato inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        [Test]
        [TestCase("19078446-0", true)]
        [TestCase("A158412-3", false)]
        [TestCase("KJAJ-K", false)]
        public static void IngresaSoloNúmero(string rut, bool esperado)
        {
            Assert.AreEqual(esperado, Program.SoloNúmero(rut.Split('-')[0]));
        }
    }
}