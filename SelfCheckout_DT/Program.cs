using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

Checkout ch = new Checkout();

//GET app/v1/Stock
app.MapGet("/app/v1/Stock", () =>
{
    if (!ch.inserted.Any())
    {
        string errormsg = "There are no coins in CheckoutMashine";
        Console.WriteLine(errormsg);
        return errormsg;
    }
    else
        return JsonConvert.SerializeObject(ch.inserted, Formatting.Indented);
});

//POST app/v1/Stock
app.MapPost("/app/v1/Stock", async delegate (HttpContext context)
{
    ch.inserted.Clear();
    using (StreamReader reader = new StreamReader(context.Request.Body, Encoding.UTF8))
    {
        string jsonstring = await reader.ReadToEndAsync();
        try
        {
            ch.inserted = JsonConvert.DeserializeObject<List<Denomination>>(jsonstring);
            return JsonConvert.SerializeObject(ch.inserted, Formatting.Indented);
        }
        catch(Exception exc)
        {
            string errormsg = "Error during processing the JSON array";
            Console.WriteLine(errormsg);
            return errormsg;
        }
    }
});

//POST app/v1/Checkout
app.MapPost("/app/v1/Checkout", async delegate (HttpContext context)
{
    using (StreamReader reader = new StreamReader(context.Request.Body, Encoding.UTF8))
    {
        string jsonstring = await reader.ReadToEndAsync();

        //@TODO add the coins, not change them
        ch = JsonConvert.DeserializeObject<Checkout>(jsonstring);

        return JsonConvert.SerializeObject(ch.checkoutCalculation(), Formatting.Indented);
    }
});

app.Run();

/// <summary>
/// Checkout class
/// Calculate the trip and contain Denomiations
/// </summary>
public class Checkout
{
    /// <summary>
    /// Coins inserted (all)
    /// </summary>
    public List<Denomination> inserted;

    /// <summary>
    /// The price
    /// </summary>
    public int price = 0;

    public Checkout()
    {
        inserted = new List<Denomination>();
    }

    /// <summary>
    /// Calculate the Trip
    /// </summary>
    /// <returns>List of the trip's denominations</returns>
    public List<Denomination> checkoutCalculation()
    {
        List<Denomination> returnDenominations = new List<Denomination>();

        inserted.Sort((p, q) => (-1 * p.Value.CompareTo(q.Value)));

        //@TODO calculation

        price = 0;
        return returnDenominations;
    }
}

/// <summary>
/// Denomination class
/// </summary>
public class Denomination
{
    /// <summary>
    /// Value of denomination
    /// </summary>
    [Key]
    public int Value { get; set; }

    /// <summary>
    /// Amount of denomination
    /// </summary>
    public int Amount { get; set; }
}
