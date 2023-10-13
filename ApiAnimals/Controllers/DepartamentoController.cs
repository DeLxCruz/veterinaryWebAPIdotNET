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
public class DepartamentoController : BaseControllerApi
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DepartamentoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<DepartamentoDto>>> Get()
    {
        var departments = await _unitOfWork.Departamentos.GetAllAsync();
        return _mapper.Map<List<DepartamentoDto>>(departments);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<DepartamentoDto>> Get(int id)
    {
        var department = await _unitOfWork.Departamentos.GetByIdAsync(id);

        if (department == null)
        {
            return NotFound();
        }

        return _mapper.Map<DepartamentoDto>(department);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Departamento>> Post(DepartamentoDto departmentDto)
    {
        var department = _mapper.Map<Departamento>(departmentDto);
        this._unitOfWork.Departamentos.Add(department);
        await _unitOfWork.SaveAsync();

        if (department == null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Get), new { id = department.Id }, department);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<DepartamentoDto>> Put(int id, DepartamentoDto departmentDto)
    {
        if (departmentDto.Id == 0)
        {
            departmentDto.Id = id;
        }

        if (departmentDto.Id != id)
        {
            return NotFound();
        }

        if (departmentDto == null)
        {
            return BadRequest();
        }

        var department = _mapper.Map<Departamento>(departmentDto);
        this._unitOfWork.Departamentos.Update(department);
        await _unitOfWork.SaveAsync();
        return departmentDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<DepartamentoDto>> Delete(int id)
    {
        var department = await _unitOfWork.Departamentos.GetByIdAsync(id);

        if (department == null)
        {
            return BadRequest();
        }

        _unitOfWork.Departamentos.Remove(department);
        await _unitOfWork.SaveAsync();
        return _mapper.Map<DepartamentoDto>(department);
    }

}