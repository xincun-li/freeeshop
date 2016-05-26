namespace Final_eshop_xincunli.Models
{
    public class MemberDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }        
        public string ConfirmPassword { get; set; }        
        public string Name { get; set; }
        public int Role { get; set; }
    }
}