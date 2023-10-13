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
public class ServicioController : BaseControllerApi
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ServicioController (IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<ServicioDto>>> Get()
    {
        var services = await _unitOfWork.Servicios.GetAllAsync();
        return _mapper.Map<List<ServicioDto>>(services);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ServicioDto>> Get(int id)
    {
        var service = await _unitOfWork.Servicios.GetByIdAsync(id);

        if (service == null)
        {
            return NotFound();
        }

        return _mapper.Map<ServicioDto>(service);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Servicio>> Post(ServicioDto serviceDto)
    {
        var service = _mapper.Map<Servicio>(serviceDto);
        this._unitOfWork.Servicios.Add(service);
        await _unitOfWork.SaveAsync();

        if (service == null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Post), new { id = serviceDto.Id }, serviceDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ServicioDto>> Put(int id, [FromBody] ServicioDto serviceDto)
    {
        if (serviceDto.Id == 0)
        {
            serviceDto.Id = id;
        }

        if (serviceDto.Id != id)
        {
            return BadRequest();
        }

        if (serviceDto == null)
        {
            return NotFound();
        }

        var service = _mapper.Map<Servicio>(serviceDto);
        _unitOfWork.Servicios.Update(service);
        await _unitOfWork.SaveAsync();
        return serviceDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ServicioDto>> Delete(int id)
    {
        var service = await _unitOfWork.Servicios.GetByIdAsync(id);

        if (service == null)
        {
            return NotFound();
        }

        _unitOfWork.Servicios.Remove(service);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}