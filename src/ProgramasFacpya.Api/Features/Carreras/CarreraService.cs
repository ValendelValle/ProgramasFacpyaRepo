using Dapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProgramasFacpya.Api.Common;
using ProgramasFacpya.Api.Data;
using ProgramasFacpya.Api.Features.Carreras.Dominio.Dto;
using ProgramasFacpya.Api.Features.Carreras.Dominio.Entidad;
namespace ProgramasFacpya.Api.Features.Carreras;

public class CarreraService (DbContext db, IValidator<CarreraDTO>validator)
{
    private readonly DbContext _db = db;
    private readonly IValidator<CarreraDTO> _validator = validator;
    public async Task<ApiResponse<Carrera>> ObtenerPorId(int idCarrera)
    {
        using var connection = _db.CreateConnection();
        var sql = @"SELECT c.IdCarrera,
                        c.Descrip
                    FROM CARRERA c
                    WHERE c.IdCarrera = @IdCarrera";

        var carrera = await connection.QueryFirstOrDefaultAsync<Carrera>(sql, new { IdCarrera = idCarrera });
        if (carrera == null)
            return ApiResponse<Carrera>.Error($"No se encontró una carrera con Id {idCarrera}");

        return ApiResponse<Carrera>.Ok(carrera);
    }
    public async Task<ApiResponse<IEnumerable<Carrera>>> ObtenerTodos()
    {
        using var connection = _db.CreateConnection();
        var sql = @"SELECT c.IdCarrera,
                        c.Descrip
                    FROM CARRERA c";

        var carreras = await connection.QueryAsync<Carrera>(sql);
        return ApiResponse<IEnumerable<Carrera>>.Ok(carreras);
    }
    public async Task<ApiResponse<int>> Crear(CarreraDTO dto)
    {
        using var connection = _db.CreateConnection();
        
        var validacion = await _validator.ValidateAsync(dto);
        if (!validacion.IsValid)
            return ApiResponse<int>.Error(validacion.Errors.First().ErrorMessage);

        //FechaRegistro se llena con GETDATE() en la consulta SQL, por lo que no es necesario asignarla aquí.
        var sql = @"INSERT INTO CARRERA (Descrip)
                    VALUES (@Descrip);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

        var idCarrera = await connection.ExecuteScalarAsync<int>(sql, new { dto.Descrip });
        return ApiResponse<int>.Ok(idCarrera, "Carrera creada exitosamente");
    }

    public async Task<ApiResponse<string>> Actualizar(int idCarrera, CarreraDTO dto)
    {
        using var connection = _db.CreateConnection();
        var validacion = await _validator.ValidateAsync(dto);
        if (!validacion.IsValid)
            return ApiResponse<string>.Error(validacion.Errors.First().ErrorMessage);

        var sql = @"UPDATE CARRERA
                    SET Descrip = @Descrip
                    WHERE IdCarrera = @IdCarrera";
        var rows = await connection.ExecuteAsync(sql, new { IdCarrera = idCarrera, dto.Descrip });

        if (rows == 0)
            return ApiResponse<string>.Error($"No se encontró una carrera con Id {idCarrera}");
        
        return ApiResponse<string>.Ok("Carrera actualizada correctamente");
    }

    public async Task<ApiResponse<string>> Eliminar(int idCarrera)
    {
        using var connection = _db.CreateConnection();
        string SqlScript = @"DELETE FROM CARRERA WHERE IdCarrera = @IdCarrera";
        var rows = await connection.ExecuteAsync(SqlScript, new { IdCarrera = idCarrera });

        if (rows == 0)
            return ApiResponse<string>.Error($"No se encontró una carrera con Id {idCarrera}");
        
        return ApiResponse<string>.Ok("Carrera eliminada correctamente");
    }
/*
    private static async Task<bool> ExisteCarrera(System.Data.IDbConnection connection, byte idCarrera)
    {
        var existe = await connection.QueryFirstOrDefaultAsync<byte?>(
            "SELECT IdCarrera FROM CARRERA WHERE IdCarrera = @IdCarrera", 
            new { IdCarrera = idCarrera });
        return existe != null;
    }*/
}
