using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Rumo.Validation;

namespace Rumo.Models;

public class Verser
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid id{get;set;} = Guid.NewGuid();
    public Aet? Aet { get; set; }
    public Guid AetId{get;set;}
    public  Vehicle? Vehicle {get;set;}
    [Uppercase]
    public String? VehicleId {get;set;}
}