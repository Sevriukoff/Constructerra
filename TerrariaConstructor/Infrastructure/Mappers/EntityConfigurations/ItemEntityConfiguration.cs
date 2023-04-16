using System;
using LiteDB;
using TerrariaConstructor.Infrastructure.Interfaces;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.Infrastructure.Mappers.EntityConfigurations;

public class ItemEntityConfiguration : IEntityConfiguration<Item>
{
    public BsonValue Serialize(Item obj)
    {
        return new BsonDocument
        {
            {"_id", obj.Id},
            {"MaxStack", 1},
            {"Name", obj.Name},
            {"RuName", obj.Name},
            {"ToolTip", string.Empty},
            {"Rarity", null},
            {"Damage", 0},
            {"DamageType", null},
            {"CriticalChance", 0},
            {"Knockback", 0},
            {"KnockbackType", null},
            {"QuantityForResearch", 1},
            {"UseTime", 0},
            {"UseTimeType", null},
            {"PickaxePower", 0},
            {"AxePower", 0},
            {"HammerPower", 0},
            {"IsPlaceable", false},
            {"Types", null},
            {"ImagePath", string.Empty},
            {"RuToolTip", string.Empty}
        };
    }

    public Item Deserialize(BsonValue bsonValue)
    {
        var result = new Item
        {
            Id = bsonValue["_id"].AsInt32,
            Name = string.IsNullOrEmpty(bsonValue["RuName"].AsString) ? bsonValue["Name"] : bsonValue["RuName"],
            InternalName = bsonValue["InternalName"].AsString,
            Tooltip = bsonValue["RuToolTip"].AsString ?? "",
            Rarity = Enum.Parse<ItemRarity>(bsonValue["Rarity"].AsString),
            Damage = bsonValue["Damage"].AsInt32,
            DamageType = Enum.Parse<DamageType>(bsonValue["DamageType"].AsString),
            CriticalChance = bsonValue["CriticalChance"].AsInt32,
            Knockback = bsonValue["Knockback"].AsDouble,
            KnockbackType = Enum.Parse<KnockbackType>(bsonValue["KnockbackType"]),
            UseTime = bsonValue["UseTime"].AsInt32,
            PickaxePower = bsonValue["PickaxePower"].AsInt32,
            AxePower = bsonValue["AxePower"].AsInt32,
            HammerPower = bsonValue["HammerPower"].AsInt32,
            DefensePoint = bsonValue["Defense"].AsInt32,
            QuantityForResearch = bsonValue["QuantityForResearch"].AsInt32,
            CostByBuy = bsonValue["Buy"].AsInt32,
            CostBySell = bsonValue["Sell"],
            MaxStack = bsonValue["MaxStack"].AsInt32,
            IsPlaceable = bsonValue["IsPlaceable"].AsBoolean,
            WikiUrl = bsonValue["WikiUrl"].AsString,
            Description = string.IsNullOrEmpty(bsonValue["RuToolTip"].AsString)
                ? bsonValue["Types"].AsArray.Count > 0 ? bsonValue["Types"].AsArray[0].AsString : "None"
                : bsonValue["RuToolTip"]
        };

        var types = bsonValue["Types"].AsArray;

        string[] categories = new string[types.Count];

        for (int i = 0; i < types.Count; i++)
        {
            categories[i] = types[i].AsString;
        }

        result.Categories = categories;

        return result;
    }
}