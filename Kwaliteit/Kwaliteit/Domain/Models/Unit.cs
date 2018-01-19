using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kwaliteit.Domain.Models
{
    public class Unit
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        public string Naam { get; set; }

        [Ignore]
        public bool Edit { get; set; }

        [ForeignKey(typeof(Order))]
        public Guid OrderId { get; set; }

        [ManyToOne(nameof(OrderId), CascadeOperations = CascadeOperation.CascadeRead)]
        public Order Order { get; set; }

        [ForeignKey(typeof(Status))]
        public Guid StatusId { get; set; }

        [ManyToOne(nameof(StatusId), CascadeOperations = CascadeOperation.CascadeRead)]
        public Status Status { get; set; }
    }
}
