using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rumo.Models;

public class Verser
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid id{get;set;}
    public required Aet Aet{get;set;}
    public Guid AetId{get;set;}
    public required Vehicle Vehicle {get;set;}
    public required String VehicleId {get;set;}
}