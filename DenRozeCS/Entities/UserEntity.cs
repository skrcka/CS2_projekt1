namespace DenRozeCS.Entities
{   
    public class UserEntity
    {
        public int UID { get; set;}

        public string Login { get; set;}

        public string Full_name { get; set; }

        public string Password { get; set;}

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public override string ToString()
        {
            return $"{UID} {Login} {Full_name} {Phone} {Email} {Address}";
        }
    }
}
