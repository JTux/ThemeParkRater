using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThemeParkRater.Data;
using ThemeParkRater.Models.ThemeParkModels;

namespace ThemeParkRater.Services
{
    public class ThemeParkService
    {
        public bool CreateThemePark(ThemeParkCreate model)
        {
            ThemePark themePark = new ThemePark()
            {
                ThemeParkName = model.ThemeParkName,
                ThemeParkCity = model.ThemeParkCity,
                ThemeParkState = model.ThemeParkState
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.ThemeParks.Add(themePark);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ThemeParkListItem> GetThemeParks()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .ThemeParks
                    .Select(p => new ThemeParkListItem
                    {
                        ThemeParkID = p.ThemeParkID,
                        ThemeParkName = p.ThemeParkName,
                        ThemeParkState = p.ThemeParkState,
                        GoodnessLevel = p.GoodnessLevel
                    });

                return query.ToArray();
            }
        }

        public ThemeParkDetail GetParkByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.ThemeParks.FirstOrDefault(p => p.ThemeParkID == id);

                var model = new ThemeParkDetail
                {
                    ThemeParkID = entity.ThemeParkID,
                    ThemeParkName = entity.ThemeParkName,
                    ThemeParkCity = entity.ThemeParkCity,
                    ThemeParkState = entity.ThemeParkState,
                    GoodnessLevel = entity.GoodnessLevel
                };

                return model;
            }
        }

        public bool EditThemePark(ThemeParkEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.ThemeParks.FirstOrDefault(p => p.ThemeParkID == model.ThemeParkID);

                entity.ThemeParkName = model.ThemeParkName;
                entity.ThemeParkCity = model.ThemeParkCity;
                entity.ThemeParkState = model.ThemeParkState;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteThemePark(int id)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity = ctx.ThemeParks.Single(p => p.ThemeParkID == id);

                ctx.ThemeParks.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        
    }
}
