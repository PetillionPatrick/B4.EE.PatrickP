using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kwaliteit.Domain.Models
{
    public class Machine
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        public string Naam { get; set; }

        [Ignore]
        public bool Edit { get; set; }

        [ForeignKey(typeof(Beuk))]
        public Guid BeukId { get; set; }

        [ManyToOne(nameof(BeukId))]
        public Beuk Beuk { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Status> Statussen { get; set; }
    }
}
