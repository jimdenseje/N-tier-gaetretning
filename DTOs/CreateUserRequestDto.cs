namespace DTOs
{
    public class CreateUserRequestDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public short Age { get; set; }
    }
}
