using System.ComponentModel.DataAnnotations;

namespace KFCSharedData
{
    public class Record
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Date { get; set; }

        public byte[]? Picture { get; set; }

        public string? Address { get; set; }

        public int Population { get; set; }
    }
}
