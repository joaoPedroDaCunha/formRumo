using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Rumo.Validation;

namespace Rumo.Models;

public class Aet
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid id{get;set;}
    public DateOnly Date {get;set;}
    [Uppercase]
    public String? Sigla {get;set;} = null;
    public required Vehicle Vehicle {get;set;}
    [Uppercase]
    public required String VehicleId {get;set;}
    public ICollection<Verser>? Versers {get;set;}
}