using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App9.Services;
using App9.Views;
using OAuthNativeFlow;

namespace App9
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new NavigationPage( new OAuthNativeFlowPage());
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
