using Microsoft.AspNetCore.Mvc;
using ProgramasFacpya.Api.Servicios; //integra el controlador al namespace de la clase
using ProgramasFacpya.Api.Models; //conecta los modelos que vamos a usar
using ProgramasFacpya.Api.Features.Maestros;
using ProgramasFacpya.Api.Features.Maestros.Dominio.Dto;
using System.Reflection.Metadata.Ecma335;

namespace ProgramasFacpya.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class MaestroController (MaestroService service): ControllerBase
{
     private readonly MaestroService _service = service;
    [HttpGet("ObtenerTodos")]
    public async Task<IActionResult> ObtenerTodos()
    {
        return Ok(await _service.ObtenerTodos());
    }
    [HttpGet("ObtenerPorId/{idMaestro}")]
    public async Task<IActionResult> ObtenerPorId(int idMaestro)
    {
        return Ok(await _service.ObtenerPorId(idMaestro));
    }
    [HttpPost("Crear")]
    public async Task<IActionResult> Crear([FromBody] MaestroDTO dto)
    {
        return Ok(await _service.Crear(dto));
    }
    
    [HttpPut("Actualizar/{idMaestro}")]
    public async Task<IActionResult> Actualizar(int idMaestro, [FromBody] MaestroDTO dto)
    {
        return Ok(await _service.Actualizar(idMaestro, dto));
    }

    [HttpDelete("Eliminar/{idMaestro}")]
    public async Task<IActionResult> Eliminar(int idMaestro)
    {
        return Ok(await _service.Eliminar(idMaestro));
    }  
}
