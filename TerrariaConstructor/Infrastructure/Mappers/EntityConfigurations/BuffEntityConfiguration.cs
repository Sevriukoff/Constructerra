using System;
using LiteDB;
using TerrariaConstructor.Infrastructure.Interfaces;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.Infrastructure.Mappers.EntityConfigurations;

public class BuffEntityConfiguration : IEntityConfiguration<Buff>
{
    public BsonValue Serialize(Buff obj)
    {
        return new BsonDocument
        {
            {"_id", obj.Id}
        };
    }

    public Buff Deserialize(BsonValue bsonValue)
    {
        var result = new Buff();

        result.Id = bsonValue["_id"].AsInt32;
        result.Name = bsonValue["RuName"].AsString;
        result.IsNegative = bsonValue["Type"].AsString == "Debuff";
        result.ToolTip = bsonValue["RuTooltip"].AsString;
        result.EffectDescription = bsonValue["RuEffectDescription"].AsString;
        result.ImageId = bsonValue["ImageId"].AsInt32;
        result.WikiUrl = bsonValue["WikiUrl"].AsString;
                
        if (bsonValue["Durations"].Type != BsonType.Null)
        {
            result.DurationTime = TimeSpan.FromTicks(bsonValue["Durations"].AsArray[0].AsInt64);
        }

        return result;
    }
}