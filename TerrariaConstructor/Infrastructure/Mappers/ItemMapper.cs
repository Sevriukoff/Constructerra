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
                var result = new Item
                {
                    Id = b["_id"].AsInt32,
                    Name = string.IsNullOrEmpty(b["RuName"].AsString) ? b["Name"] : b["RuName"],
                    InternalName = b["InternalName"].AsString,
                    Tooltip = b["RuToolTip"].AsString,
                    Rarity = Enum.Parse<ItemRarity>(b["Rarity"].AsString),
                    Damage = b["Damage"].AsInt32,
                    DamageType = Enum.Parse<DamageType>(b["DamageType"].AsString),
                    CriticalChance = b["CriticalChance"].AsInt32,
                    Knockback = b["Knockback"].AsDouble,
                    KnockbackType = Enum.Parse<KnockbackType>(b["KnockbackType"]),
                    UseTime = b["UseTime"].AsInt32,
                    PickaxePower = b["PickaxePower"].AsInt32,
                    AxePower = b["AxePower"].AsInt32,
                    HammerPower = b["HammerPower"].AsInt32,
                    DefensePoint = b["Defense"].AsInt32,
                    QuantityForResearch = b["QuantityForResearch"].AsInt32,
                    CostByBuy = b["Buy"].AsInt32,
                    CostBySell = b["Sell"],
                    MaxStack = b["MaxStack"].AsInt32,
                    IsPlaceable = b["IsPlaceable"].AsBoolean,
                    WikiUrl = b["WikiUrl"].AsString,
                    Description = string.IsNullOrEmpty(b["RuToolTip"].AsString)
                        ? b["Types"].AsArray.Count > 0 ? b["Types"].AsArray[0].AsString : "None"
                        : b["RuToolTip"]
                };

                return result;
            });
        
        this.RegisterType(
            (Modifier m) => new BsonDocument
            {
                
            },
            (BsonValue b) =>
            {
                var result = new Modifier();

                result.Id = b["_id"].AsObjectId;
                result.Name = b["Name"].AsString;
                result.Category = b["Category"].AsString;
                result.Damage = b["Damage"].AsInt32;
                result.MovementSpeed = b["MovementSpeed"].AsInt32;
                result.MeleeSpeed = b["MeleeSpeed"].AsInt32;
                result.CriticalChance = b["CriticalChance"].AsInt32;
                result.DefensePoints = b["Defense"].AsInt32;
                result.ManaCost = b["ManaCost"].AsInt32;
                result.Size = b["Size"].AsInt32;
                result.Velocity = b["Velocity"].AsInt32;
                result.Knockback = b["Knockback"].AsInt32;
                result.Tier = b["Tier"].AsInt32;
                result.Value = b["Value"].AsDouble;
                
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