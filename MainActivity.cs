using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Analytics;
using System;

namespace PushNotification
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            if (!AppCenter.Configured)
            {
                Microsoft.AppCenter.Push.Push.PushNotificationReceived += OnPushNotificationReceived;
            }

            AppCenter.Start("04295fc6-0ee0-44e2-9bd4-78e86be78639", typeof(Push));

            AppCenter.GetInstallIdAsync().ContinueWith(installId =>
            {
                System.Diagnostics.Debug.WriteLine("**********************************************");
                System.Diagnostics.Debug.WriteLine("App center installId=" + installId.Result);
                System.Diagnostics.Debug.WriteLine("**********************************************");
            });
        }
        private void OnPushNotificationReceived(object sender, PushNotificationReceivedEventArgs e)
        {
            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
            Android.App.AlertDialog alert = builder.Create();
            alert.SetTitle(e.Title);
            alert.SetMessage(e.Message);
            alert.SetIcon(Resource.Drawable.mr_dialog_material_background_dark);
            alert.Show();
       
        }
    }
}