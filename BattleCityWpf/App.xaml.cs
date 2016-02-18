using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BattleCityWpf.Model;

namespace BattleCityWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var field = new GameField();
            var game = new Game(field);
            game.Start();
            var window = new MainWindow() { DataContext = field };
            window.ShowDialog();
            game.Stop();
        }
    }
}
