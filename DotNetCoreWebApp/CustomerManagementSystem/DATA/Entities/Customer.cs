using System.ComponentModel.DataAnnotations;

namespace CustomerManagementSystem.DATA.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [StringLength(256)]
        public string? FirstName { get; set; }

        [StringLength(256)]
        public string? LastName { get; set; }

        [StringLength(256)]
        public string? Email { get; set; }
    }
}
