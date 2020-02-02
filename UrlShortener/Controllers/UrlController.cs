using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.DTO;
using UrlShortener.Helpers;
using UrlShortener.Models;

namespace UrlShortener.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UrlController : ControllerBase
    {
        private readonly UrlShortnerContext _dbContext;

        public UrlController(UrlShortnerContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }


        [HttpPost]
        [Route("Search")]
        public ActionResult<UrlDTO> Search(UrlDTO url)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var urlService = new UrlService();
            var idhash = urlService.Decode(url.Url);
            var result = _dbContext.Urls.Where(x => x.IdHash == idhash)
                ?.Select(x => x.LongUrl)
                ?.FirstOrDefault();

            return new UrlDTO() { Url = result };
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<UrlDTO>> Create(UrlDTO urlDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var url = new Url
            {
                LongUrl = urlDto.Url
            };

            await _dbContext.Urls.AddAsync(url);
            await _dbContext.SaveChangesAsync();
            var urlService = new UrlService();
            var shortUrl = urlService.Encode(url.IdHash);
            return new UrlDTO() { Url = shortUrl };
        }
    }
}