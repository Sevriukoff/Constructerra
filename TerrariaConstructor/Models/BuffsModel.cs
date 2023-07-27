using System.Collections.Generic;
using Autofac;
using TerrariaConstructor.Infrastructure;

namespace TerrariaConstructor.Models;

public class BuffsModel
{
    public Buff[] Buffs { get; set; } = new Buff[44];

    public IEnumerable<Buff> GetAllBuffs()
    {
        using (var scope = App.Container.BeginLifetimeScope())
        {
            var unitOfWork = scope.Resolve<UnitOfWork>();

            return unitOfWork.BuffsRepository.GetAll();
        }
    }
}