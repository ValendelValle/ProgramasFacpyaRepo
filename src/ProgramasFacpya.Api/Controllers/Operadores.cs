using Microsoft.AspNetCore.Mvc;

namespace ProgramasFacpya.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Operadores : ControllerBase
{
    [HttpGet("Suma/{Numero1:int}/{Numero2:int}")] //Ejer3

    public IActionResult Suma(int Numero1, int Numero2)
    {
        return Ok($"La suma de {Numero1} y {Numero2} es {Numero1 + Numero2}");
    }

    [HttpGet("Resta/{Numero1:int}/{Numero2:int}")] //Ejer4

    public IActionResult Resta(int Numero1, int Numero2)
    {
        return Ok($"La resta de {Numero1} y {Numero2} es {Numero1 - Numero2}");
    }

    [HttpGet("Multiplicacion/{Numero1:int}/{Numero2:int}")] //Ejer5
    public IActionResult Multiplicacion(int Numero1, int Numero2)
    {
        return Ok($"La multiplicación de {Numero1} y {Numero2} es {Numero1 * Numero2}");
    }

    [HttpGet("Promedio/{Numero1:int}/{Numero2:int}/{Numero3:int}")] //Ejer6
    public IActionResult Promedio(int Numero1, int Numero2, int Numero3)
    {
        if (Numero1 == 0 || Numero2 == 0 || Numero3 == 0)
        {
            return BadRequest("Ningún número puede ser cero.");
        }
        return Ok($"El promedio de {Numero1}, {Numero2} y {Numero3} es {(Numero1 + Numero2 + Numero3) / 3}");
    }

    [HttpGet("Cuadrado/{Numero1:int}")] //Ejer7
    public IActionResult Cuadrado(int Numero1)
    {
        return Ok($"El cuadrado de {Numero1} es {Numero1 * Numero1}");
    }
    
    [HttpGet("Area Cuadrado/{Numero1:int}")] //Ejer8
    public IActionResult AreaCuadrado(int Numero1)
    {
        return Ok($"El área del cuadrado es {Numero1 * Numero1}");
    }
    
    [HttpGet("Perimetro Cuadrado/{Numero1:int}")] //Ejer9
    public IActionResult PerimetroCuadrado(int Numero1)
    {
        return Ok($"El perímetro del cuadrado con lado igual a {Numero1} es {4 * Numero1}");
    }
    
    [HttpGet("Area Circulo/{Numero1:int}")] //Ejer10
    public IActionResult AreaCirculo(int Numero1)
    {
        return Ok($"El área del círculo es {Math.PI * (Numero1 * Numero1)}");
    }

    [HttpGet("Perimetro Circulo/{Numero1:int}")] //Ejer11
    public IActionResult PerimetroCirculo(int Numero1)
    {
        return Ok($"El perímetro del círculo es {2 * Math.PI * Numero1}");
    }

    [HttpGet("Area Rectangulo/{Numero1:int}/{Numero2:int}")] //Ejer12
    public IActionResult AreaRectangulo(int Numero1, int Numero2)
    {
        return Ok($"El área del rectángulo es {Numero1 * Numero2}");
    }
   
   [HttpGet("Area Triangulo/{Numero1:int}/{Numero2:int}")]//Ejer13
    public IActionResult AreaTriangulo(int Numero1, int Numero2)
    {
        return Ok($"El área del triángulo es {0.5 * Numero1 * Numero2}");
    }
}
