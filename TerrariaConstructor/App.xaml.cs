using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Autofac;
using Autofac.Core;
using LiteDB;
using TerrariaConstructor.Infrastructure;
using TerrariaConstructor.Infrastructure.Interfaces;
using TerrariaConstructor.Infrastructure.Mappers;
using TerrariaConstructor.Infrastructure.Repositories;
using TerrariaConstructor.Models;
using TerrariaConstructor.ViewModels;
using TerrariaConstructor.Views;

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

            Wpf.Ui.Appearance.Accent.Apply(Wpf.Ui.Appearance.Accent.SystemAccent, 
                Wpf.Ui.Appearance.Accent.PrimaryAccent, Color.FromRgb(130,99,255), 
                Wpf.Ui.Appearance.Accent.TertiaryAccent);
            
            Container = AppStartup();
        }

        private static IContainer AppStartup()
        {
            //register dependencies
            var builder = new ContainerBuilder();

            string itemsDatabaseName = ConfigurationManager.ConnectionStrings["ItemsDatabase"].ConnectionString;
            string playersDatabaseName = ConfigurationManager.ConnectionStrings["PlayersDatabase"].ConnectionString;
            /*
            builder.RegisterInstance(new LiteDatabase(itemsDatabaseName , new ItemMapper()))
                .Named<ILiteDatabase>(itemsDatabaseName).SingleInstance();
            builder.RegisterInstance(new LiteDatabase(playersDatabaseName))
                .Named<ILiteDatabase>(playersDatabaseName).SingleInstance();
            */
            builder.RegisterType<LiteDatabase>().Named<ILiteDatabase>(itemsDatabaseName)
                .InstancePerLifetimeScope()
                .WithParameter((pi, ctx) => pi.ParameterType == typeof(string),
                (pi, ctx) => itemsDatabaseName)
                .WithParameter((pi, ctx) => pi.ParameterType == typeof(BsonMapper),
                    (pi, ctx) => new ItemMapper());
            
            builder.RegisterType<LiteDatabase>().Named<ILiteDatabase>(playersDatabaseName)
                .InstancePerLifetimeScope()
                .WithParameter((pi, ctx) => pi.ParameterType == typeof(string),
                    (pi, ctx) => playersDatabaseName);

            Func<ParameterInfo,IComponentContext,bool> parameterSelector = (pi, ctx) => pi.ParameterType == typeof(ILiteDatabase);
            Func<ParameterInfo,IComponentContext,object?> valueProvider = (pi, ctx) => ctx.ResolveNamed<ILiteDatabase>(itemsDatabaseName);
            
            builder.RegisterType<ItemsRepository>().As<IItemsRepository>()
                .InstancePerLifetimeScope()
                .WithParameter(parameterSelector, valueProvider);
            
            builder.RegisterType<BuffsRepository>().As<IBuffsRepository>()
                .InstancePerLifetimeScope()
                .WithParameter(parameterSelector, valueProvider);

            builder.RegisterType<AppearanceRepository>().As<IAppearanceRepository>()
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
            
            builder.RegisterType<CharacteristicsViewModel>().SingleInstance();
            builder.RegisterType<EquipsViewModel>().AsSelf();
            builder.RegisterType<InventoriesViewModel>().AsSelf();
            
            builder.RegisterType<WelcomeView>().AsSelf();
            builder.RegisterType<CharacteristicsView>().SingleInstance();
            builder.RegisterType<EquipsView>().AsSelf();
            builder.RegisterType<ToolsView>().AsSelf();
            builder.RegisterType<InventoriesView>().AsSelf();
            builder.RegisterType<BuffsView>().AsSelf();
            builder.RegisterType<ResearchView>().AsSelf();

           return builder.Build();
        }
    }
}