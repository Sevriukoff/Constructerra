using LiteDB;
using TerrariaConstructor.Infrastructure.Interfaces;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.Infrastructure.Repositories;

public class BuffsRepository : IBuffsRepository
{
    private readonly ILiteDatabase _itemsDatabase;

    public BuffsRepository(ILiteDatabase itemsDatabase)
    {
        _itemsDatabase = itemsDatabase;
    }

    public Buff GetById(int id)
    {
        var result = _itemsDatabase.GetCollection<Buff>("Buffs").FindOne(x => x.Id == id);

        return result;
    }
}