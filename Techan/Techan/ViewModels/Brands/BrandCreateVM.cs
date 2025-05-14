using Microsoft.AspNetCore.Http;

namespace Techan.ViewModels.Brand
{
    public class BrandCreateVM
    {
        public string Name { get; set; }
        public IFormFile ImagePath { get; set; }
        public IFormFile ImageFile { get; internal set; }
    }
}
