using System;

namespace Admin.Dtos
{
    public record RegisterDto
    (
        string FullName,
        string Email,
        string UserName,
        string Password,
        DateTime? Created,
        DateTime? Updated,
        bool? isLocked
    );
}
