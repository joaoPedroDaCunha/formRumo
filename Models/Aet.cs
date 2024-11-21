using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rumo.Models;

public class Aet
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid id;
    public DateOnly Date {get;set;}
    public required Vehicle Vehicle {get;set;}
    public required String VehicleId {get;set;}
    public ICollection<Verser> Versers {get;set;}
}