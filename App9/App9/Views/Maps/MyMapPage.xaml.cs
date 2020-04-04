using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace App9.Views.Maps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyMapPage : ContentPage

    {
        
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        
        protected async override void OnAppearing() {
            base.OnAppearing();
            map.MoveToRegion(MapSpan.FromCenterAndRadius(
                new Position(55.7522200, 37.6155600), Distance.FromMiles(3)));
            try
            {
             var   sentPrompt = await firebaseHelper.GetAllLatLnd();
                if (sentPrompt.Count > 0)
                {
                    for (int i = 0; i < sentPrompt.Count; i++)
                    {
                        var pin = new Pin
                        {
                            Type = PinType.Place,
                            Position = new Position(sentPrompt[i].Latitude, sentPrompt[i].Longitude),
                            Label = sentPrompt[i].Title,        
                        };
                        map.Pins.Add(pin);
                    }
                }
            }
            catch { }

            
            
    
        }
        public MyMapPage()
        {
            InitializeComponent();
            //sentPrompt = await firebaseHelper.GetAllLatLnd();
           

        }
        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}