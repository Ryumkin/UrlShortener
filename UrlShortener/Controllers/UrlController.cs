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
        public ActionResult<UrlDTO> Get(UrlDTO url)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            
            var urlService = new UrlService();
            var idhash = urlService.Decode(url.Url);
            var result = _dbContext.Urls.Where(idhash => idhash.IdHash == idhash.IdHash)
                ?.Select(x=>x.LongUrl)
                ?.FirstOrDefault();

            return new UrlDTO() { Url= result };
        }

        //[HttpPost]
        //public ActionResult<Url> Create(XZ URLRG)
        //{
        //    if (URLRG == null)
        //        return BadRequest();

        //    var uri = new Uri(URLRG.URLRG);
        //    var result = _context.Url.Where(x => x.LongUl == uri.ToString())?.FirstOrDefault();
        //    if (result != null)
        //        return result;
        //    var url = new Url
        //    {
        //        LongUl = uri.ToString()
        //    };

        //    _context.Url.Add(url);
        //    _context.SaveChanges();
        //    var helper = new UrlHelper();
        //    url.ShortUrl = helper.Encode(url.IdHash);
        //    _context.SaveChanges();
        //    return url;

        //}
    }
}