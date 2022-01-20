namespace Admin.Dtos
{
    public record LoginAdminDto
    (
        string Username,
        string Password,
        bool? isLocked
    );
}
