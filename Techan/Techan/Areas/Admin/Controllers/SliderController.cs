using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Techan.DataAcessLayer;
using Techan.Models;
using Techan.ViewModels.Sliders;

namespace Techan.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Superadmin,Admin,Moderator")]
    public class SliderController(TechanDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Slider> datas = [];
            datas = await _context.Sliders.ToListAsync();
            List<SliderGetVM> sliders = [];
            foreach (var item in datas)
            {
                sliders.Add(new SliderGetVM()
                {
                    BigTitle = item.BigTitle,
                    Id = item.Id,
                    ImagePath = item.ImagePath,
                    Link = item.Link,
                    LittleTitle = item.LittleTitle,
                    Offer = item.Offer,
                    Title = item.Title
                });
            }
            return View(sliders);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM model)
        {
            if (!ModelState.IsValid)
                return View(model);
            if (!model.Image.ContentType.StartsWith("image"))
            {
                string ext = Path.GetExtension(model.Image.FileName);
                ModelState.AddModelError("ImagePath", "File must be page format");
            }
            if (model.Image.Length / 1024 > 200)
            {
                ModelState.AddModelError("ImagePath", "Size must not be greater than 200 kb");
            }
            string NewName = Path.GetRandomFileName() + Path.GetExtension(model.Image!.FileName);
            string path = Path.Combine("wwwroot", "imgs", "sliders", NewName);
            await using (FileStream fs = System.IO.File.Create(path))
            {
                await model.Image.CopyToAsync(fs);
            }
            Slider slider = new()
            {
                BigTitle = model.BigTitle,
                ImagePath = NewName,
                Link = model.Link,
                LittleTitle = model.LittleTitle,
                Offer = model.Offer,
                Title = model.Title
            };
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue || id.Value < 1) return BadRequest();
            int result = await _context.Sliders.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (result == 0)
                return NotFound();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id.HasValue && id < 1) return BadRequest();
            var entity = await _context.Sliders.Select(x => new SliderUpdateVM
            {
                Id = x.Id,
                Title = x.Title,
                BigTitle = x.BigTitle,
                Offer = x.Offer,
                Link = x.Link,
                ImagePath = x.ImagePath,
                LittleTitle = x.LittleTitle,
            }).FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return NotFound();
            return View(entity);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, SliderUpdateVM vm)
        {
            if (id.HasValue && id < 1)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(vm);

            var entity = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return BadRequest();

            if (vm.Image != null)
            {
                if (!vm.Image.ContentType.StartsWith("image"))
                {
                    string ext = Path.GetExtension(vm.Image.FileName);
                    ModelState.AddModelError("ImagePath", "File must be page format");
                }
                if (vm.Image.Length / 1024 > 200)
                {
                    ModelState.AddModelError("ImagePath", "Size must not be greater than 200 kb");
                }
                string newFileName = Path.GetRandomFileName() + Path.GetExtension(vm.Image.FileName);
                string newPath = Path.Combine("wwwroot", "imgs", "sliders", newFileName);
                await using (FileStream fs = System.IO.File.Create(newPath))
                {
                    await vm.Image.CopyToAsync(fs);
                }
                entity.ImagePath = newFileName;
                entity.BigTitle = vm.BigTitle;
                entity.Offer = vm.Offer;
                entity.Link = vm.Link;
                entity.Title = vm.Title;
                entity.LittleTitle = vm.LittleTitle;
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
