using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using Autofac.Core;
using LiteDB;
using TerrariaConstructor.Infrastructure;
using TerrariaConstructor.Infrastructure.Interfaces;
using TerrariaConstructor.Infrastructure.Mappers;
using TerrariaConstructor.Infrastructure.Repositories;
using TerrariaConstructor.Models;
using TerrariaConstructor.ViewModels;

namespace TerrariaConstructor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IContainer Container { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Container = AppStartup();
        }

        private static IContainer AppStartup()
        {
            //register dependencies
            var builder = new ContainerBuilder();

            string itemsDatabaseName = ConfigurationManager.ConnectionStrings["ItemsDatabase"].ConnectionString;
            string playersDatabaseName = ConfigurationManager.ConnectionStrings["PlayersDatabase"].ConnectionString;

            builder.RegisterInstance(new LiteDatabase(itemsDatabaseName , new ItemMapper()))
                .Named<ILiteDatabase>(itemsDatabaseName).SingleInstance();
            builder.RegisterInstance(new LiteDatabase(playersDatabaseName))
                .Named<ILiteDatabase>(playersDatabaseName).SingleInstance();

            Func<ParameterInfo,IComponentContext,bool> parameterSelector = (pi, ctx) => pi.ParameterType == typeof(ILiteDatabase);
            Func<ParameterInfo,IComponentContext,object?> valueProvider = (pi, ctx) => ctx.ResolveNamed<ILiteDatabase>(itemsDatabaseName);
            
            builder.RegisterType<ItemsRepository>().As<IItemsRepository>()
                .InstancePerLifetimeScope()
                .WithParameter(parameterSelector, valueProvider);
            
            builder.RegisterType<BuffsRepository>().As<IBuffsRepository>()
                .InstancePerLifetimeScope()
                .WithParameter(parameterSelector, valueProvider);
            
            builder.RegisterType<PlayerRepository>().As<IPlayerRepository>()
                .InstancePerLifetimeScope()
                .WithParameter(parameterSelector,
                    (pi, ctx) => ctx.ResolveNamed<ILiteDatabase>(playersDatabaseName));

            builder.RegisterType<UnitOfWork>().AsSelf()
                .InstancePerLifetimeScope()
                .WithParameter(parameterSelector, valueProvider)
                .WithParameter(parameterSelector, 
                    (pi, ctx) => ctx.ResolveNamed<ILiteDatabase>(playersDatabaseName));

            builder.RegisterType<PlayerModel>().SingleInstance();

            builder.RegisterType<CharacteristicsModel>().SingleInstance();
            builder.RegisterType<EquipsModel>().SingleInstance();
            builder.RegisterType<ToolsModel>().SingleInstance();
            builder.RegisterType<InventoriesModel>().SingleInstance();
            builder.RegisterType<BuffsModel>().SingleInstance();
            builder.RegisterType<ResearchModel>().SingleInstance();

            builder.RegisterType<MainViewModel>().AsSelf();
            
            builder.RegisterType<CharacteristicViewModel>().AsSelf();

           return builder.Build();
        }
    }
}