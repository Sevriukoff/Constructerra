using System;
using System.Collections.Generic;
using System.Linq;
using LiteDB;
using TerrariaConstructor.Infrastructure.Interfaces;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.Infrastructure.Repositories;

public class ModifierRepository : IModifierRepository
{
    private readonly ILiteDatabase _itemsDatabase;

    public ModifierRepository(ILiteDatabase itemsDatabase)
    {
        _itemsDatabase = itemsDatabase;
    }

    public Modifier GetById(int id)
    {
        var collection = _itemsDatabase.GetCollection<Modifier>("modifiers");

        var modifiers = collection.FindAll();
        var prefixModifiers = new List<PrefixModifier>(collection.Count());

        foreach (var modifier in modifiers)
        {
            var prefixModifier = new PrefixModifier();
            prefixModifier.Name = modifier.Name;
            prefixModifier.Category = modifier.Category;
            prefixModifier.Damage = modifier.Damage;
            prefixModifier.MovementSpeed = modifier.MovementSpeed;
            prefixModifier.MeleeSpeed = modifier.MeleeSpeed;
            prefixModifier.CriticalChance = modifier.CriticalChance;
            prefixModifier.DefensePoints = modifier.DefensePoints;
            prefixModifier.ManaCost = modifier.ManaCost;
            prefixModifier.Size = modifier.Size;
            prefixModifier.Velocity = modifier.Velocity;
            prefixModifier.Knockback = modifier.Knockback;
            prefixModifier.Tier = modifier.Tier;
            prefixModifier.Value = modifier.Value;

            int prefixModifierId = -1;

            if (prefixModifier.Name == "Deadly" && prefixModifier.Category == "Weapon/Ranged")
            {
                prefixModifier.Id = 20;
                prefixModifiers.Add(prefixModifier);
                continue;
            }
            
            if (prefixModifier.Name == "Deadly" && prefixModifier.Category == "Weapon/Common")
            {
                prefixModifier.Id = 43;
                prefixModifiers.Add(prefixModifier);
                continue;
            }
            
            if (prefixModifier.Name == "Hasty" && prefixModifier.Category == "Weapon/Ranged")
            {
                prefixModifier.Id = 18;
                prefixModifiers.Add(prefixModifier);
                continue;
            }
            
            if (prefixModifier.Name == "Hasty" && prefixModifier.Category == "Accessory")
            {
                prefixModifier.Id = 75;
                prefixModifiers.Add(prefixModifier);
                continue;
            }
            
            if (prefixModifier.Name == "Quick" && prefixModifier.Category == "Weapon/Common")
            {
                prefixModifier.Id = 42;
                prefixModifiers.Add(prefixModifier);
                continue;
            }
            
            if (prefixModifier.Name == "Quick" && prefixModifier.Category == "Accessory")
            {
                prefixModifier.Id = 76;
                prefixModifiers.Add(prefixModifier);
                continue;
            }

            prefixModifierId = prefixModifier.Name switch
            {
                "Large" => 1,
                "Massive" => 2,
                "Dangerous" => 3,
                "Savage" => 4,
                "Sharp" => 5,
                "Pointy" => 6,
                "Tiny" => 7,
                "Terrible" => 8,
                "Small" => 9,
                "Dull" => 10,
                "Unhappy" => 11,
                "Bulky" => 12,
                "Shameful" => 13,
                "Heavy" => 14,
                "Light" => 15,
                "Sighted" => 16,
                "Rapid" => 17,
                "Intimidating" => 19,
                "Staunch" => 21,
                "Awful" => 22,
                "Lethargic" => 23,
                "Awkward" => 24,
                "Powerful" => 25,
                "Mystic" => 26,
                "Adept" => 27,
                "Masterful" => 28,
                "Inept" => 29,
                "Ignorant" => 30,
                "Deranged" => 31,
                "Intense" => 32,
                "Taboo" => 33,
                "Celestial" => 34,
                "Furious" => 35,
                "Keen" => 36,
                "Superior" => 37,
                "Forceful" => 38,
                "Broken" => 39,
                "Damaged" => 40,
                "Shoddy" => 41,
                "Agile" => 44,
                "Nimble" => 45,
                "Murderous" => 46,
                "Slow" => 47,
                "Sluggish" => 48,
                "Lazy" => 49,
                "Annoying" => 50,
                "Nasty" => 51,
                "Manic" => 52,
                "Hurtful" => 53,
                "Strong" => 54,
                "Unpleasant" => 55,
                "Weak" => 56,
                "Ruthless" => 57,
                "Frenzying" => 58,
                "Godly" => 59,
                "Demonic" => 60,
                "Zealous" => 61,
                "Hard" => 62,
                "Guarding" => 63,
                "Armored" => 64,
                "Warding" => 65,
                "Arcane" => 66,
                "Precise" => 67,
                "Lucky" => 68,
                "Jagged" => 69,
                "Spiked" => 70,
                "Angry" => 71,
                "Menacing" => 72,
                "Brisk" => 73,
                "Fleeting" => 74,
                "Wild" => 77,
                "Rash" => 78,
                "Intrepid" => 79,
                "Violent" => 80,
                "Legendary" => 81,
                "Unreal" => 82,
                "Mythical" => 83,
                _ => throw new ArgumentException()
            };

            prefixModifier.Id = prefixModifierId;
            
            prefixModifiers.Add(prefixModifier);
        }

        _itemsDatabase.DropCollection("modifiers");
        var newCollection = _itemsDatabase.GetCollection<PrefixModifier>("modifiers");
        newCollection.Insert(prefixModifiers);
        
        return new Modifier();
    }
}