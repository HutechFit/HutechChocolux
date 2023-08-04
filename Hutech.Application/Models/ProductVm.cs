namespace Hutech.Application.Models;

public record ProductVm(
    int Id,
    string Name,
    double Price, 
    int Status, 
    string Description, 
    string Image,
    int CategoryId);