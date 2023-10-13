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
public class RazaController : BaseControllerApi
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RazaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<RazaDto>>> Get()
    {
        var breeds = await _unitOfWork.Razas.GetAllAsync();
        return _mapper.Map<List<RazaDto>>(breeds);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<RazaDto>> Get(int id)
    {
        var breed = await _unitOfWork.Razas.GetByIdAsync(id);

        if (breed == null)
        {
            return NotFound();
        }

        return _mapper.Map<RazaDto>(breed);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Raza>> Post(RazaDto breedDto)
    {
        var breed = _mapper.Map<Raza>(breedDto);
        this._unitOfWork.Razas.Add(breed);
        await _unitOfWork.SaveAsync();

        if (breed == null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Post), new { id = breedDto.Id }, breedDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<RazaDto>> Put(int id, RazaDto breedDto)
    {
        if (breedDto.Id == 0)
        {
            breedDto.Id = id;
        }

        if (breedDto.Id != id)
        {
            return BadRequest();
        }

        if (breedDto == null)
        {
            return NotFound();
        }

        var breed = _mapper.Map<Raza>(breedDto);
        _unitOfWork.Razas.Update(breed);
        await _unitOfWork.SaveAsync();
        return breedDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<RazaDto>> Delete(int id)
    {
        var breed = await _unitOfWork.Razas.GetByIdAsync(id);

        if (breed == null)
        {
            return NotFound();
        }

        _unitOfWork.Razas.Remove(breed);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}