using System.ComponentModel.DataAnnotations;

namespace Rumo.Models;

public class Vehicle
{
    [Key]
    [Length(7,7)]
    public required String Plate {get;set;}
    public required String Renavam {get;set;}
    public required String Chassis {get;set;} 
    [Length(4,4)]
    public int Exercice {get;set;}
    public required String Mark {get;set;}
    public required String Version {get;set;}
    public required String Type{get;set;}
    public DateOnly DuoDate {get;set;}
    public required String Situation{get;set;}
    public ICollection<Aet> Aets {get;set;}
    public ICollection<Verser> Versers {get;set;}
}