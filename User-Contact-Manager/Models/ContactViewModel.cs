namespace User_Contact_Manager.Models
{
    public class ContactViewModel
    {
        public string? Id { get; set; } 

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public string? Company { get; set; }

        public string? Notes { get; set; }
    }
}
