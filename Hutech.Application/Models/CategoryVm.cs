namespace Hutech.Application.Models;

public record CategoryResponse(int Id, string Name, string Description);

public record CategoryRequest(string Name, string Description);