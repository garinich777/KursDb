using System.Collections.Generic;

namespace KursDb.Model.Tables
{
    public class UpBillet
    {
        public int Id { get; set; }
        public string Material { get; set; }
        public int Price { get; set; }
        public int Density { get; set; }

        public virtual ICollection<ShoeModel> ShoeModels { get; set; }
    }
}
