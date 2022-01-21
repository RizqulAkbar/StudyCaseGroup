namespace Admin.Dtos
{
    public record LoginPenggunaDto
    (
        string Username,
        string Password,
        bool? isLocked
    );
}
