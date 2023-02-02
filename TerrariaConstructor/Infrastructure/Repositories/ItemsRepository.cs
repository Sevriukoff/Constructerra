using LiteDB;
using TerrariaConstructor.Infrastructure.Interfaces;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.Infrastructure.Repositories;

public class ItemsRepository : IItemsRepository
{
    private readonly ILiteDatabase _itemsDatabase;

    public ItemsRepository(ILiteDatabase itemsDatabase)
    {
        _itemsDatabase = itemsDatabase;
    }

    public Item GetById(int id)
    {
        var result =_itemsDatabase.GetCollection<Item>("items").FindOne(x => x.Id == id);

        return result;
    }

    public Item GetByName(string name)
    {
        var result = _itemsDatabase.GetCollection<Item>("items").FindOne(x => x.Name == name);

        return result;
    }
}