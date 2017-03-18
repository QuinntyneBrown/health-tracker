using HealthTracker.Data.Model;
using System.Collections.Generic;
using System.Linq;

namespace HealthTracker.Features.Profiles
{
    public class ProfileApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }
        public ICollection<WeightSnapShotApiModel> WeightSnapShots { get; set; }

        public static TModel FromProfile<TModel>(Profile profile) where
            TModel : ProfileApiModel, new()
        {
            var model = new TModel();
            model.Id = profile.Id;
            model.TenantId = profile.TenantId;
            model.Name = profile.Name;
            model.WeightSnapShots = profile.WeightSnapShots.Select(x => WeightSnapShotApiModel.FromWeightSnapShot(x)).ToList();
            return model;
        }

        public static ProfileApiModel FromProfile(Profile profile)
            => FromProfile<ProfileApiModel>(profile);

    }
}
