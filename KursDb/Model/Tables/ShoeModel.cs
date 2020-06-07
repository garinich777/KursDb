using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KursDb.Model.Tables
{
    public class ShoeModel
    {
        public int Id { get; set; }
        public int DownBilletId { get; set; }
        public int InsoleId { get; set; }
        public int PatternId { get; set; }
        public int ShoeTreeId { get; set; }
        public int SoleId { get; set; }
        public int UpBilletId { get; set; }
        public int Size { get; set; }
        [Required]
        [MaxLength(1)]
        public int Sex { get; set; }

        public virtual DownBillet DownBillet { get; set; }
        public virtual Insole Insole { get; set; }
        public virtual Pattern Pattern { get; set; }
        public virtual ShoeTree ShoeTree { get; set; }
        public virtual Sole Sole { get; set; }
        public virtual UpBillet UpBillet { get; set; }

        public virtual ICollection<ModelInOrder> ModelInOrders { get; set; }
        public virtual ICollection<FittingInModel> FittingsInModel { get; set; }
    }
}
