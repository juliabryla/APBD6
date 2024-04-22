﻿namespace zadanie6.Model;
using System.ComponentModel.DataAnnotations;

public class Animal
{
    public int IdAnimal{ get; set; }
    [Required]
    [MaxLength(200)]
    public string Name{ get; set; }
    [MaxLength(200)]
    public string Description{ get; set; }
    [Required]
    [MaxLength(200)]
    public string Category{ get; set; }
    [Required]
    [MaxLength(200)]
    public string Area{ get; set; }
   
    
    
}