namespace ProgramasFacpya.Api.Servicios;
//ENCAPSULAMOS LOS POSIBLES ERRORES QUE SE PUEDEN PRESENTAR
using ProgramasFacpya.Api.Models;
public class BL_ALUMNO
{
    public static List<string> Validacion(string P_Nombre, string P_APaterno, string P_AMaterno)
    {
        List<string> lstErrores = [];
        if(P_Nombre.Replace(" ", "").Length<3)
            lstErrores.Add("El nombre no puede estar vacío");
        if (ValidaTexto(P_Nombre))
            lstErrores.Add("El nombre solo debe contener letras");
        
        if(P_APaterno.Replace(" ", "").Length<3)
            lstErrores.Add("El apellido paterno no puede estar vacío");
        if (ValidaTexto(P_APaterno))
            lstErrores.Add("El apellido paterno solo debe contener letras");

        if(P_AMaterno.Replace(" ", "").Length<3)
            lstErrores.Add("El apellido materno no puede estar vacío"); 
        if(ValidaTexto(P_AMaterno))
            lstErrores.Add("El apellido materno solo debe contener letras");
        return lstErrores;
    }
                                             // estructura record | variable, instancia del record
        public static List<string> Validaciones2(ALUMNO2 Alumno) //usamos el record
    {
        List<string> lstErrores = [];
        if(Alumno.Nombre.Replace(" ", "").Length<3)
            lstErrores.Add("El nombre no puede estar vacío");
        if (ValidaTexto(Alumno.Nombre))
            lstErrores.Add("El nombre solo debe contener letras");
        
        if(Alumno.APaterno.Replace(" ", "").Length<3)
            lstErrores.Add("El apellido paterno no puede estar vacío");
        if (ValidaTexto(Alumno.APaterno))
            lstErrores.Add("El apellido paterno solo debe contener letras");

        if(Alumno.AMaterno.Replace(" ", "").Length<3)
            lstErrores.Add("El apellido materno no puede estar vacío"); 
        if(ValidaTexto(Alumno.AMaterno))
            lstErrores.Add("El apellido materno solo debe contener letras");

        if(Alumno.Edad.Replace(" ","").Length<0)
            lstErrores.Add("La edad no puede estar vacía");
        if(!int.TryParse(Alumno.Edad, out int edad) || edad < 6 || edad > 80)
            lstErrores.Add("Ingrese una edad real");
        if(ValidaNumero(Alumno.Edad))
            lstErrores.Add("La edad solo debe contener números");
        return lstErrores;
    }
//validaciones de contenido de texto y números
    private static bool ValidaTexto(string P_Texto) //el alcance de las clases es interno
    {
        bool IsResultado = false;

        foreach(char Letra in P_Texto.Replace(" ", ""))
        {
            if(!char.IsLetter(Letra))
            {
                IsResultado = true;
                break;
            }
        }
        return IsResultado;
    }
//agregar validacion de contenido, de rango 
    private static bool ValidaNumero(string P_Numero)
    {
        bool IsResultado = false;
        foreach(char Num in P_Numero.Replace(" ",""))
        {
            if(!char.IsDigit(Num))
            {
                IsResultado = true;
                break;
            }
        }
        return IsResultado;
    }
}
