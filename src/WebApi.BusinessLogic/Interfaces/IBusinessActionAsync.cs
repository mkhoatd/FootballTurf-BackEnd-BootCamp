using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;

namespace WebApi.BusinessLogic.Interfaces;

public interface IBusinessActionAsync<in TIn, TOut>
{
    IImmutableList<ValidationResult> Errors { get; }
    bool HasErrors { get; }
    Task<TOut> ActionAsync(TIn dto);
}