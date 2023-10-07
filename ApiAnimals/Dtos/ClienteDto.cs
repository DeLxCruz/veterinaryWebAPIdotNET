using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAnimals.Dtos;
public class ClienteDto
{
    public string NOmbre { get; set; }
    public string Apellidos { get; set; }
    public string Email { get; set; }
    public ClienteDto ClienteDirecion { get; set; }
    
}