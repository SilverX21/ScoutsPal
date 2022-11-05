//using Newtonsoft.Json;
using RestSharp;
using System.Text.Json;
using testeConsola;

Console.WriteLine("Hello, World!");

var url = "https://reqres.in/api/users?page=2";
var client = new RestClient(url);
var request = new RestRequest(url, Method.Get);
RestResponse response = await client.ExecuteAsync<Test>(request);
var restResponse = JsonSerializer.Deserialize<Test>(response?.Content);

Console.WriteLine(response);
Console.ReadLine();