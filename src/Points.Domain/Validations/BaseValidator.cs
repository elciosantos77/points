using FluentValidation;
using FluentValidation.Results;
using Points.Domain.Core.Models;

namespace Points.Domain.Validations
{
    public abstract class BaseValidator<TEntity> : AbstractValidator<TEntity> where TEntity : Entity<TEntity>
    {
        public BaseValidator()
        {
            ValidationResult =  new ValidationResult();
        }

        public ValidationResult ValidationResult { get; set; }

        public abstract bool Validar(TEntity entidade);
    }
}
