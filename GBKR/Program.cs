using System.IO;
using Newtonsoft.Json;
using System.Text;


string itserviceJson = File.ReadAllText("itservice.json");
string itemJson = File.ReadAllText("item.json");

var itservices = JsonConvert.DeserializeObject<List<ITService>>(itserviceJson);
var items = JsonConvert.DeserializeObject<List<Items>>(itemJson);


var output = from service in itservices join item in items on service.Item equals item.Key
select new {item.Name, service.Approval, service.Approve_by_manager, service.Priority_key};



var sb = new StringBuilder();
sb.AppendLine("<!DOCTYPE html>");
sb.AppendLine("<html><head><style>");
sb.AppendLine("table { border-collapse: collapse; width: 100%; }");
sb.AppendLine("th, td { border: 1px solid black; padding: 8px; text-align: left; }");
sb.AppendLine("th { background-color: #f2f2f2; }");
sb.AppendLine("</style></head><body>");
sb.AppendLine("<table>");
sb.AppendLine("<tr><th>Name</th><th>Approval</th><th>Approve By Manager</th><th>Priority Key</th></tr>");

foreach (var element in output)
{
    sb.AppendLine($"<tr><td>{element.Name}</td><td>{element.Approval}</td><td>{element.Approve_by_manager}</td><td>{element.Priority_key}</td></tr>");
}

sb.AppendLine("</table>");
sb.AppendLine("</body></html>");

// Save HTML to file
File.WriteAllText("output.html", sb.ToString());




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


