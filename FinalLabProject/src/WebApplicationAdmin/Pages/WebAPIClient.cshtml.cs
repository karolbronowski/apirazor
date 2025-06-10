using FinalLabProject.Application.TodoLists.Queries.GetTodos;
using FinalLabProject.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;
using System.Text.Json.Serialization;
using System.Text.Json;
using WebApplicationAdmin.Config;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplicationAdmin.Pages
{
    public class WebAPIClientModel : PageModel
    {
    public class PaginatedList<T>
    {
        [JsonPropertyName("items")]
        public List<T> Items { get; set; } = new();

        [JsonPropertyName("pageNumber")]
        public int PageNumber { get; set; }

        [JsonPropertyName("totalPages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("totalCount")]
        public int TotalCount { get; set; }

        [JsonPropertyName("hasPreviousPage")]
        public bool HasPreviousPage { get; set; }

        [JsonPropertyName("hasNextPage")]
        public bool HasNextPage { get; set; }
    }
    public class SongVm
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; } = "";
        [JsonPropertyName("artistId")]
        public int ArtistId { get; set; }
        [JsonPropertyName("listenedTimes")]
        public int ListenedTimes { get; set; }
    }

        public List<SongVm> Songs { get; set; } = new();
        public bool IsArtist { get; set; }
        public int? UserArtistId { get; set; }
        public int? UserListenerId { get; set; }
        public string? DebugSongsJson { get; set; }
        private readonly ILogger<GrpcClientModel> _logger;
        private readonly IOptions<WebAPIConfig> _webAPIconfig;
        private readonly string TodoListUrl = "TodoLists";
        private readonly string UsersUrl = "Users";
        private string? _token { get; set; }
        public string? ResultMessage = string.Empty;

        public WebAPIClientModel(ILogger<GrpcClientModel> logger, IOptions<WebAPIConfig> webAPIconfig)
        {
            _logger = logger;
            _webAPIconfig = webAPIconfig;
            _token = string.Empty;
        }
        public IActionResult OnGet()
        {
            // Sprawdź czy użytkownik jest zalogowany
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AccessToken")))
                return RedirectToPage("/Index");

            var email = HttpContext.Session.GetString("UserEmail");
            var token = HttpContext.Session.GetString("AccessToken");

            // Sprawdź czy użytkownik to artysta
            var client = new RestClient($"http://{_webAPIconfig.Value.Host}:{_webAPIconfig.Value.Port}");
            var request = new RestRequest($"/api/Artists/by-email?Email={email}", Method.Get);
            request.AddHeader("Authorization", $"Bearer {token}");
            var response = client.Execute<List<dynamic>>(request);
            DebugSongsJson = $"Status: {response.StatusCode}\nError: {response.ErrorMessage}\nContent: {response.Content}\nData: {JsonSerializer.Serialize(response.Data)}";
            if (response.IsSuccessful && response.Data != null && response.Data.Count > 0)
            {
                IsArtist = true;
                UserArtistId = (int)response.Data[0].id;
                // Pobierz piosenki artysty
                var songsReq = new RestRequest($"/api/Songs/by-artist?artistId={UserArtistId}&pageNumber=1&pageSize=100", Method.Get);
                songsReq.AddHeader("Authorization", $"Bearer {token}");
                var songsResp = client.Execute<PaginatedList<SongVm>>(songsReq);
                DebugSongsJson = $"Status: {songsResp.StatusCode}\nError: {songsResp.ErrorMessage}\nContent: {songsResp.Content}\nData: {JsonSerializer.Serialize(songsResp.Data?.Items)}";
                Songs = songsResp.Data?.Items ?? new List<SongVm>();

            }
            else
            {
                // Sprawdź czy użytkownik to słuchacz
                var reqListener = new RestRequest($"/api/Listeners/by-username?Username={email}", Method.Get);
                reqListener.AddHeader("Authorization", $"Bearer {token}");
                var respListener = client.Execute<dynamic>(reqListener);
                if (respListener.IsSuccessful && respListener.Data != null)
                {
                    IsArtist = false;
                    UserListenerId = (int)respListener.Data.id;
                    // Pobierz wszystkie piosenkp
                    var songsReq = new RestRequest($"/api/Songs?pageNumber=1&pageSize=10", Method.Get);
                    songsReq.AddHeader("Authorization", $"Bearer {token}");
                    var songsResp = client.Execute<PaginatedList<SongVm>>(songsReq);
                    DebugSongsJson = $@"Status: {songsResp.StatusCode}
Error: {songsResp.ErrorMessage}
Content: {songsResp.Content}
Data: {JsonSerializer.Serialize(songsResp.Data?.Items)}";
                    Songs = songsResp.Data?.Items ?? new List<SongVm>();
                }
            }

            return Page();
        }

        public IActionResult OnPostFavorite(int songId)
        {
            var token = HttpContext.Session.GetString("AccessToken");
            var listenerId = UserListenerId;
            if (token == null || listenerId == null)
                return RedirectToPage("/Index");

            var client = new RestClient($"http://{_webAPIconfig.Value.Host}:{_webAPIconfig.Value.Port}");
            var req = new RestRequest($"/api/Listeners/favorite-song", Method.Put);
            req.AddHeader("Authorization", $"Bearer {token}");
            req.AddHeader("content-type", "application/json");
            req.AddJsonBody(new { listenerId = listenerId, songId = songId, isFavorited = true });
            var resp = client.Execute<RestResponse>(req);

            ResultMessage = resp.IsSuccessful ? "Dodano do ulubionych!" : "Błąd dodawania do ulubionych";
            return RedirectToPage();
        }
    }
}
