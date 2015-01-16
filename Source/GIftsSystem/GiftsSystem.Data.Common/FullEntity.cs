namespace GiftsSystem.Data.Common
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using GiftsSystem.Data.Contracts;

    public abstract class FullEntity : IAuditInfo, IDeletableEntity
    {
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Specifies whether or not the CreatedOn property should be automatically set.
        /// </summary>
        public bool PreserveCreatedOn { get; set; }

        [NotMapped]
        public DateTime? ModifiedOn { get; set; }

        [Editable(false)]
        public bool IsDeleted { get; set; }

        [Editable(false)]
        public DateTime? DeletedOn { get; set; }
    }
}
