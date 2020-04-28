using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using App9.Models;
using App9.Views;
using App9.ViewModels;
using System.Collections.ObjectModel;
using System.IO;
using Xamarin.Auth;
using OAuthNativeFlow;
using System.Resources;

using System;
using System.Globalization;
using System.Resources;
using System.Threading;
using App9.Resx;

namespace App9.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {

        FirebaseHelper firebaseHelper = new FirebaseHelper();
        ItemsViewModel viewModel;
       // ListView itemsFirebase;
        //List<Prompt> promptsFB;
      
        public ItemsPage()
        {
            InitializeComponent();
            
            BindingContext = viewModel = new ItemsViewModel();                                 
        }
        
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {            
            var item = args.SelectedItem as Prompt;
            if (item == null)
                return;
            if (item.Agreed == "draft") 
            {
                Console.WriteLine("{}draft");
                await Navigation.PushAsync(new ItemDraftDetailPage1( new ItemDetailViewModel(item))); }
            else if (item.Agreed == "sent")
            {
                Console.WriteLine("send");
                await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item))); }
            

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }
        async void Maps_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new  Maps.MyMapPage());
            //await Navigation.PushModalAsync(new  NavigationPage(new  Maps.MyMapPage()));
            
        }
        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync( new NewItemPage());
           // await Navigation.PushModalAsync( new NavigationPage(new NewItemPage()));
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (AuthenticationState.Authenticator != null)
            {
                
                
                string t_d = Resource.text_draft;
                
                string t_s = Resource.text_sented;
                //string t_d= rm.GetString("text_draft");

                //string t_s= rm.GetString("text_sented");

               


                var draftPrompt = App.Database.GetItems();
                
                
                var sentPrompt = await firebaseHelper.GetAllPersons();               
                var allPrompts = draftPrompt.Concat(sentPrompt);
                foreach (var p in allPrompts)
                {
                    if (p.Agreed == "draft")

                        p.TitleSatus = t_d;// (String)Resources["text_draft"];
                    else if (p.Agreed == "sent")
                    {
                        p.TitleSatus = t_s;//(String)Resources["text_sented"];

                    }
                }
             
                ItemsListView.ItemsSource = allPrompts;
                
                if (viewModel.Items.Count == 0)
                    viewModel.LoadItemsCommand.Execute(null);
            }
            else {                
                await Navigation.PushAsync(new OAuthNativeFlowPage() ); }
        }
        }
    }
