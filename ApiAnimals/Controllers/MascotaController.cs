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
public class MascotaController : BaseControllerApi
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MascotaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<MascotaDto>>> Get()
    {
        var pets = await _unitOfWork.Mascotas.GetAllAsync();
        return _mapper.Map<List<MascotaDto>>(pets);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<MascotaDto>> Get(int id)
    {
        var pet = await _unitOfWork.Mascotas.GetByIdAsync(id);

        if (pet == null)
        {
            return NotFound();
        }

        return _mapper.Map<MascotaDto>(pet);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<MascotaDto>> Post(MascotaDto petDto)
    {
        var pet = _mapper.Map<Mascota>(petDto);
        this._unitOfWork.Mascotas.Add(pet);
        await _unitOfWork.SaveAsync();

        if (pet == null)
        {
            return BadRequest();
        }

        return _mapper.Map<MascotaDto>(pet);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<MascotaDto>> Put(int id, [FromBody] MascotaDto petDto)
    {
        if (petDto.Id == 0)
        {
            petDto.Id = id;
        }

        if (petDto.Id != id)
        {
            return BadRequest();
        }

        if (petDto == null)
        {
            return NotFound();
        }

        var pet = _mapper.Map<Mascota>(petDto);
        _unitOfWork.Mascotas.Update(pet);
        await _unitOfWork.SaveAsync();
        return petDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<MascotaDto>> Delete(int id)
    {
        var pet = await _unitOfWork.Mascotas.GetByIdAsync(id);

        if (pet == null)
        {
            return NotFound();
        }

        _unitOfWork.Mascotas.Remove(pet);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }


    
}