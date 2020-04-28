using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App9.Services;
using App9.Views;
using OAuthNativeFlow;
using System.IO;
using System.Collections.Generic;

namespace App9
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "pit.db";
        public static Views.DB.PropmtRepository database;        
        public static Views.DB.PropmtRepository Database
        {
            get
            {
                if (database == null)
                {
                    database = new Views.DB.PropmtRepository(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                }
                return database;
            }
        }
        public  App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            //await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
            //  MainPage = new OAuthNativeFlowPage();
            
            //MainPage = new OAuthNativeFlowPage();
            
           MainPage = new NavigationPage( new OAuthNativeFlowPage());
            //MainPage = new NavigationPage( new OAuthNativeFlowPage());
            

        }

        protected override void OnStart()
        {       
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
