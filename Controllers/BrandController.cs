using eTech.Entities;
using eTech.Entities.Requests;
using eTech.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eTech.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : ControllerBase {
        private readonly IBrandService _brandService;
        private readonly IImageService _imageService;
        public BrandController(IBrandService brandService, IImageService imageService) {
            _brandService = brandService;
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            return Ok(await _brandService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {
            return Ok(await _brandService.GetById(id));
        }

        [HttpGet("{id}/image")]
        public async Task<IActionResult> GetImage(int id) {
            Brand brand = await _brandService.GetById(id);
            if (brand == null) {
                return NotFound();
            }
            return await _imageService.GetImage(brand.Image.FileName);
        }


        [HttpPost]
        [Authorize]
        // Experimental
        public Task<Brand> Add([FromForm] BrandRequestAdd brandRequest) {
            Image image = _imageService.Upload(brandRequest.Image).Result;
            Brand brand = new Brand() {
                Name = brandRequest.Name,
                Country = brandRequest.Country,
                Products = new List<Product>(),
                Image = image
            };
            return _brandService.Add(brand);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromForm] BrandRequestUpdate brand) {
            Brand currentBrand = await _brandService.GetById(brand.Id);
            if (currentBrand == null) {
                return BadRequest("Brand not found");
            }
            currentBrand.Name = brand.Name;
            currentBrand.Country = brand.Country;
            Image img = currentBrand.Image;
            if (brand.Image != null) {
                Image image = _imageService.Upload(brand.Image).Result;
                if (image == null) {
                    return BadRequest("Image upload failed");
                }
                currentBrand.Image = null;

                currentBrand.Image = image;
            }
            Brand updatedBrand = await _brandService.Update(currentBrand);
            await _imageService.DeleteImage(img);
            return Ok(updatedBrand);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public Task Delete(int id) {
            return _brandService.Delete(id);
        }
    }
}