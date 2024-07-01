using System.ComponentModel.DataAnnotations;

namespace ContainersPortal.Models;

public class Student
{
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set; }    

    [Required]
    public DateTime DateOfBirth { get; set; }
}