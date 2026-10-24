namespace ProgramasFacpya.Api.Features.Alumnos.Dominio.Dto;
//se usa para crear como para actualizar (IdAlumno es Id y va en la ruta)
public class AlumnoDTO
{
    public string Nombre { get; set; } = string.Empty;
    public string APaterno{ get; set; } = string.Empty;
    public string AMaterno{ get; set; } = string.Empty;
    public byte IdCarrera {get; set; }
}
