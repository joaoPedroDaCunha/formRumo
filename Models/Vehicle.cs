using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Rumo.Validation;

namespace Rumo.Models;

public class Vehicle
{
    [Key]
    [Uppercase]
    public required String Plate {get;set;}
    [Uppercase]
    [Required]
    [MaxLength(50)]
    public required String Renavam {get;set;}
    [Uppercase]
    [MaxLength(50)]
    public required String Chassis {get;set;} 
    [Uppercase]
    public required String Mark {get;set;}
    [Uppercase]
    public required String Version {get;set;}
    public required String Type{get;set;}
    public DateOnly DuoDate {get;set;}
    public required String Situation{get;set;}
    public ICollection<Aet> Aets {get;set;}
    public ICollection<Verser> Versers {get;set;}
}