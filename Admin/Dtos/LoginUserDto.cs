namespace Admin.Dtos
{
    public record LoginUserDto
    (
        string Username,
        string Password,
        bool? isLocked
    );
}
