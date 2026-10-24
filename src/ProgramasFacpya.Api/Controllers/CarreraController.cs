using Microsoft.AspNetCore.Mvc;
using ProgramasFacpya.Api.Servicios; //integra el controlador al namespace de la clase
using ProgramasFacpya.Api.Models; //conecta los modelos que vamos a usar
using ProgramasFacpya.Api.Features.Carreras;
using ProgramasFacpya.Api.Features.Carreras.Dominio.Dto;
using System.Reflection.Metadata.Ecma335;
namespace ProgramasFacpya.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CarreraController (CarreraService service): ControllerBase
{
    private readonly CarreraService _service = service;
    [HttpGet("ObtenerTodos")]
    public async Task<IActionResult> ObtenerTodos()
    {
        return Ok(await _service.ObtenerTodos());
    }
    [HttpGet("ObtenerPorId/{idCarrera}")]
    public async Task<IActionResult> ObtenerPorId(int idCarrera)
    {
        return Ok(await _service.ObtenerPorId(idCarrera));
    }
    [HttpPost("Crear")]
    public async Task<IActionResult> Crear([FromBody] CarreraDTO dto)
    {
        return Ok(await _service.Crear(dto));
    }
    
    [HttpPut("Actualizar/{idCarrera}")]
    public async Task<IActionResult> Actualizar(int idCarrera, [FromBody] CarreraDTO dto)
    {
        return Ok(await _service.Actualizar(idCarrera, dto));
    }

    [HttpDelete("Eliminar/{idCarrera}")]
    public async Task<IActionResult> Eliminar(int idCarrera)
    {
        return Ok(await _service.Eliminar(idCarrera));
    }  
}
