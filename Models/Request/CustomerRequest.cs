namespace AuthenDemo.Models.Request
{
    public class CustomerRequest
    {
        public string Username { get; set; } = null!;

        public string? Password { get; set; }

        public string? rePassword { get; set; }

        public string Name { get; set; } = null!;

        public int? Age { get; set; }

        public string? Gender { get; set; }
    }
}
