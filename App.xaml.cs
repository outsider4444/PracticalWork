﻿using PracticalWork.Data;
using System.Configuration;
using System.Data;
using System.Windows;

namespace PracticalWork
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            using (var dbContext = new AppDbContext())
            {
                dbContext.Database.EnsureCreated();
            }
        }
    }

}
