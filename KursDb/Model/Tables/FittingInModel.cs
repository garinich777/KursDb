using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KursDb.Model.Tables
{
    public class FittingInModel
    {
        [Key]
        [Column(Order = 1)]
        public int FittingId { get; set; }
        public virtual Fitting Fitting { get; set; }

        [Key]
        [Column(Order = 2)]
        public int ModelId { get; set; }
        public virtual ShoeModel ShoeModel { get; set; }

        public int Count { get; set; }
    }
}
