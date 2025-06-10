using FinalLabProject.Application.TodoLists.Queries.GetTodos;
using FinalLabProject.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;
using WebApplicationAdmin.Config;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace WebApplicationAdmin.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<GrpcClientModel> _logger;
        private readonly IOptions<WebAPIConfig> _webAPIconfig;
        private readonly string UsersUrl = "Users";
        private string? _token { get; set; }
        public string? ResultMessage = string.Empty;
        public bool IsLoggedIn => !string.IsNullOrEmpty(HttpContext.Session.GetString("AccessToken"));
        public string? LoggedUserEmail => HttpContext.Session.GetString("UserEmail");

        public IndexModel(ILogger<GrpcClientModel> logger, IOptions<WebAPIConfig> webAPIconfig)
        {
            _logger = logger;
            _webAPIconfig = webAPIconfig;
            _token = string.Empty;
        }
        public IActionResult OnPostLogin()
        {
            var email = Request.Form["LoginEmail"].ToString().Trim();
            var password = Request.Form["LoginPassword"].ToString().Trim();

            var client = new RestClient($"http://{this._webAPIconfig.Value.Host}:{this._webAPIconfig.Value.Port}");
            var request = new RestRequest("api/Users/login", Method.Post);
            request.AddHeader("content-type", "application/json");
            request.AddJsonBody(new { email = email, password = password });
            var response = client.Execute<AccessTokenResponse>(request);

            if (response.Data?.AccessToken != null)
            {
                this.ResultMessage = $"Zalogowano użytkownika: {email}";
                HttpContext.Session.SetString("AccessToken", response.Data.AccessToken);
                HttpContext.Session.SetString("UserEmail", email);
            }
            else
            {
                this.ResultMessage = $"Błąd logowania:<br/>Status: {response.StatusCode}<br/>Treść odpowiedzi: {response.Content}";
            }
            return Page();
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage();
        }

        public IActionResult OnPost()
        {
            var registerType = Request.Form["RegisterType"].ToString();
            var name = Request.Form["Name"].ToString().Trim();
            var username = Request.Form["Username"].ToString().Trim();
            var email = Request.Form["Email"].ToString().Trim();
            var password = Request.Form["Password"].ToString().Trim();

            RestClient client;
            RestRequest request;

            if (registerType == "artist")
            {
                var bio = Request.Form["Bio"].ToString().Trim();
                var payoutTier = Request.Form["PayoutTier"].ToString().Trim();

                client = new RestClient($"http://{this._webAPIconfig.Value.Host}:{this._webAPIconfig.Value.Port}");
                request = new RestRequest("/api/Artists", Method.Post);
                request.AddHeader("content-type", "application/json");
                request.AddJsonBody(new
                {
                    userId = (string?)null,
                    name = name,
                    username = username,
                    email = email,
                    bio = bio,
                    payoutTier = payoutTier,
                    password = password
                });
            }
            else // listener
            {
                client = new RestClient($"http://{this._webAPIconfig.Value.Host}:{this._webAPIconfig.Value.Port}");
                request = new RestRequest("/api/Listeners", Method.Post);
                request.AddHeader("content-type", "application/json");
                request.AddJsonBody(new
                {
                    userId = (string?)null,
                    name = name,
                    username = username,
                    email = email,
                    password = password
                });
            }

            var response1 = client.Execute<int>(request);

            if (!response1.IsSuccessful)
            {
                this.ResultMessage =
                    $"Błąd podczas tworzenia użytkownika: {response1.ErrorMessage ?? ""}<br/>" +
                    $"Status: {response1.StatusCode}<br/>" +
                    $"Treść odpowiedzi: {response1.Content}" +
                    $"{response1.ToString()}";
                return Page();
            }

            string? newId = response1.Data.ToString();

            var client2 = new RestClient($"http://{this._webAPIconfig.Value.Host}:{this._webAPIconfig.Value.Port}");
            var request2 = new RestRequest("api/Users/login", Method.Post);
            request2.AddHeader("content-type", "application/json");
            request2.AddJsonBody(new { email = email, password = password });
            var response2 = client2.Execute<AccessTokenResponse>(request2);
            _token = response2.Data?.AccessToken;

            if (!string.IsNullOrEmpty(_token))
            {
                HttpContext.Session.SetString("AccessToken", _token); // ZAPISZ TOKEN
                HttpContext.Session.SetString("UserEmail", email);
                if (registerType == "artist")
                    this.ResultMessage = $"Pomyślnie utworzono artystę (ID: {newId}) i zalogowano użytkownika.";
                else
                    this.ResultMessage = $"Pomyślnie utworzono słuchacza (ID: {newId}) i zalogowano użytkownika.";
            }
            else
            {
                var client3 = new RestClient($"http://{this._webAPIconfig.Value.Host}:{this._webAPIconfig.Value.Port}");
                var request3 = new RestRequest("/api/ListUsers/all", Method.Get);
                var response3 = client3.Execute<List<object>>(request3);

                this.ResultMessage =
                    $"Błąd autoryzacji:<br/>Status: {response2.StatusCode}<br/>" +
                    $"Treść odpowiedzi: {response2.Content}<br/>" +
                    $"Lista użytkowników do debugowania:<br/>{response3.Content}";
                return Page();

            }
            return Page();
        }
    }
};

