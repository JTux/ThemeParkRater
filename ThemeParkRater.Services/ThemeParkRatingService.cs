using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThemeParkRater.Data;
using ThemeParkRater.Models.ThemeParkRatingModels;

namespace ThemeParkRater.Services
{
    public class ThemeParkRatingService
    {
        private readonly Guid _userId;

        public ThemeParkRatingService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateRating(ThemeParkRatingCreate model)
        {
            var rating = new ThemeParkRating
            {
                ThemeParkID = model.ThemeParkID,
                GoodnessLevel = model.GoodnessLevel,
                OwnerID = _userId
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Ratings.Add(rating);
                if (ctx.SaveChanges() == 1)
                {
                    CalculateGoodness(rating.ThemeParkID);
                    return true;
                }
                return false;
            }
        }

        public IEnumerable<ThemeParkRatingListItem> GetRatingsByParkID(int parkId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Ratings
                    .Where(r => r.ThemeParkID == parkId)
                    .Select(r => new ThemeParkRatingListItem
                    {
                        ThemeParkRatingID = r.ThemeParkRatingID,
                        ThemeParkID = r.ThemeParkID,
                        GoodnessLevel = r.GoodnessLevel,
                        ThemeParkName = r.ThemePark.ThemeParkName,
                        ThemeParkState = r.ThemePark.ThemeParkState
                    }).ToArray();

                return query;
            }
        }

        public ThemeParkRatingDetail GetRatingByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Ratings.Single(r => r.ThemeParkID == id);

                var model = new ThemeParkRatingDetail
                {
                    ThemeParkRatingID = entity.ThemeParkRatingID,
                    ThemeParkID = entity.ThemeParkID,
                    ThemeParkName = entity.ThemePark.ThemeParkName,
                    GoodnessLevel = entity.GoodnessLevel
                };

                return model;
            }
        }

        public bool EditThemeParkRating(ThemeParkRatingEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Ratings.Single(r => r.ThemeParkRatingID == model.ThemeParkRatingID);

                entity.GoodnessLevel = model.GoodnessLevel;
                entity.ThemeParkID = model.ThemeParkID;

                if (ctx.SaveChanges() == 1)
                {
                    CalculateGoodness(model.ThemeParkID);
                    return true;
                }
                return false;
            }
        }

        public bool DeleteThemeParkRating(int ratingId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Ratings.Single(r => r.ThemeParkRatingID == ratingId);

                ctx.Ratings.Remove(entity);

                if (ctx.SaveChanges() == 1)
                {
                    CalculateGoodness(entity.ThemeParkID);
                    return true;
                }
                return false;
            }
        }

        private bool CalculateGoodness(int parkId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Ratings.Where(r => r.ThemeParkID == parkId).ToList();

                float totalGoodness = 0;
                foreach (var rating in query)
                {
                    totalGoodness += rating.GoodnessLevel;
                }
                totalGoodness /= query.Count;

                var park = ctx.ThemeParks.Single(p => p.ThemeParkID == parkId);
                park.GoodnessLevel = totalGoodness;

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
