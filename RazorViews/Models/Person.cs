namespace RazorViews.Models
{
    public class Person
    {
        public string? Name { get; set; }
        public DateTime? Birthday { get; set; }
        public Gender Gender { get; set; }
    }

    public enum Gender
    {
        Male, Female
    }
}
