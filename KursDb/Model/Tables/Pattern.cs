using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KursDb.Model.Tables
{
    public class Pattern
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(1)]
        public string Sex { get; set; }
        public int Size { get; set; }
        public int Square { get; set; }
        public int Сomplexity { get; set; }


        public virtual ICollection<ShoeModel> ShoeModels { get; set; }
    }
}
