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
public class ClienteDireccionController : BaseControllerApi
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ClienteDireccionController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<ClienteDireccionDto>>> Get()
    {
        var clientAddress = await _unitOfWork.ClienteDirecciones.GetAllAsync();
        return _mapper.Map<List<ClienteDireccionDto>>(clientAddress);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<ClienteDireccionDto>> Get(int id)
    {
        var clientAddress = await _unitOfWork.ClienteDirecciones.GetByIdAsync(id);

        if (clientAddress == null)
        {
            return NotFound();
        }

        return _mapper.Map<ClienteDireccionDto>(clientAddress);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<ClienteDireccionDto>> Post(ClienteDireccionDto clientAddressDto)
    {
        var clientAddress = _mapper.Map<ClienteDireccion>(clientAddressDto);
        this._unitOfWork.ClienteDirecciones.Add(clientAddress);
        await _unitOfWork.SaveAsync();

        if (clientAddress == null)
        {
            return BadRequest();
        }

        clientAddressDto.Id = clientAddress.Id;
        return CreatedAtAction(nameof(Post), new { id = clientAddressDto.Id }, clientAddress);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ClienteDireccionDto>> Put(int id, [FromBody] ClienteDireccionDto clientAddressDto)
    {
        if (clientAddressDto.Id == 0)
        {
            clientAddressDto.Id = id;
        }

        if (clientAddressDto.Id != id)
        {
            return BadRequest();
        }

        if (clientAddressDto == null)
        {
            return NotFound();
        }

        var clientAddress = _mapper.Map<ClienteDireccion>(clientAddressDto);
        _unitOfWork.ClienteDirecciones.Update(clientAddress);
        await _unitOfWork.SaveAsync();
        return clientAddressDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<ClienteDireccionDto>> Delete(int id)
    {
        var clientAddress = await _unitOfWork.ClienteDirecciones.GetByIdAsync(id);

        if (clientAddress == null)
        {
            return NotFound();
        }

        _unitOfWork.ClienteDirecciones.Remove(clientAddress);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}