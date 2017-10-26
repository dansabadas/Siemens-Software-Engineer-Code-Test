namespace App
{
    public class Company
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Classification Classification { get; set; }
    }

    public static class CompanyNames
    {
        public const string VeryImportantClient = "VeryImportantClient";
        public const string ImportantClient = "ImportantClient";
    }
}