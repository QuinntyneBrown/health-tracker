using HealthTracker.Data.Model;
using System;

namespace HealthTracker.Features.Biometrics
{
    public class WeightSnapShotApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }        
        public float Pounds { get; set; }
        public DateTime WeighedOn { get; set; }
        public static TModel FromWeightSnapShot<TModel>(WeightSnapShot weightSnapShot) where
            TModel : WeightSnapShotApiModel, new()
        {
            var model = new TModel();
            model.Id = weightSnapShot.Id;
            model.TenantId = weightSnapShot.TenantId;
            model.Pounds = weightSnapShot.Pounds;
            model.WeighedOn = weightSnapShot.WeighedOn;
            return model;
        }

        public static WeightSnapShotApiModel FromWeightSnapShot(WeightSnapShot weightSnapShot)
            => FromWeightSnapShot<WeightSnapShotApiModel>(weightSnapShot);

    }
}
