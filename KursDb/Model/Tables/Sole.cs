using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KursDb.Model.Tables
{
    public class Sole
    {
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        [MaxLength(1)]
        public string Season { get; set; }
        [Required]
        [MaxLength(1)]
        public string Sex { get; set; }
        public int Size { get; set; }
        public int Price { get; set; }

        public virtual ICollection<ShoeModel> ShoeModels { get; set; }
    }
}
