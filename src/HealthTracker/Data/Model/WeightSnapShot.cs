using HealthTracker.Data.Helpers;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTracker.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class WeightSnapShot: ILoggable
    {
        public int Id { get; set; }
        
		[ForeignKey("Tenant")]
        public int? TenantId { get; set; }

        [ForeignKey("Profile")]
        public int? ProfileId { get; set; }
     
		public float Pounds { get; set; }

        public DateTime WeighedOn { get; set; }
        
		public DateTime CreatedOn { get; set; }
        
		public DateTime LastModifiedOn { get; set; }
        
		public string CreatedBy { get; set; }
        
		public string LastModifiedBy { get; set; }
        
		public bool IsDeleted { get; set; }

        public virtual Tenant Tenant { get; set; }

        public virtual Profile Profile { get; set; }
    }
}
