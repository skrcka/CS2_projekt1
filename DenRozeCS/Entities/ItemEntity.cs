using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DenRozeCS.Entities
{   
    public class ItemEntity
    {
        public int IID { get; set;}

        public string Name { get; set;}

        public string Code { get; set; }

        public decimal Price { get; set;}

        public decimal DPH { get; set; }

        public int Count { get; set; }

        public int Mincount { get; set; }

        public int Weight { get; set; }

        public override string ToString()
        {
            return $"{IID} {Name} {Code} {Price} {DPH} {Count} {Mincount} {Weight}";
        }
    }
}
