namespace InlogMoq;
using System.Dynamic;
using ImpromptuInterface;
using Dynamitey;
using Xunit;

public interface IInlogSysteem
{
    public string IngelogdeGebruiker();
    public void Login(string gebruiker);
}

class InlogSysteem : IInlogSysteem
{
    public string IngelogdeGebruiker()
    {
        // Zogenaamd wordt hier de data opgehaald uit de database...
        return "Bob";
    }
    public void Login(string gebruiker)
    {
        // Zogenaamd wordt hier data in de database gezet...
        Console.WriteLine(gebruiker + " is ingelogd");
    }
}

class ProfielPagina
{
    private IInlogSysteem inlogSysteem;
    public ProfielPagina(IInlogSysteem inlogSysteem)
    {
        this.inlogSysteem = inlogSysteem;
    }
    public string Tekst()
    {
        return "Dit is de profielpagina van " + inlogSysteem.IngelogdeGebruiker() + ". Klik hier om verder te gaan. ";
    }
}

class MijnMoq<T> where T : class
{
    private IDictionary<string, Object> obj = new ExpandoObject();

    // Houdt het aantal oproepen voor elke methode bij
    private Dictionary<string, int> callCounts = new Dictionary<string, int>();

    // Methode om een mock te configureren. Het resultaat van de functie f wordt teruggegeven als de methode wordt aangeroepen.
    public void Setup<S>(string Name, Func<S> f)
    {
        obj.Add(Name, Return<S>.Arguments(f));
    }

    // Hier wordt de mock gemaakt en ActLike zorgt ervoor dat de mock zich gedraagt als het type T
    public T Object
    {
        get
        {
            return obj.ActLike<T>();
        }
    }

    // Methode om het aantal oproepen van een methode te krijgen
    public int CallCount(string methodName)
    {
        if (callCounts.ContainsKey(methodName))
        {
            return callCounts[methodName];
        }
        return 0;
    }

    // Intercepteer methodeaanroepen om het aantal keren bij te houden
    public void Intercept(string methodName)
    {
        if (callCounts.ContainsKey(methodName))
        {
            callCounts[methodName]++;
        }
        else
        {
            callCounts[methodName] = 1;
        }
    }
}
