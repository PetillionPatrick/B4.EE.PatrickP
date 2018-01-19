using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kwaliteit.Domain.Models
{
    public class Order
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        public string Naam { get; set; }

        [Ignore]
        public bool Edit { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Unit> Units { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Status> Statussen { get; set; }



    }
}
