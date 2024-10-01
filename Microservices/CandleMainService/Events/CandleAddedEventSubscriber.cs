using CandleMainService.Models;
using System.Globalization;
using System.Text.Json;

namespace CandleMainService.Events
{
    public class CandleAddedEventSubscriber : DotNetCore.CAP.ICapSubscribe
    {
    //[DotNetCore.CAP.CapSubscribe("CandleAdded")]
    public void Consumer(System.Text.Json.JsonElement candleData)
    {
        var aaaa = GetObjectValue(candleData).ToString();

        var candle = System.Text.Json.JsonSerializer.Deserialize<Candle>(aaaa);
   
        Console.WriteLine(candleData);

    }

    public static object? GetObjectValue(object? obj)
    {
        switch (obj)
        {
            case null:
                return "NULL";
            case JsonElement jsonElement:
                {
                    var typeOfObject = jsonElement.ValueKind;
                    var rawText = jsonElement.GetRawText(); // Retrieves the raw JSON text for the element.

                    return typeOfObject switch
                    {
                        JsonValueKind.Number => float.Parse(rawText, CultureInfo.InvariantCulture),
                        JsonValueKind.String => obj.ToString(), // Directly gets the string.
                        JsonValueKind.True => true,
                        JsonValueKind.False => false,
                        JsonValueKind.Null => null,
                        JsonValueKind.Undefined => null, // Undefined treated as null.
                        JsonValueKind.Object => rawText, // Returns raw JSON for objects.
                        JsonValueKind.Array => rawText, // Returns raw JSON for arrays.
                        _ => rawText // Fallback to raw text for any other kind.
                    };
                }
            default:
                throw new ArgumentException("Expected a JsonElement object", nameof(obj));
        }

    }


    }
}
