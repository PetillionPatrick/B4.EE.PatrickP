using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kwaliteit.Domain.Models
{
    public class Status
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        [Ignore]
        public string[] StatusKeuze { get; set; }

        public string GekozenStatus { get; set; }

        public DateTime Datum { get; set; }

        public string VormNr { get; set; }

        public string PartNummer { get; set; }

        public string ArchiefNr { get; set; }

        public string ReparatieNummer { get; set; }

        public bool ProefSpuiting { get; set; }

        [Ignore]
        public bool Edit { get; set; }

        [ForeignKey(typeof(Operator))]
        public Guid OperatorId { get; set; }

        [ManyToOne(nameof(OperatorId), CascadeOperations = CascadeOperation.CascadeRead)]
        public Operator Operator { get; set; }

        [ForeignKey(typeof(Order))]
        public Guid OrderId { get; set; }

        [ManyToOne(nameof(OrderId), CascadeOperations = CascadeOperation.CascadeRead)]
        public Order Order { get; set; }

        [ForeignKey(typeof(LineInspector))]
        public Guid LiId { get; set; }

        [ManyToOne(nameof(LiId), CascadeOperations = CascadeOperation.CascadeRead)]
        public LineInspector Li { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Unit> Units { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Afkeuren> Afkeuren { get; set; }

        [ForeignKey(typeof(Machine))]
        public Guid MachineId { get; set; }

        [ManyToOne(nameof(MachineId), CascadeOperations = CascadeOperation.CascadeRead)]
        public Machine Machine { get; set; }

        public Status()
        {
            StatusKeuze = new string[]
            {
                "Geen produktie", "Start", "Reparatie", "Einde", "Afkeur"
            };
        }
    }
}
