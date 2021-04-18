using System;

namespace DenRozeCS.Entities
{   
    public class BillItemEntity
    {
        public int BIID { get; set;}

        public int Count { get; set;}
        public int IID { get; set;}
        public int BID { get; set; }
        public int OID { get; set; }

        public override string ToString()
        {
            return $"{BIID} {Count} {BID} {OID}";
        }
    }
}
