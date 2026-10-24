namespace ProgramasFacpya.Api.Features.Alumnos.Dominio.Entidad;
public class Alumno
{
    public int IdAlumno { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string APaterno{ get; set; } = string.Empty;
    public string AMaterno{ get; set; } = string.Empty;
    public byte IdCarrera {get; set; }
    public DateTime FecRegistro { get; set; }

    //Descripcion de la carpeta (viene desde el JSON)
    public string Carrera { get; set; } = string.Empty;
}
