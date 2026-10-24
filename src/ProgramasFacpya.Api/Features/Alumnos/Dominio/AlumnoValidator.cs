using FluentValidation;
using ProgramasFacpya.Api.Features.Alumnos.Dominio.Dto;
namespace ProgramasFacpya.Api.Features.Alumnos;
public class AlumnoValidator : AbstractValidator<AlumnoDTO>
{
    public AlumnoValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es requerido")
            .Must(TenerMinimoTresLetras).WithMessage("El nombre debe tener al menos 3 caracteres")
            .Must(SoloLetras).WithMessage("El nombre solo puede contener letras")
            .MaximumLength(50).WithMessage("El nombre no puede exceder los 50 caracteres.");

        RuleFor(x => x.APaterno)
            .NotEmpty().WithMessage("El apellido paterno es requerido")
            .Must(TenerMinimoTresLetras).WithMessage("El apellido paterno debe tener al menos 3 caracteres")
            .Must(SoloLetras).WithMessage("El apellido paterno solo puede contener letras")
            .MaximumLength(50).WithMessage("El apellido paterno no puede exceder los 50 caracteres.");

        RuleFor(x => x.AMaterno)
            .NotEmpty().WithMessage("El apellido materno es requerido")
            .Must(TenerMinimoTresLetras).WithMessage("El apellido materno debe tener al menos 3 caracteres")
            .Must(SoloLetras).WithMessage("El apellido materno solo puede contener letras")
            .MaximumLength(50).WithMessage("El apellido materno no puede exceder los 50 caracteres.");

        RuleFor(x => x.IdCarrera)
            .GreaterThan((byte)0).WithMessage("Debe seleccionar una carrera válida.");
    }

    private static bool TenerMinimoTresLetras(string texto) => 
        texto.Replace(" ", "").Length >= 3;
    private static bool SoloLetras(string texto) => 
        texto.Replace(" ", "").All(char.IsLetter);
    
}
