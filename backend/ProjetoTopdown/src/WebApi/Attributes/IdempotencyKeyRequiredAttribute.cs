namespace ProjetoTopdown.WebApi.Attributes;

/// <summary>
/// Atributo para marcar endpoints que requerem o header de idempotência.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public sealed class IdempotencyKeyRequiredAttribute : Attribute
{
}