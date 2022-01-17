namespace DriverService.Data
{
    public partial class UserData
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }    
        public float LatDriver { get; set; }
        public float LongDriver { get; set; }

        public string Email { get; set; }
        public string Username { get; set; }
    }
}
