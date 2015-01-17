using System;

namespace Roomie.Web.Persistence.Models
{
    public class ShallowTaskModel
    {
        public int Id { get; set; }
        public int? Owner { get; set; }
        public int? Target { get; set; }
        public string Origin { get; set; }
        public ScriptModel Script { get; set; }
        public DateTime? Expiration { get; set; }
        public DateTime? ReceivedTimestamp { get; set; }

        public static ShallowTaskModel FromTaskModel(TaskModel model)
        {
            var result = new ShallowTaskModel
            {
                Expiration = model.Expiration,
                Id = model.Id,
                Origin = model.Origin,
                ReceivedTimestamp = model.ReceivedTimestamp,
                Script = model.Script
            };

            if (model.Owner != null)
            {
                result.Owner = model.Owner.Id;
            }

            if (model.Target != null)
            {
                result.Target = model.Target.Id;
            }

            return result;
        }
    }
}
