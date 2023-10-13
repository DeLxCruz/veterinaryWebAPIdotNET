using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAnimals.Dtos;
public class ClienteTelefonoDto
{
    public int Id { get; set; }
    public ClienteDto Cliente { get; set; }
    public string Numero { get; set; }
}