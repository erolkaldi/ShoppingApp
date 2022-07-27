using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Definition.DefinitionModels;

namespace ShoppingApp.DefinitionApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private IConfiguration _configuration;

        public OfferController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Authorize]
        public IActionResult GetActiveOffers()
        {
            List<OfferDto> offers = new List<OfferDto>();
            offers.Add(new OfferDto
            {
                Title = "Aaaaa",
                Description = "Bbbbbbb bbbbbbb",
                PhotoUrl = "https://sedna360.s3.eu-central-1.amazonaws.com/Trace/Photos/00608407-d52c-4cb2-98b5-7888d6c5976c.jpeg"
            });
            offers.Add(new OfferDto
            {
                Title = "Cccccc",
                Description = "Ddddd ddd dddddddv",
                PhotoUrl = "https://sedna360.s3.eu-central-1.amazonaws.com/Trace/Photos/00608407-d52c-4cb2-98b5-7888d6c5976c.jpeg"
            });
            return Ok(offers);
        }
    }
}
