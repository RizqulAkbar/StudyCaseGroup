using Admin.Models;

namespace Admin.Dtos
{
    public class LockUserPayload
    {
        public LockUserPayload(Pengguna pengguna)
        {
            Pengguna = pengguna;
        }

        public Pengguna Pengguna { get; }
    }
}
