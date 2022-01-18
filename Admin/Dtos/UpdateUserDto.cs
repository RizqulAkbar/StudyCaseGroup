using System;

namespace Admin.Dtos
{
    public record UpdateUserDto
    (
        string? FullName,
        string? Email,
        string? Username,
        string? Password,
        DateTime? Created,
        DateTime? Update,
        bool? IsLocked
    );
}
