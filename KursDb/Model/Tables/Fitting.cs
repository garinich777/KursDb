using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KursDb.Model.Tables
{
    public class Fitting
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(1)]
        public string Type { get; set; }
        public int Price { get; set; }

        public virtual ICollection<FittingInModel> FittingInModels { get; set; }
    }
}
