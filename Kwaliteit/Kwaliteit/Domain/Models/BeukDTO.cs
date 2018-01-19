using System;
using System.Collections.Generic;
using System.Text;

namespace Kwaliteit.Domain.Models
{
    public class BeukDTO
    {
        public Guid Id { get; set; }

        public string Naam { get; set; }

        public Guid OwnerId { get; set; }
    }
}
