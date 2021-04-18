using System;

namespace DenRozeCS.Entities
{   
    public class BillEntity
    {
        public int BID { get; set;}

        public decimal Total { get; set;}

        public string EETinfo { get; set; }

        public DateTime Created_at { get; set;}
        public DateTime Edited_at { get; set; }
        public string TransactionType { get; set; }

        public override string ToString()
        {
            return $"{BID} {EETinfo} {Created_at} {Total}";
        }
    }
}
