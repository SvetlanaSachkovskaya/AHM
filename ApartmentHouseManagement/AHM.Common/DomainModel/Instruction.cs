using System;

namespace AHM.Common.DomainModel
{
    public class Instruction : Entity
    {
        public Building Building { get; set; }

        public int BuildingId { get; set; }

        public string Content { get; set; }

        public int EmploeeId { get; set; }

        public virtual User Emploee { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsClosed { get; set; }

        public Priority Priority { get; set; }
    }
}