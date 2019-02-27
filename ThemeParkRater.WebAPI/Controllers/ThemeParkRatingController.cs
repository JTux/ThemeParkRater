using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ThemeParkRater.Models.ThemeParkRatingModels;
using ThemeParkRater.Services;

namespace ThemeParkRater.WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/ThemeParkRating")]
    public class ThemeParkRatingController : ApiController
    {

        [Route("ByParkID/{id:int}")]
        public IHttpActionResult GetByParkId(int id)
        {
            var service = GetRatingService();
            var ratings = service.GetRatingsByParkID(id);
            return Ok(ratings);
        }

        [Route("ByRatingID/{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            var service = GetRatingService();
            var park = service.GetRatingByID(id);
            return Ok(park);
        }

        public IHttpActionResult Post(ThemeParkRatingCreate rating)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = GetRatingService();

            if (!service.CreateRating(rating))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Put(ThemeParkRatingEdit rating)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = GetRatingService();

            if (!service.EditThemeParkRating(rating))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var service = GetRatingService();

            if (!service.DeleteThemeParkRating(id))
                return InternalServerError();

            return Ok();
        }

        private ThemeParkRatingService GetRatingService()
        {
            return new ThemeParkRatingService(Guid.Parse(User.Identity.GetUserId()));
        }
    }
}
