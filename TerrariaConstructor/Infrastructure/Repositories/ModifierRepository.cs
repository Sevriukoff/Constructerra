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
        var result = _itemsDatabase.GetCollection<Modifier>("modifiers")
            .FindOne(x => x.Id == id);

        return result ?? new Modifier {Id = id};
    }
}