namespace ProgramasFacpya.Api.Models;
//RECORD/DTO porque la podemos mover
public sealed record ALUMNO2
(
    string Nombre,
    string APaterno,
    string AMaterno,
    string Edad
);