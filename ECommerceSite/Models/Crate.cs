using System.ComponentModel.DataAnnotations;

namespace ECommerceSite.Models
{
    /// <summary>
    /// Represents a single crate available purchase
    /// </summary>
    public class Crate
    {

        //primary key
        /// <summary>
        /// The unique identifier for each crate
        /// </summary>
        [Key]
        public int CrateId { get; set; }
        /// <summary>
        /// Name of crate
        /// </summary>
        [Required] //not really needed today, nullable is easily spotted in code with squiggles
        public string Title { get; set; }   
        /// <summary>
        /// sale price of crate online
        /// </summary>
        [Range(5, 500)]
        public double Price { get; set; }
    }
}
