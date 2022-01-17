namespace DriverService.Data
{
    public record UserToken
        (
            string? Token,
            string? Expired,
            string? Message
        );
}
