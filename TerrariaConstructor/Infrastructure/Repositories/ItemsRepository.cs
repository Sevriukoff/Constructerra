using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        var result =_itemsDatabase.GetCollection<Item>("items")
            .FindOne(x => x.Id == id);
        
        if (result != null && loadImage)
        {
            var fileInfo = _itemsDatabase.FileStorage.OpenRead(result.Id.ToString());
            result.Image = new byte[fileInfo.Length];
            fileInfo.Read(result.Image, 0, (int) fileInfo.Length);
        }
        
        return result ?? new Item {Id = id};
    }

    public Item GetByInternalName(string internalName)
    {
        var result = _itemsDatabase.GetCollection<Item>("items")
            .FindOne(x => x.InternalName == internalName);

        return result != null ? GetById(result.Id) : null;
    }

    public IEnumerable<Item> GetAll()
    {
        var items = _itemsDatabase.GetCollection<Item>("items").FindAll().ToArray();

        foreach (var item in items)
        {
            var fileInfo = _itemsDatabase.FileStorage.OpenRead(item.Id.ToString());
            item.Image = new byte[fileInfo.Length];
            fileInfo.Read(item.Image, 0, (int) fileInfo.Length);
        }

        return items;
    }
}