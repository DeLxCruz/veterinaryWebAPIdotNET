using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiAnimals.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ApiAnimals.Controllers;
public class CitaController : BaseControllerApi
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CitaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<CitaDto>>> Get()
    {
        var appointments = await _unitOfWork.Citas.GetAllAsync();
        return _mapper.Map<List<CitaDto>>(appointments);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<CitaDto>> Get(int id)
    {
        var appointment = await _unitOfWork.Citas.GetByIdAsync(id);

        if (appointment == null)
        {
            return NotFound();
        }

        return _mapper.Map<CitaDto>(appointment);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Cita>> Post(CitaDto citaDto)
    {
        var appointment = _mapper.Map<Cita>(citaDto);
        this._unitOfWork.Citas.Add(appointment);
        await _unitOfWork.SaveAsync();

        if (appointment == null)
        {
            return BadRequest();
        }
        citaDto.Id = appointment.Id;
        return CreatedAtAction(nameof(Post), new { id = citaDto.Id }, citaDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<CitaDto>> Put(int id, [FromBody] CitaDto citaDto)
    {
        if (citaDto.Id == 0)
        {
            citaDto.Id = id;
        }

        if(citaDto.Id != id)
        {
            return BadRequest();
        }

        if(citaDto == null)
        {
            return NotFound();
        }

        var appointment = _mapper.Map<Cita>(citaDto);
        _unitOfWork.Citas.Update(appointment);
        await _unitOfWork.SaveAsync();
        return citaDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id)
    {
        var appointment = await _unitOfWork.Citas.GetByIdAsync(id);

        if (appointment == null)
        {
            return NotFound();
        }

        _unitOfWork.Citas.Remove(appointment);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}