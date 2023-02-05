using System;
using LiteDB;
using TerrariaConstructor.Infrastructure.Interfaces;
using TerrariaConstructor.Infrastructure.Repositories;

namespace TerrariaConstructor.Infrastructure;

public class UnitOfWork : IDisposable
{
    public IItemsRepository ItemsRepository { get; set; }
    public IBuffsRepository BuffsRepository { get; set; }
    public IAppearanceRepository AppearanceRepository { get; set; }
    public IPlayerRepository PlayerRepository { get; set; }
    
    private readonly ILiteDatabase _itemsDatabase;
    private readonly ILiteDatabase _playersDatabase;

    public UnitOfWork(ILiteDatabase itemsDatabase,
        ILiteDatabase playersDatabase,
        IItemsRepository itemsRepository,
        IBuffsRepository buffsRepository,
        IPlayerRepository playerRepository,
        IAppearanceRepository appearanceRepository)
    {
        _itemsDatabase = itemsDatabase;
        _playersDatabase = playersDatabase;
        ItemsRepository = itemsRepository;
        BuffsRepository = buffsRepository;
        PlayerRepository = playerRepository;
        AppearanceRepository = appearanceRepository;
    }

    public void Rollback()
    {
        _itemsDatabase.Rollback();
        _playersDatabase.Rollback();
    }
    
    public void Commit()
    {
        _itemsDatabase.Commit();
        _playersDatabase.Commit();
    }

    public void Dispose()
    {
        _itemsDatabase.Dispose();
        _playersDatabase.Dispose();
    }
}