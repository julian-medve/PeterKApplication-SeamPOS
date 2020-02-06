using System.ComponentModel.DataAnnotations;

namespace PeterKApplication.Shared.Dtos
{
    public class NewAgentDto
    {
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string AgentCode { get; set; }
    }
}