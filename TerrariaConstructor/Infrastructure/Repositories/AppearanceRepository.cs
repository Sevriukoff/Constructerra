using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using LiteDB;
using TerrariaConstructor.Infrastructure.Interfaces;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.Infrastructure.Repositories;

public class AppearanceRepository : IAppearanceRepository
{
    private readonly ILiteDatabase _itemsDatabase;
    
    public AppearanceRepository(ILiteDatabase itemsDatabase)
    {
        _itemsDatabase = itemsDatabase;
    }
    
    public Appearance GetHairById(int id)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerable<Appearance> GetAllHairs()
    {
        var filesInfo =_itemsDatabase.FileStorage
            .Find(x => x.Filename.Contains("Hairstyle"));

        var liteFileInfos = filesInfo as LiteFileInfo<string>[] ?? filesInfo.ToArray();
        var result = new Appearance[liteFileInfos.Length];
        int index = 0;

        for (var i = 0; i < liteFileInfos.Length; i++)
        {
            var info = liteFileInfos[i];
            var hair = new Appearance {Id = i};

            var stream = info.OpenRead();
            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, (int) stream.Length);

            hair.Image = bytes;
            result[index] = hair;

            index++;
        }

        return result;
    }

    public Appearance GetSkinById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Appearance> GetAllSkins()
    {
        var filesInfo =_itemsDatabase.FileStorage
            .Find(x => x.Filename.Contains("SkinVariant"));

        var liteFileInfos = filesInfo as LiteFileInfo<string>[] ?? filesInfo.ToArray();
        var result = new Appearance[liteFileInfos.Length];
        int index = 0;

        for (var i = 0; i < liteFileInfos.Length; i++)
        {
            var info = liteFileInfos[i];
            var hair = new Appearance {Id = i};

            var stream = info.OpenRead();
            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, (int) stream.Length);

            hair.Image = bytes;
            result[index] = hair;

            index++;
        }

        return result;
    }

    public Appearance GetItemImageById(int itemId)
    {
        var fileInfo = _itemsDatabase.FileStorage.OpenRead(itemId.ToString());
        var image = new byte[fileInfo.Length];
        fileInfo.Read(image, 0, (int) fileInfo.Length);

        return new Appearance
        {
            Id = itemId,
            Image = image,
            IsSelected = false
        };
    }
}