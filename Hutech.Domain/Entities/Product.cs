﻿using Hutech.Domain.Enums;

namespace Hutech.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public required string? Name { get; set; }
    public double Price { get; set; }
    public Status Status { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}