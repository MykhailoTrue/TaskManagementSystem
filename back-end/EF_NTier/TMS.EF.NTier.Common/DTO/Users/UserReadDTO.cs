namespace TMS.EF.NTier.Common.DTO.Users
{
    public class UserReadDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
