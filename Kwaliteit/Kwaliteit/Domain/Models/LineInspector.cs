using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kwaliteit.Domain.Models
{
    public class LineInspector
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        public string Nummer { get; set; }

        public string Naam { get; set; }

        [Ignore]
        public bool Edit { get; set; }

        [ForeignKey(typeof(Status))]
        public Guid StatusId { get; set; }

        [ManyToOne(nameof(StatusId), CascadeOperations = CascadeOperation.CascadeRead)]
        public Status Status { get; set; }
    }
}
