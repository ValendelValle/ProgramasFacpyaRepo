using Dapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProgramasFacpya.Api.Common;
using ProgramasFacpya.Api.Data;
using ProgramasFacpya.Api.Features.Alumnos.Dominio.Dto;
using ProgramasFacpya.Api.Features.Alumnos.Dominio.Entidad;
namespace ProgramasFacpya.Api.Features.Alumnos;

public class AlumnoService(DbContext db, IValidator<AlumnoDTO>validator)
{
    private readonly DbContext _db = db;
    private readonly IValidator<AlumnoDTO> _validator = validator;
    public async Task<ApiResponse<Alumno>> ObtenerPorId(int idAlumno)
    {
        using var connection = _db.CreateConnection();
        var sql = @"SELECT a.IdAlumno,
                        a.Nombre,
                        a.APaterno,
                        a.AMaterno,
                        a.IdCarrera,
                        a.FechaRegistro,
                        c.Descrip AS Carrera
                    FROM ALUMNO a
                    INNER JOIN CARRERA c ON a.IdCarrera = c.IdCarrera
                    WHERE a.IdAlumno = @IdAlumno";
        
        var alumno = await connection.QueryFirstOrDefaultAsync<Alumno>(sql, new { IdAlumno = idAlumno });
        if (alumno == null)
            return ApiResponse<Alumno>.Error($"No se encontró un alumno con Id {idAlumno}");
        
        return ApiResponse<Alumno>.Ok(alumno);
    }
    public async Task<ApiResponse<IEnumerable<Alumno>>> ObtenerTodos()
    {
        using var connection = _db.CreateConnection();
        var sql = @"SELECT a.IdAlumno,
                        a.Nombre,
                        a.APaterno,
                        a.AMaterno,
                        a.IdCarrera,
                        a.FechaRegistro,
                        c.Descrip AS Carrera
                    FROM ALUMNO a
                    INNER JOIN CARRERA c ON a.IdCarrera = c.IdCarrera
                    ORDER BY a.APaterno, a.AMaterno, a.Nombre";
        
        var alumnos = await connection.QueryAsync<Alumno>(sql);
        return ApiResponse<IEnumerable<Alumno>>.Ok(alumnos);
    }

    public async Task<ApiResponse<int>> Crear(AlumnoDTO dto)
    {
        var validacion = await _validator.ValidateAsync(dto);
        if (!validacion.IsValid)
            return ApiResponse<int>.Error(validacion.Errors.First().ErrorMessage);
        using var connection = _db.CreateConnection();
        if(!await ExisteCarrera(connection, dto.IdCarrera))
            return ApiResponse<int>.Error("La carrera seleccionada no es válida.");
        //FechaRegistro se llena con GETDATE() en la consulta SQL, por lo que no es necesario asignarla aquí.
        var sql = @"INSERT INTO ALUMNO (Nombre, APaterno, AMaterno, IdCarrera)
                    VALUES (@Nombre, @APaterno, @AMaterno, @IdCarrera);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

        var idAlumno = await connection.ExecuteScalarAsync<int>(sql, new { dto.Nombre, dto.APaterno, dto.AMaterno, dto.IdCarrera});
        return ApiResponse<int>.Ok(idAlumno, "Alumno creado exitosamente");
    }

    public async Task<ApiResponse<string>> Actualizar(int idAlumno, AlumnoDTO dto)
    {
        var validacion = await _validator.ValidateAsync(dto);
        if (!validacion.IsValid)
            return ApiResponse<string>.Error(validacion.Errors.First().ErrorMessage);
        using var connection = _db.CreateConnection();

        if(!await ExisteCarrera(connection, dto.IdCarrera))
            return ApiResponse<string>.Error("La carrera seleccionada no es válida.");
        
        var sql = @"UPDATE ALUMNO
                    SET Nombre = @Nombre,
                        APaterno = @APaterno,
                        AMaterno = @AMaterno,
                        IdCarrera = @IdCarrera
                    WHERE IdAlumno = @IdAlumno";

        var rows = await connection.ExecuteAsync(sql, new { IdAlumno = idAlumno, dto.Nombre, dto.APaterno, dto.AMaterno, dto.IdCarrera });

        if (rows == 0)
            return ApiResponse<string>.Error($"No se encontró un alumno con Id {idAlumno}");
        
        return ApiResponse<string>.Ok("Alumno actualizado correctamente");
    }

    public async Task<ApiResponse<string>> Eliminar(int idAlumno)
    {
        using var connection = _db.CreateConnection();
        string SqlScript = @"DELETE FROM ALUMNO WHERE IdAlumno = @IdAlumno";
        var rows = await connection.ExecuteAsync(SqlScript, new { IdAlumno = idAlumno });

        if (rows == 0)
            return ApiResponse<string>.Error($"No se encontró un alumno con Id {idAlumno}");
        
        return ApiResponse<string>.Ok("Alumno eliminado correctamente");
    }

    private static async Task<bool> ExisteCarrera(System.Data.IDbConnection connection, byte idCarrera)
    {
        var existe = await connection.QueryFirstOrDefaultAsync<byte?>(
            "SELECT IdCarrera FROM CARRERA WHERE IdCarrera = @IdCarrera", 
            new { IdCarrera = idCarrera });
        return existe != null;
    }
}
