namespace FuerzaG.Domain.Services.Validations;

public interface IValidator<T>
{
    Result Validate(T entity);
}