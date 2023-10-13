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
public class ClienteTelefonoController : BaseControllerApi
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ClienteTelefonoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<ClienteTelefonoDto>>> Get()
    {
        var clientPhone = await _unitOfWork.ClienteTelefonos.GetAllAsync();
        return _mapper.Map<List<ClienteTelefonoDto>>(clientPhone);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<ClienteTelefonoDto>> Get(int id)
    {
        var clientPhone = await _unitOfWork.ClienteTelefonos.GetByIdAsync(id);

        if (clientPhone == null)
        {
            return NotFound();
        }

        return _mapper.Map<ClienteTelefonoDto>(clientPhone);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<ClienteTelefonoDto>> Post(ClienteTelefonoDto clientPhoneDto)
    {
        var clientPhone = _mapper.Map<ClienteTelefono>(clientPhoneDto);
        this._unitOfWork.ClienteTelefonos.Add(clientPhone);
        await _unitOfWork.SaveAsync();

        if (clientPhone == null)
        {
            return BadRequest();
        }

        clientPhoneDto.Id = clientPhone.Id;
        return CreatedAtAction(nameof(Post), new { id = clientPhoneDto.Id }, clientPhoneDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ClienteTelefonoDto>> Put(int id, [FromBody] ClienteTelefonoDto clientPhoneDto)
    {
        if (clientPhoneDto.Id == 0)
        {
           clientPhoneDto.Id = id;
        }

        if (clientPhoneDto.Id != id)
        {
            return BadRequest();
        }

        if (clientPhoneDto == null)
        {
            return NotFound();
        }

        var clientPhone = _mapper.Map<ClienteTelefono>(clientPhoneDto);
        this._unitOfWork.ClienteTelefonos.Update(clientPhone);
        await _unitOfWork.SaveAsync();
        return clientPhoneDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<ActionResult<ClienteTelefonoDto>> Delete(int id)
    {
        var clientPhone = await _unitOfWork.ClienteTelefonos.GetByIdAsync(id);

        if (clientPhone == null)
        {
            return NotFound();
        }

        this._unitOfWork.ClienteTelefonos.Remove(clientPhone);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}