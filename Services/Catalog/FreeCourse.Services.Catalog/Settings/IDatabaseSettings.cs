namespace FreeCourse.Services.Catalog.Settings
{
    public interface IDatabaseSettings //Config ayarlarını bir sınıf üzerinden okumak için options pattern kullanıyoruz. O yüzden interface oluşturduk.
    {
        public string CourseCollectionName { get; set; }

        public string CategoryCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}
