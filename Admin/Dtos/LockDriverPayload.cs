using Admin.Models;

namespace Admin.Dtos
{
    public class LockDriverPayload
    {
        public LockDriverPayload(UserDriver userDriver) 
        { 
            UserDriver = userDriver;
        }

        public UserDriver UserDriver { get; }
    }
}
