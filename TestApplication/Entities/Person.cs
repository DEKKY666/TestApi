namespace TestApplication.Entities
{
    public class Person
    {
        public Guid Guid { get; set; }
        public DateTime CreateDate { get; set; }
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }   

        public string City { get; set; }
    }

}