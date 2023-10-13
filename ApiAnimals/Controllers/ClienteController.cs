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
public class ClienteController : BaseControllerApi
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ClienteController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<ClienteDto>>> Get()
    {
        var clients = await _unitOfWork.Clientes.GetAllAsync();
        return _mapper.Map<List<ClienteDto>>(clients);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ClienteDto>> Get(int id)
    {
        var client = await _unitOfWork.Clientes.GetByIdAsync(id);

        if (client == null)
        {
            return NotFound();
        }

        return _mapper.Map<ClienteDto>(client);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Cliente>> Post(ClienteDto clientDto)
    {
        var client = _mapper.Map<Cliente>(clientDto);
        this._unitOfWork.Clientes.Add(client);
        await _unitOfWork.SaveAsync();

        if (client == null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Post), new { id = clientDto.Id }, clientDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<ClienteDto>> Put(int id, [FromBody] ClienteDto clientDto)
    {
        if (clientDto.Id == 0)
        {
            clientDto.Id = id;
        }
        
        if (clientDto.Id != id)
        {
            return BadRequest();
        }

        if (clientDto == null)
        {
            return BadRequest();
        }

        var client = _mapper.Map<Cliente>(clientDto);
        this._unitOfWork.Clientes.Update(client);
        await _unitOfWork.SaveAsync();
        return clientDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ClienteDto>> Delete(int id)
    {
        var client = await _unitOfWork.Clientes.GetByIdAsync(id);

        if (client == null)
        {
            return BadRequest();
        }

        this._unitOfWork.Clientes.Remove(client);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}