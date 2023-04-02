using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Autofac;
using ReactiveUI;
using TerrariaConstructor.Common.Enums;
using TerrariaConstructor.Common.Events;
using TerrariaConstructor.Infrastructure;
using TerrariaConstructor.Models;
using Wpf.Ui.Common;

namespace TerrariaConstructor.ViewModels;

public class InventoriesViewModel : ReactiveObject
{
    public InventoriesModel Model { get; set; }

    public ICollection<InventoryNavigationCard> NavigationCards
    {
        get => _navigationCards;
        set => this.RaiseAndSetIfChanged(ref _navigationCards, value);
    }

    private Appearance _bronzeCoin;
    private Appearance _silverCoin;
    private Appearance _goldCoin;
    private Appearance _platinumCoin;
    private ICollection<InventoryNavigationCard> _navigationCards;
    private InventoryNavigationCard _mainInventory;
    private InventoryNavigationCard _bank1;
    private InventoryNavigationCard _bank2;
    private InventoryNavigationCard _bank3;
    private InventoryNavigationCard _bank4;

    public InventoriesViewModel(InventoriesModel model)
    {
        Model = model;

        using (var scope = App.Container.BeginLifetimeScope())
        {
            var unitOfWork = scope.Resolve<UnitOfWork>();

            _bronzeCoin = unitOfWork.AppearanceRepository.GetItemImageById(71);
            _silverCoin = unitOfWork.AppearanceRepository.GetItemImageById(72);
            _goldCoin = unitOfWork.AppearanceRepository.GetItemImageById(73);
            _platinumCoin = unitOfWork.AppearanceRepository.GetItemImageById(74);
        }

        _mainInventory = new InventoryNavigationCard
        {
            Name = "Основной инветарь",
            Icon = SymbolRegular.Navigation24,
            Description = "Основной инвентарь игрока, где храниться всякая шелуха.",
            Link = "MainInventory",
        };
        
        _bank1 = new InventoryNavigationCard
        {
            Name = "Свиная копилка",
            Icon = SymbolRegular.Navigation24,
            Description = "Основной инвентарь игрока, где храниться всякая шелуха.",
            Link = "PiggyInventory",
        };
        
        _bank2 = new InventoryNavigationCard
        {
            Name = "Сейф",
            Icon = SymbolRegular.Navigation24,
            Description = "Основной инвентарь игрока, где храниться всякая шелуха.",
            Link = "SafeInventory",
        };
        
        _bank3 = new InventoryNavigationCard
        {
            Name = "Кузница",
            Icon = SymbolRegular.Navigation24,
            Description = "Основной инвентарь игрока, где храниться всякая шелуха.",
            Link = "ForgeInventory",
        };
        
        _bank4 = new InventoryNavigationCard
        {
            Name = "Бездонный мешок",
            Icon = SymbolRegular.Navigation24,
            Description = "Основной инвентарь игрока, где храниться всякая шелуха.",
            Link = "VoidInventory",
        };

        var mainInventorySum = (int) Model.Inventory.Where(x => x != null).Sum(x => x.CostBySell * x.Stack);
        _mainInventory.Coins = GetCoinsList(mainInventorySum);
        
        var Bank1Sum = (int) Model.Bank1.Where(x => x != null).Sum(x => x.CostBySell * x.Stack);
        _bank1.Coins = GetCoinsList(Bank1Sum);
        
        var Bank2Sum = (int) Model.Bank2.Where(x => x != null).Sum(x => x.CostBySell * x.Stack);
        _bank2.Coins = GetCoinsList(Bank2Sum);
        
        var Bank3Sum = (int) Model.Bank3.Where(x => x != null).Sum(x => x.CostBySell * x.Stack);
        _bank3.Coins = GetCoinsList(Bank3Sum);
        
        var Bank4Sum = (int) Model.Bank4.Where(x => x != null).Sum(x => x.CostBySell * x.Stack);
        _bank4.Coins = GetCoinsList(Bank4Sum);

        NavigationCards = new ObservableCollection<InventoryNavigationCard>();
        NavigationCards.Add(_mainInventory);
        NavigationCards.Add(_bank1);
        NavigationCards.Add(_bank2);
        NavigationCards.Add(_bank3);
        NavigationCards.Add(_bank4);

        MessageBus.Current.Listen<PlayerUpdatedEvent>()
            .Subscribe(x => Update());
    }

    private List<Coin> GetCoinsList(int sum)
    {
        int bronzeOst = sum % 100;
        int silver = sum / 100;
        
        int silverOst = silver % 100;
        int gold = silver / 100;
        
        int goldOst = gold % 100;
        int platinum = gold / 100;
        
        return new List<Coin>
        {
            new Coin
            {
                Image = _bronzeCoin.Image,
                Type = CoinType.Bronze,
                Amount = bronzeOst,
            },
            new Coin
            {
                Image = _silverCoin.Image,
                Type = CoinType.Silver,
                Amount = silverOst,
            },
            new Coin
            {
                Image = _goldCoin.Image,
                Type = CoinType.Gold,
                Amount = goldOst,
            },
            new Coin
            {
                Image = _platinumCoin.Image,
                Type = CoinType.Platinum,
                Amount = platinum,
            },
        };
        
    }
    
    public void Update()
    {
        var mainInventorySum = (int) Model.Inventory.Where(x => x != null).Sum(x => x.CostBySell * x.Stack);
        _mainInventory.Coins = GetCoinsList(mainInventorySum);
        
        var Bank1Sum = (int) Model.Bank1.Where(x => x != null).Sum(x => x.CostBySell * x.Stack);
        _bank1.Coins = GetCoinsList(Bank1Sum);
        
        var Bank2Sum = (int) Model.Bank2.Where(x => x != null).Sum(x => x.CostBySell * x.Stack);
        _bank2.Coins = GetCoinsList(Bank2Sum);
        
        var Bank3Sum = (int) Model.Bank3.Where(x => x != null).Sum(x => x.CostBySell * x.Stack);
        _bank3.Coins = GetCoinsList(Bank3Sum);
        
        var Bank4Sum = (int) Model.Bank4.Where(x => x != null).Sum(x => x.CostBySell * x.Stack);
        _bank4.Coins = GetCoinsList(Bank4Sum);
    }
}