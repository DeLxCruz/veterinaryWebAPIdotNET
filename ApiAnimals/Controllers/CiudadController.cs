using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiAnimals.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiAnimals.Controllers;
public class CiudadController : BaseControllerApi
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CiudadController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<CiudadDto>>> Get()
    {
        var cities = await _unitOfWork.Ciudades.GetAllAsync();
        return _mapper.Map<List<CiudadDto>>(cities);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<CiudadDto>> Get(int id)
    {
        var city = await _unitOfWork.Ciudades.GetByIdAsync(id);

        if (city == null)
        {
            return NotFound();
        }

        return _mapper.Map<CiudadDto>(city);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Ciudad>> Post(CiudadDto ciudadDto)
    {
        var city = _mapper.Map<Ciudad>(ciudadDto);
        this._unitOfWork.Ciudades.Add(city);
        await _unitOfWork.SaveAsync();

        if (city == null)
        {
            return BadRequest();
        }

        return CreatedAtAction("Get", new { id = city.Id }, city);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<CiudadDto>> Put(int id, [FromBody] CiudadDto ciudadDto)
    {
        if (ciudadDto.Id == 0)
        {
            ciudadDto.Id = id;
        }

        if (ciudadDto.Id != id)
        {
            return BadRequest();
        }

        if (ciudadDto == null)
        {
            return NotFound();
        }

        var city = _mapper.Map<Ciudad>(ciudadDto);
        _unitOfWork.Ciudades.Update(city);
        await _unitOfWork.SaveAsync();
        return ciudadDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Ciudad>> Delete(int id)
    {
        var city = await _unitOfWork.Ciudades.GetByIdAsync(id);

        if (city == null)
        {
            return BadRequest();
        }

        _unitOfWork.Ciudades.Remove(city);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}