using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rumo.Models;

public class Verser
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid id{get;set;} = Guid.NewGuid();
    public Aet? Aet { get; set; }
    public Guid AetId{get;set;}
    public  Vehicle? Vehicle {get;set;}
    public String? VehicleId {get;set;}
}