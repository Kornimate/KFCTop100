using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFCSharedData
{
    public class RecordDTO
    {
        [Required]
        public string? Name { get; set; }
        
        [Required]
        public string? Date { get; set; }

        [Required]
        public string? Picture { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        public int Population { get; set; }
        
        public static explicit operator Record(RecordDTO dto)
        {
            return new Record
            {
                Name = dto.Name,
                Date = dto.Date,
                Picture = Convert.FromBase64String(dto.Picture ?? ""),
                Address = dto.Address!.Split('.')[0],
                Population = dto.Population
            };
        }
    }
}
