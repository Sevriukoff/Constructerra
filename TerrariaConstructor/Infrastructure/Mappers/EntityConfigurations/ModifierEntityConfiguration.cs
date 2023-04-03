using System;
using LiteDB;
using TerrariaConstructor.Infrastructure.Interfaces;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.Infrastructure.Mappers.EntityConfigurations;

public class ModifierEntityConfiguration : IEntityConfiguration<Modifier>
{
    public BsonValue Serialize(Modifier obj)
    {
        return new BsonDocument();
    }

    public Modifier Deserialize(BsonValue bsonValue)
    {
        var result = new Modifier();

        result.Id = bsonValue["_id"].AsInt32;
        result.Name = bsonValue["Name"].AsString;
        result.Category = bsonValue["Category"].AsString;
        result.Damage = bsonValue["Damage"].AsInt32;
        result.MovementSpeed = bsonValue["MovementSpeed"].AsInt32;
        result.MeleeSpeed = bsonValue["MeleeSpeed"].AsInt32;
        result.CriticalChance = bsonValue["CriticalChance"].AsInt32;
        result.DefensePoints = bsonValue["Defense"].AsInt32;
        result.ManaCost = bsonValue["ManaCost"].AsInt32;
        result.Size = bsonValue["Size"].AsInt32;
        result.Velocity = bsonValue["Velocity"].AsInt32;
        result.Knockback = bsonValue["Knockback"].AsInt32;
        result.Tier = bsonValue["Tier"].AsInt32;
        result.Value = bsonValue["Value"].AsDouble;
                
        return result;
    }
}