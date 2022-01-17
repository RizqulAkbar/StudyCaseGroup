namespace DriverService.Data
{
    public record RegisterUser
     (
         string Firstname,
         string Lastname,
         string Email,
         string Username,
         string Password
     );
}
