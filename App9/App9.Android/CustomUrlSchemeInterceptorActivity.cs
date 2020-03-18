using System;

using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;


using OAuthNativeFlow;

namespace App9.Droid
{
    //public class CustomUrlSchemeInterceptorActivity : ContentPage
    //{
		[Activity(Label = "CustomUrlSchemeInterceptorActivity", NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
		[IntentFilter(
			new[] { Intent.ActionView },
			Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
			DataSchemes = new[] { "com.googleusercontent.apps.1012719202452-g8cgcjg3p0maua9skuh84t982q8i8sts" },
			DataPath = "/oauth2redirect")]
		public class CustomUrlSchemeInterceptorActivity : Activity
		{
			protected override void OnCreate(Bundle savedInstanceState)
			{
				base.OnCreate(savedInstanceState);

				// Convert Android.Net.Url to Uri
				var uri = new Uri(Intent.Data.ToString());

				// Load redirectUrl page
				AuthenticationState.Authenticator.OnPageLoading(uri);
				var intent = new Intent(this, typeof(MainActivity));
				intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
				StartActivity(intent);

				this.Finish();

				return;
			}
		}
	}
