using System;
using LiteDB;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.Infrastructure.Mappers;

public class ItemMapper : BsonMapper
{
    public ItemMapper()
    {
        this.RegisterType(
            (Buff b) => new BsonDocument
            {
                {"_id", b.Id}
            }, 
            (value =>
            {
                var result = new Buff();

                result.Id = value["_id"].AsInt32;
                result.Name = value["RuName"].AsString;
                result.IsNegative = value["Type"].AsString == "Debuff";
                result.ToolTip = value["RuTooltip"].AsString;
                result.EffectDescription = value["RuEffectDescription"].AsString;
                result.ImageId = value["ImageId"].AsInt32;
                
                if (value["Durations"].Type != BsonType.Null)
                {
                    result.DurationTime = TimeSpan.FromTicks(value["Durations"].AsArray[0].AsInt64);
                }

                return result;
            }));
        
        this.RegisterType(
            (Item i) => new BsonDocument
            {
                { "_id", i.Id },
                { "MaxStack", 1 },
                { "Name", i.Name },
                { "RuName", i.Name },
                { "ToolTip", string.Empty },
                { "Rarity", null },
                { "Damage", 0 },
                { "DamageType", null },
                { "CriticalChance", 0 },
                { "Knockback", 0 },
                { "KnockbackType", null },
                { "QuantityForResearch", 1 },
                { "UseTime", 0 },
                { "UseTimeType", null },
                { "PickaxePower", 0 },
                { "AxePower", 0 },
                { "HammerPower", 0 },
                { "IsPlaceable", false },
                { "Types", null },
                { "ImagePath", string.Empty },
                { "RuToolTip", string.Empty }
            },
            (BsonValue b) =>
            {
                var result = new Item();

                result.Id = b["_id"].AsInt32;
                result.Name = string.IsNullOrEmpty(b["RuName"].AsString) ? b["Name"] : b["RuName"];
                result.Description = string.IsNullOrEmpty(b["RuToolTip"].AsString)
                    ? b["Types"].AsArray.Count > 0 ? b["Types"].AsArray[0].AsString : "None"
                    : b["RuToolTip"];
                result.Sell = b["Sell"];

                return result;
            });
    }
    
    /*
     
     private BsonMapper _mapper = Global;
     
    public void Register()
    {
        _mapper.Entity<Item>()
            .Field(x => x.Name, "RuName");
    }
    */
}