using System.IO;
using Newtonsoft.Json;
using System.Text;


string itserviceJson = File.ReadAllText("itservice.json");
string itemJson = File.ReadAllText("item.json");
string oblastJson = File.ReadAllText("oblast.json");

var itservices = JsonConvert.DeserializeObject<List<ITService>>(itserviceJson);
var items = JsonConvert.DeserializeObject<List<Items>>(itemJson);
var oblasti = JsonConvert.DeserializeObject<List<Oblast>>(oblastJson);



// var result = from service in itservices
//              join item in items on service.Item equals item.Key
//              join oblast in oblasti on item.Oblast equals oblast.Key
//              select new
//              {
//                  ServiceKey = service.Key,
//                  ItemName = item.Name,
//                  OblastName = oblast.Name,
//                  Approval = service.Approval,
//                  Priority = service.PriorityKey
//              };


var output = from service in itservices
             join item in items on service.Item equals item.Key
             join oblast in oblasti on service.Oblast equals oblast.Key
             select new
             {
                 OblastID = oblast.Key,
                 OblastName = oblast.Name,

                 ItemID = item.Key,
                 ItemName = item.Name,

                 ManagerApproval = service.Approve_by_manager,
                 NeedApproval = service.Approval,
                 PriorityKey = service.Priority_key
             };

// foreach (var element in output)
// {
//     Console.WriteLine($"{element.OblastName} | {element.ItemName} | {element.NeedApproval} | {element.PriorityKey}");
// }


var sb = new StringBuilder();
sb.AppendLine("<!DOCTYPE html>");
sb.AppendLine("<html><head><style>");
sb.AppendLine("table { border-collapse: collapse; width: 100%; }");
sb.AppendLine("th, td { border: 1px solid black; padding: 8px; text-align: left; }");
sb.AppendLine("th { background-color: #f2f2f2; }");
sb.AppendLine("</style></head><body>");
sb.AppendLine("<table>");
sb.AppendLine("<tr><th>OblastID</th><th>Oblast</th><th>ImeID</th><th>Ime</th><th>Approval</th><th>ApprovalByManager</th><th>PriorityKey</th></tr>");

foreach (var element in output)
{
    sb.AppendLine($"<tr><td>{element.OblastID}</td><td>{element.OblastName}</td><td>{element.ItemID}</td><td>{element.ItemName}</td><td>{element.NeedApproval}</td><td>{element.ManagerApproval}</td><td>{element.PriorityKey}</td></tr>");
}

sb.AppendLine("</table>");
sb.AppendLine("</body></html>");


File.WriteAllText("output.html", sb.ToString());




public class ITService
{
    [JsonProperty("item")]
    public string Item {get; set;}
    [JsonProperty("oblast")]
    public string Oblast {get; set;}


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

public class Oblast
{
    [JsonProperty("key")]
    public string Key {get; set;}

    [JsonProperty("name")]
    public string Name {get; set;}
}


