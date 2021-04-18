using System;

namespace DenRozeCS.Entities
{   
    public class OrderEntity
    {
        public int OID { get; set;}
        public string Note { get; set; }
        public DateTime Created_at { get; set;}
        public DateTime Edited_at { get; set; }
        public int UID { get; set; }

        public override string ToString()
        {
            return $"{OID} {Note} {Created_at}";
        }
    }
}
