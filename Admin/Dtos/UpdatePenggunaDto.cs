using System;

namespace Admin.Dtos
{
    public record UpdatePenggunaDto
    (
        string? Email,
        string? Firstname,
        string? Lastname,
        string? Username,
        string? Password,
        DateTime? Updated
    );
}
