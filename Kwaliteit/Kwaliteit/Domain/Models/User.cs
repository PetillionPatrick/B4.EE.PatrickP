using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kwaliteit.Domain.Models
{
    public class User
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Beuk> Beuken { get; set; }
    }
}
