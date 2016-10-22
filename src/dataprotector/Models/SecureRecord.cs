using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace dataprotector.Models
{
    public class SecureRecord
    {
        [Key]
        public int RecordId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CreditCardNo { get; set; }
    }
}