using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAnimals.Dtos;
public class ClienteDireccionDto
{
    public int Id { get; set; }
    public int IdCliente { get; set; }
    public ClienteDto Clientes { get; set; }
    public string TipoDeVia { get; set; }
    public int NumeroPri { get; set; }
    public string Letra { get; set; }
    public string Bis { get; set; }
    public string LetraSec { get; set; }
    public string Cardinal { get; set; }
    public string Complemento { get; set; }
    public string CodigoPostal { get; set; }
    public int IdCiudad { get; set; }
    public CiudadDto Ciudades { get; set; }
}