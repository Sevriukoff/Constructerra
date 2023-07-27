using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using ReactiveUI;
using TerrariaConstructor.Common.Events;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.ViewModels.Base;

public abstract class BaseInventoryViewModel : ReactiveObject
{
    #region Fields

    protected readonly InventoriesModel _model;
    private readonly IList<Item> _editableInventory;
    private ObservableCollection<Item> _items;
    private Item _selectedEditItem;
    private Item _selectedAddItem;

    #endregion

    #region Properties

    public virtual ObservableCollection<Item> Items
    {
        get => _items;
        set => this.RaiseAndSetIfChanged(ref _items, value);
    }

    public Item SelectedEditItem
    {
        get => _selectedEditItem;
        set => this.RaiseAndSetIfChanged(ref _selectedEditItem, value);
    }

    public Item SelectedAddItem
    {
        get => _selectedAddItem;
        set => this.RaiseAndSetIfChanged(ref _selectedAddItem, value);
    }

    public ObservableCollection<Item> AllItems { get; set; }

    #endregion

    #region ctor's

    protected BaseInventoryViewModel(InventoriesModel model, IList<Item> editableInventory)
    {
        _model = model;
        _editableInventory = editableInventory;

        Items = new ObservableCollection<Item>(editableInventory);
        AllItems = new ObservableCollection<Item>(model.GetAllItems());
        
        MessageBus.Current.Listen<PlayerUpdatedEvent>()
            .Subscribe(x => Update());

        this.WhenAnyValue(x => x.SelectedEditItem)
            .Where(x => x is {Id: 0})
            .Subscribe(x =>
            {
                if (SelectedAddItem != null)
                {
                    var index = Items.IndexOf(x);
                    
                    var newItem = new Item
                    {
                        Id = SelectedAddItem.Id,
                        Name = SelectedAddItem.Name,
                        InternalName = SelectedAddItem.InternalName,
                        Image = SelectedAddItem.Image,
                        Description = SelectedAddItem.Description,
                        Tooltip = SelectedAddItem.Tooltip,
                        Rarity = SelectedAddItem.Rarity,
                        Categories = SelectedAddItem.Categories,
                        Damage = SelectedAddItem.Damage,
                        DamageType = SelectedAddItem.DamageType,
                        CriticalChance = SelectedAddItem.CriticalChance,
                        Knockback = SelectedAddItem.Knockback,
                        KnockbackType = SelectedAddItem.KnockbackType,
                        UseTime = SelectedAddItem.UseTime,
                        PickaxePower = SelectedAddItem.PickaxePower,
                        AxePower = SelectedAddItem.AxePower,
                        HammerPower = SelectedAddItem.HammerPower,
                        DefensePoint = SelectedAddItem.DefensePoint,
                        QuantityForResearch = SelectedAddItem.QuantityForResearch,
                        CostByBuy = SelectedAddItem.CostByBuy,
                        CostBySell = SelectedAddItem.CostBySell,
                        Prefix = SelectedAddItem.Prefix,
                        Modifier = SelectedAddItem.Modifier,
                        Stack = 1,
                        MaxStack = SelectedAddItem.MaxStack,
                        IsPlaceable = SelectedAddItem.IsPlaceable,
                        IsFavorite = SelectedAddItem.IsFavorite,
                        WikiUrl = SelectedAddItem.WikiUrl
                    };

                    Items[index] = newItem;
                    
                    if (Items.Count > editableInventory.Count)
                        editableInventory.Add(newItem);
                    else
                        editableInventory[index] = newItem;
                }
            });
    }

    #endregion

    #region Methods

    protected virtual void Update()
    {
        Items = new ObservableCollection<Item>(_editableInventory);
    }

    #endregion
}