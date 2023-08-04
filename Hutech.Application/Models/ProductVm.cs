﻿namespace Hutech.Application.Models;

public record ProductResponse(
    int Id,
    string Name,
    double Price, 
    int Status, 
    string Description, 
    string Image,
    int CategoryId);

public record ProductRequest(
    string Name,
    double Price,
    int Status,
    string Description,
    string Image,
    int CategoryId);