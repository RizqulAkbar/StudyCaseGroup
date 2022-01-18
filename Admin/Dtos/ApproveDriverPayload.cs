using Admin.Models;

namespace Admin.Dtos
{
    public class ApproveDriverPayload
    {
        public ApproveDriverPayload(UserDriver userDriver)
        {
            UserDriver = userDriver;
        }

        public UserDriver UserDriver { get; }
    }
}
