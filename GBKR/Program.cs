using Newtonsoft.Json;


string itserviceJson = File.ReadAllText("itservice.json");
string itemJson = File.ReadAllText("item.json");

var itservices = JsonConvert.DeserializeObject<List<ITService>>(itserviceJson);
var items = JsonConvert.DeserializeObject<List<Items>>(itemJson);


var output = from service in itservices join item in items on service.Item equals item.Key
select new {item.Name, service.Approval, service.Approve_by_manager, service.Priority_key};

foreach (var element in output)
{
    Console.WriteLine("Ime          |Approval   |   Approve_By_Manager  |  PriorityKey");
    Console.WriteLine($"{element.Name} | {element.Approval} | {element.Approve_by_manager} | {element.Priority_key}");
    Console.WriteLine("");
    Console.WriteLine("");
}




public class ITService
{
    [JsonProperty("item")]
    public string Item {get; set;}


    [JsonProperty("approval")]
    public string Approval {get; set;}

    [JsonProperty("approve_by_manager")]
    public string Approve_by_manager {get; set;}

    [JsonProperty("priority_key")]
    public string Priority_key {get; set;}
}


public class Items
{
    [JsonProperty("key")]
    public string Key {get; set;}

    [JsonProperty("name")]
    public string Name {get; set;}
}


