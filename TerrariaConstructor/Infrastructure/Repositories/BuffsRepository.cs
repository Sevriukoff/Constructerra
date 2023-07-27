using System.Collections.Generic;
using System.Linq;
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

    public Buff GetById(int id, bool loadImage = true)
    {
        var result = _itemsDatabase.GetCollection<Buff>("Buffs").FindOne(x => x.Id == id);

        var itemSource = result.Sources?.Select(x =>
            _itemsDatabase.GetCollection<Item>("items")
                .FindOne(y => y.InternalName == x.Replace(" ", "")))
                .Where(x => x != null)
                .ToArray();

        if (itemSource != null)
        {
            foreach (var item in itemSource)
            {
                var fileInfo = _itemsDatabase.FileStorage.OpenRead(item.Id.ToString());
                item.Image = new byte[fileInfo.Length];
                fileInfo.Read(item.Image, 0, (int) fileInfo.Length);
            }

            result.ItemSources = itemSource;
        }

        if (result != null && loadImage)
        {
            var fileInfo = _itemsDatabase.FileStorage.OpenRead(result.ImageId.ToString());
            result.Image = new byte[fileInfo.Length];
            fileInfo.Read(result.Image, 0, (int) fileInfo.Length);
        }
        
        return result;
    }

    public IEnumerable<Buff> GetAll()
    {
        var buffs = _itemsDatabase.GetCollection<Buff>("Buffs").FindAll().ToArray();
        var itemsCollection = _itemsDatabase.GetCollection<Item>("items");

        foreach (var buff in buffs)
        {
            var fileInfo = _itemsDatabase.FileStorage.OpenRead(buff.ImageId.ToString());
            buff.Image = new byte[fileInfo.Length];
            fileInfo.Read(buff.Image, 0, (int) fileInfo.Length);
            
            var itemSource = buff.Sources?.Select(x =>
                    _itemsDatabase.GetCollection<Item>("items")
                        .FindOne(y => y.InternalName == x.Replace(" ", "")))
                        .Where(x => x != null)
                        .ToArray();

            if (itemSource != null)
            {
                foreach (var item in itemSource)
                {
                    var fileInfoItem = _itemsDatabase.FileStorage.OpenRead(item.Id.ToString());
                    item.Image = new byte[fileInfoItem.Length];
                    fileInfoItem.Read(item.Image, 0, (int) fileInfoItem.Length);
                }

                buff.ItemSources = itemSource;
            }
        }

        return buffs;
    }
}