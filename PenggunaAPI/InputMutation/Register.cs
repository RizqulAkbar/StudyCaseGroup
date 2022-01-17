namespace PenggunaAPI.InputMutation
{
    public record Register
    (
        string Firstname,
        string Lastname,
        string Email,
        string Username,
        string Password,
        double Latitude,
        double Longitude
    );
}
