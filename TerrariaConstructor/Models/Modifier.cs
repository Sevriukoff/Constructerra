using System;
using LiteDB;

namespace TerrariaConstructor.Models;

public class Modifier
{
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public int Damage { get; set; }
    public int MovementSpeed { get; set; }
    public int MeleeSpeed { get; set; }
    public int CriticalChance { get; set; }
    public int DefensePoints { get; set; }
    public int ManaCost { get; set; }
    public int Size { get; set; }
    public int Velocity { get; set; }
    public int Knockback { get; set; }
    public int Tier { get; set; }
    public double Value { get; set; }
}

public class PrefixModifier
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public int Damage { get; set; }
    public int MovementSpeed { get; set; }
    public int MeleeSpeed { get; set; }
    public int CriticalChance { get; set; }
    public int DefensePoints { get; set; }
    public int ManaCost { get; set; }
    public int Size { get; set; }
    public int Velocity { get; set; }
    public int Knockback { get; set; }
    public int Tier { get; set; }
    public double Value { get; set; }
}