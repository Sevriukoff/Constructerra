using System.IO;
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

    public Item GetById(int id, bool loadImage = true)
    {
        var result =_itemsDatabase.GetCollection<Item>("items").FindOne(x => x.Id == id);
        
        if (result != null && loadImage)
        {
            var fileInfo = _itemsDatabase.FileStorage.OpenRead(result.Id.ToString());
            result.Image = new byte[fileInfo.Length];
            fileInfo.Read(result.Image, 0, (int) fileInfo.Length);
        }
        
        return result ?? new Item {Id = id};
    }

    public Item GetByName(string name)
    {
        var result = _itemsDatabase.GetCollection<Item>("items").FindOne(x => x.Name == name);

        return result;
    }
}