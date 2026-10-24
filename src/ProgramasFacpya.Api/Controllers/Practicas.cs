using Microsoft.AspNetCore.Mvc;
namespace ProgramasFacpya.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Practicas : ControllerBase
{
    [HttpGet("ComparadorA0/{Num}")]//Ejer1------------------------------
    public IActionResult ComparadorA0(string Num)
    {
        if(ValidarNumero(Num))
            return BadRequest("Ingrese un número válido");
        int Numero = Convert.ToInt32(Num);
        if (Numero > 0)
            return Ok($"El número {Numero} es mayor a 0");
        if (Numero==0)
            return Ok($"El número ingresado es igual a 0");
        if (Numero < 0)
            return Ok($"El número {Numero} es menor a 0");
        else
            return BadRequest("Ingrese un número válido");
    }

    [HttpGet("Promedio/{Num1}/{Num2}/{Num3}/{Num4}/{Num5}")]//Ejer2------------------------
    public IActionResult Promedio(string Num1, string Num2, string Num3, string Num4, string Num5)
    {
        int[] numeros = ConvertirInt(Num1, Num2, Num3, Num4, Num5);
        if (NumerosPositivos(numeros))
            return BadRequest("Ningún número puede ser cero o menor que cero.");
        if (ValidarNumero(Num1) || ValidarNumero(Num2) || ValidarNumero(Num3) || ValidarNumero(Num4) || ValidarNumero(Num5))
            return BadRequest("Ningun numero puede ser letra");
        int Suma = numeros[0] + numeros[1] + numeros[2] + numeros[3] + numeros[4];
        double Promedio = (double)Suma / 5;
        string Mensaje;
        if (Promedio <= 100 && Promedio >= 60)
            Mensaje = "aprobado";
        else if (Promedio < 60 && Promedio >= 0)
            Mensaje = "reprobado";
        else
            Mensaje = "Ingrese calificaciones válidas, no puede ser negativo ni mayor a 100";
        return Ok($"El promedio de las calificaciones {Num1}, {Num2}, {Num3}, {Num4} y {Num5} es {Promedio}, el estudiante ha {Mensaje}");
    }

    [HttpGet("ComparadorMayor/{Num1}/{Num2}/{Num3}")] //Ejer3
    public IActionResult ComparadorMayor(string Num1, string Num2, string Num3)
    {
        int[] numeros = ConvertirInt(Num1, Num2, Num3);
        if (NumerosPositivos(numeros))
            return BadRequest("Ningún número puede ser cero o menor que cero.");
        if (ValidarNumero(Num1) || ValidarNumero(Num2) || ValidarNumero(Num3))
            return BadRequest("Ningun numero puede ser letra");
        if(numeros[0] == numeros[1] || numeros[1] == numeros[2] || numeros[0] == numeros[2])
            return BadRequest($"Al menos dos números son iguales, todos deben ser números diferentes");
        int Mayor = Math.Max(Math.Max(numeros[0], numeros[1]), numeros[2]);
        return Ok($"El número mayor de {Num1}, {Num2} y {Num3} es {Mayor}");
    }

    [HttpGet("ComparadorMenor/{Num1}/{Num2}/{Num3}")] //Ejer4
    public IActionResult ComparadorMenor(string Num1, string Num2, string Num3)
    {
        int[] numeros = ConvertirInt(Num1, Num2, Num3);
        if (ValidarNumero(Num1) || ValidarNumero(Num2) || ValidarNumero(Num3))
            return BadRequest("Ningun numero puede ser letra");
        if (NumerosPositivos(numeros))
            return BadRequest("Ningún número puede ser cero o menor que cero.");
        if(numeros[0] == numeros[1] || numeros[1] == numeros[2] || numeros[0] == numeros[2])
            return BadRequest($"Al menos dos números son iguales, todos deben ser números diferentes");
        int Menor = Math.Min(Math.Min(numeros[0], numeros[1]), numeros[2]);
        return Ok($"El número menor de {Num1}, {Num2} y {Num3} es {Menor}");
    }

    [HttpGet("ComparadorPar/{Num}")]//Ejer5
    public IActionResult ComparadorPar(string Num)
    {
        int[] numeros = ConvertirInt(Num);
        if (NumerosPositivos(numeros))
            return BadRequest("El numero no puede ser cero o menor que cero.");
        if(ValidarNumero(Num))
            return BadRequest("Ningun numero puede ser letra");
        if (numeros[0] % 2 == 0)
            return Ok($"El número {Num} es par");
        else
            return Ok($"El número {Num} es impar");
    }

      [HttpGet("ComparadorVocal/{Letra}")]//Ejer6
    public IActionResult ComparadorVocal(string Letra)
    {
        if (Letra.Length != 1)
            return BadRequest("Ingrese solo una letra");
        if (!char.IsLetter(Letra[0]))
            return BadRequest("Ingrese una letra válida");
        if ("aeiouAEIOU".Contains(Letra))
            return Ok($"La letra {Letra} es una vocal");
        else
            return Ok($"La letra {Letra} es una consonante");
    }
//VALIDACIONES-------------------------------------------------------------------
    private static bool ValidarNumero(string Numero)
    {
        bool IsResultado = false;
        foreach(char Num in Numero.Replace(" ","").Replace("-", ""))
        {
            if(!char.IsDigit(Num))
            {
                IsResultado = true;
                break;
            }
        }
        return IsResultado;
    }
    private static bool NumerosPositivos(params int[] Numero) //positivos
    {
        bool IsPositive =false;
        foreach (int Num in Numero)
        {
            if (Num <= 0)
                IsPositive = true;
                break;
        }
        return IsPositive;
    }
    private static int[] ConvertirInt(params string[] Numero)
    {
        int[] Numeros = new int[Numero.Length];
        for (int i = 0; i < Numero.Length; i++)
        {
            Numeros[i] = Convert.ToInt32(Numero[i]);
        }
        return Numeros;
    }
}
