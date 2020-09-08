using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Wallets_API.Data;
using Wallets_API.DBClasses;
using Wallets_API.DTO;
using Wallets_API.Helpers;
using Wallets_API.Models;

namespace Wallets_API.Controllers
{
    [Route("api/[controller]/{userId}")]
    [ApiController]
    public class PhotoController : ControllerBase
    {

        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public PhotoController(IOptions<CloudinarySettings> cloudinaryConfig, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _cloudinaryConfig = cloudinaryConfig;

            Account acc = new Account(
            _cloudinaryConfig.Value.CloudName,
            _cloudinaryConfig.Value.ApiKey,
            _cloudinaryConfig.Value.ApiSecret
        );
            _userManager = userManager;
            _context = context;

            _cloudinary = new Cloudinary(acc);
        }


        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(string userId, [FromForm]PhotoForCreationDTO photoForCreationDto)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value != userId)
                return Unauthorized();
            var photoDeleteResult = PhotoDelete(userId);
            var file = photoForCreationDto.File;

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation()
                            .Width(300).Height(300).Crop("fill").Gravity("face")
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            photoForCreationDto.Url = uploadResult.Uri.ToString();
            photoForCreationDto.PublicId = uploadResult.PublicId;

            Photo photo = new Photo
            {
                Url = photoForCreationDto.Url,
                PublicId = photoForCreationDto.PublicId,
                DateAdded = photoForCreationDto.DateAdded,
            };
            await _context.Photos.AddAsync(photo);
            await _context.SaveChangesAsync();



            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            user.UserPhotoId = photo.Id;
            await _context.SaveChangesAsync();
            return Ok(photo);
        }

        [HttpGet]
        public async Task<IActionResult> GetPhoto(string userId)
        {
            var user = await _userManager.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            var photoToReturn = await _context.Photos.Where(p => p.Id == user.UserPhotoId).FirstOrDefaultAsync();
            return Ok(photoToReturn);
        }


        [HttpDelete]
        public async Task<IActionResult> DeletePhoto(string userId)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value != userId)
                return Unauthorized();

            var photoDeleteResult = PhotoDelete(userId);
            if (await photoDeleteResult)
                return Ok();
            else
                return BadRequest("Failed to delete the photo");
        }

        private async Task<bool> PhotoDelete(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

            var photo = await _context.Photos.Where(p => p.Id == user.UserPhotoId).FirstOrDefaultAsync();

            if (photo.PublicId != null)
            {
                var deleteParams = new DeletionParams(photo.PublicId);

                var result = _cloudinary.Destroy(deleteParams);

                if (result.Result == "ok")
                {
                    _context.Photos.Remove(photo);
                    user.UserPhotoId = 0;
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            return false;
        }
    }
}