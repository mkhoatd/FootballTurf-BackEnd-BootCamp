using System.Collections.Immutable; 
using System.ComponentModel.DataAnnotations;

namespace WebApi.BusinessLogic.Interfaces;

public abstract class BusinessActionErrors
{
    private readonly List<ValidationResult> _errors = new List<ValidationResult>();
    public IImmutableList<ValidationResult> Errors => _errors.ToImmutableList();
    public bool HasErrors => _errors.Any();

    public void AddError(string errorMessage, params string[] propertyNames)
    {
        _errors.Add(new ValidationResult(errorMessage, propertyNames));
    }
}