namespace Techan.ViewModels.Brand
{
    public class BrandGetVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile ImagePath { get; set; }
        public IFormFile ImageFile { get; internal set; }
        public string ImageUrl { get; internal set; }
    }
}
