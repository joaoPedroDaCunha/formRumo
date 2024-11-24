using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Rumo.Validation;

namespace Rumo.Models;

public class Vehicle
{
    [Key]
    [Uppercase]
    public required String Plate {get;set;}
    [Uppercase]
    public required String Renavam {get;set;}
    [Uppercase]
    public required String Chassis {get;set;} 
    public int Exercice {get;set;}
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