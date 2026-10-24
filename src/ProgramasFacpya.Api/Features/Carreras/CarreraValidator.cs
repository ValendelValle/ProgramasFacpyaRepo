using FluentValidation;
using ProgramasFacpya.Api.Features.Carreras.Dominio.Dto;
namespace ProgramasFacpya.Api.Features.Carreras;

public class CarreraValidator : AbstractValidator<CarreraDTO>
{
    public CarreraValidator()
    {
        RuleFor(x => x.Descrip)
                .NotEmpty().WithMessage("La descripción es requerida")
                .Must(TenerMinimoTresLetras).WithMessage("La descripción debe ser de 2 a 3 caracteres")
                .Must(SoloLetras).WithMessage("La descripción solo puede contener letras");
    }
    private static bool TenerMinimoTresLetras(string texto) => 
        texto.Replace(" ", "").Length >= 2 && texto.Replace(" ", "").Length <= 3;
    private static bool SoloLetras(string texto) => 
        texto.Replace(" ", "").All(char.IsLetter);
}
