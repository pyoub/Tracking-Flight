using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace vol.Models.Entity
{
    public class EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public long Id { get; set; }

        public DateTime CreatedDate { get; set; }   
        
        public DateTime? UpdatedDate { get; set; }   
    }
}