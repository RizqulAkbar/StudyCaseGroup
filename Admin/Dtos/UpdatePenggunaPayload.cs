using Admin.Models;

namespace Admin.Dtos
{
    public class UpdatePenggunaPayload
    {
        public UpdatePenggunaPayload(Pengguna pengguna) 
        {
            Pengguna = pengguna;
        }

        public Pengguna Pengguna { get; }
    }
}
