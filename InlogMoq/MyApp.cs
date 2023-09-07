using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using InlogMoq;

namespace MyApp;
public class Program
{
    [Fact]
    void TestProfielPagina()
    {
        // --- Arrange ---
        // Maak de mock aan
        MijnMoq<IInlogSysteem> mock = new MijnMoq<IInlogSysteem>();

        // Configureer de mock om iets te doen
        mock.Setup("IngelogdeGebruiker", () => "Bob");

        // Maak de mock aan
        IInlogSysteem obj = mock.Object;

        // Maak het te testen object aan
        ProfielPagina sut = new ProfielPagina(obj);

        // --- Act ---
        string resultaat = sut.Tekst();

        // --- Assert ---
        Assert.Contains("Bob", resultaat);

        // Roep de Intercept-methode aan om het aantal keren bij te houden
        mock.Intercept("IngelogdeGebruiker");

        // Controleer het aantal keren dat de methode is aangeroepen
        Assert.Equal(1, mock.CallCount("IngelogdeGebruiker"));
    }
}