using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;

namespace WebApi.BusinessLogic.Interfaces;

public interface IBusinessAction<in TIn, out TOut>
{
    IImmutableList<ValidationResult> Errors { get; }
    bool HasErrors { get; }
    TOut Action(TIn dto);
}