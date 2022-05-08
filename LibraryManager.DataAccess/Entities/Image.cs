namespace LibraryManager.DataAccess.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }

        public virtual Book Book { get; set; }
    }
}
