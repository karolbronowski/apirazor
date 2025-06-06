# Project Plan

## Foundations
- Clean architecture template based on [Jason Taylor’s CleanArchitecture](https://github.com/jasontaylordev/CleanArchitecture)
- Learning resources:
  - [Onion Architecture](https://code-maze.com/onion-architecture-in-aspnetcore/)
  - [Minimal APIs](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-9.0)
  - [REST API Lab](https://koglaza.gitbook.io/backend/lab-02/rest-api)
  - [JWT Authentication](https://koglaza.gitbook.io/backend/lab-04/jwt)
  - [Razor Pages Example](https://github.com/koglaza/pab-lab/tree/main/LAB05)

---

## Main Entities
- **Song**: id, title, artistId, listenedTimes, createdAt, updatedAt
- **Artist**: id, name, centsPerThousandListeners, createdAt, updatedAt
- **Listener**: id, name, favouriteSongsIds, createdAt, updatedAt

---

## Layers: 
- [x] Domain (Done – entities, logic)
- [ ] Application (Almost done – use cases, interfaces, DTOs, handlers)
- [ ] Infrastructure (Implement interfaces, DB, JWT, etc.)
- [ ] WebAPI (Controllers, endpoints, middleware)
- [ ] WebApplicationAdmin (UI)

---

## REST API (Spotify Domain)
- [x] Song CRUD endpoints
- [x] Artist CRUD endpoints
- [x] Listener CRUD endpoints
- [ ] Middleware for HTTP status codes and error handling
- [ ] JWT authentication and role-based authorization
- [ ] EF InMemory database setup
- [ ] Swagger/OpenAPI docs
- [ ] Configuration via appsettings.json/environment variables (JWT secrets, etc.)
- [ ] Unit tests of core logic
- [ ] cURL script to test API endpoints

---

## Razor Pages Admin Panel
- [ ] Song list (with filtering by artist/user)
- [ ] Song detail/edit/delete
- [ ] Artist list/add/edit/delete (only matching ID or admin)
- [ ] User list/add/edit/delete (admin only)
- [ ] Role-based access (admin, listener, artist)
- [ ] Login page (JWT-based)
- [ ] Integrate with REST API
- [ ] UI polish (optional)

---
