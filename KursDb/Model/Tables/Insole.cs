using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KursDb.Model.Tables
{
    public class Insole
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(1)]
        public string Sex { get; set; }
        public int Size { get; set; }
        public int Price { get; set; }

        public virtual ICollection<ShoeModel> ShoeModels { get; set; }
    }
}
