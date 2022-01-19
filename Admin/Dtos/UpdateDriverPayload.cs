using Admin.Models;

namespace Admin.Dtos
{
    public class UpdateDriverPayload
    {
        public UpdateDriverPayload(UserDriver userDriver) 
        { 
            UserDriver = userDriver;
        }

        public UserDriver UserDriver { get; }
    }
}
