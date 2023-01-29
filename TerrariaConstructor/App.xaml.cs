using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using Autofac.Core;
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