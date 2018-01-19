using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kwaliteit.Domain.Models
{
    public class Beuk
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        public string Naam { get; set; }

        [Ignore]
        public bool Edit { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Machine> Machines { get; set; }

        [ForeignKey(typeof(User))]
        public Guid OwnerId { get; set; }

        [ManyToOne(nameof(OwnerId), CascadeOperations = CascadeOperation.CascadeRead)]
        public User Owner { get; set; }
    }
}
