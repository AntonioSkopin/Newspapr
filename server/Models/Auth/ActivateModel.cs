using System.ComponentModel.DataAnnotations;

namespace server.Models.Auth
{
    public class ActivateModel
    {
        [Required]
        public string Pincode { get; set; }
    }
}