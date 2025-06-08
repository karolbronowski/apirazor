using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;

namespace FinalLabProject.WebAPI.Middleware;

public class EndpointsHeadersMiddleware
{
    private readonly RequestDelegate _next;

    public EndpointsHeadersMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value?.ToLowerInvariant();
        var method = context.Request.Method;

        if (path != null)
        {
            // Songs endpoints
            if (path == "/api/songs" && method == "GET")
                AddHeaders(context, "Songs", "GetAll");
            else if (path == "/api/songs" && method == "POST")
                AddHeaders(context, "Songs", "Create");
            else if (path == "/api/songs/by-artist" && method == "GET")
                AddHeaders(context, "Songs", "GetByArtist");
            else if (path == "/api/songs/play" && method == "PUT")
                AddHeaders(context, "Songs", "Play");
            else if (path.StartsWith("/api/songs/") && method == "PUT")
                AddHeaders(context, "Songs", "Update");
            else if (path.StartsWith("/api/songs/") && method == "DELETE")
                AddHeaders(context, "Songs", "Delete");

            // Artists endpoints
            else if (path == "/api/artists" && method == "GET")
                AddHeaders(context, "Artists", "GetAll");
            else if (path == "/api/artists" && method == "POST")
                AddHeaders(context, "Artists", "Create");
            else if (path.StartsWith("/api/artists/") && method == "GET")
                AddHeaders(context, "Artists", "GetById");
            else if (path.StartsWith("/api/artists/") && method == "PUT")
                AddHeaders(context, "Artists", "Update");
            else if (path.StartsWith("/api/artists/") && method == "DELETE")
                AddHeaders(context, "Artists", "Delete");
            else if (path == "/api/artists/by-name" && method == "GET")
                AddHeaders(context, "Artists", "GetByName");
            else if (path == "/api/artists/payout-tier" && method == "PUT")
                AddHeaders(context, "Artists", "UpdatePayoutTier");

            // Listeners endpoints
            else if (path == "/api/listeners" && method == "GET")
                AddHeaders(context, "Listeners", "GetAll");
            else if (path == "/api/listeners" && method == "POST")
                AddHeaders(context, "Listeners", "Create");
            else if (path.StartsWith("/api/listeners/") && method == "GET")
                AddHeaders(context, "Listeners", "GetById");
            else if (path.StartsWith("/api/listeners/") && method == "PUT")
                AddHeaders(context, "Listeners", "Update");
            else if (path.StartsWith("/api/listeners/") && method == "DELETE")
                AddHeaders(context, "Listeners", "Delete");
            else if (path == "/api/listeners/by-username" && method == "GET")
                AddHeaders(context, "Listeners", "GetByUsername");
            else if (path == "/api/listeners/favorite-song" && method == "PUT")
                AddHeaders(context, "Listeners", "UpdateFavoriteSong");
        }

        await _next(context);
    }

    private void AddHeaders(HttpContext context, string resource, string operation)
    {
        context.Response.OnStarting(() =>
        {
            context.Response.Headers["X-Api-Resource"] = resource;
            context.Response.Headers["X-Api-Operation"] = operation;
            context.Response.Headers["X-Api-Method"] = context.Request.Method;

            // Add resource ID if present in the path (e.g., /api/songs/123)
            var segments = context.Request.Path.Value?.Split('/', StringSplitOptions.RemoveEmptyEntries);
            if (segments != null && segments.Length >= 3 && int.TryParse(segments[2], out var id))
            {
                context.Response.Headers["X-Api-Resource-Id"] = id.ToString();
            }

            // Add all query parameters as headers
            foreach (var (key, value) in context.Request.Query)
            {
                context.Response.Headers[$"X-Api-Query-{key}"] = value;
            }

            return Task.CompletedTask;
        });
    }
}