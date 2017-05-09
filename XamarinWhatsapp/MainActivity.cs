using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Content.PM;

namespace XamarinWhatsapp
{
    [Activity(Label = "XamarinWhatsapp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Button btnEnviar;
        EditText edtMensaje;
        string whatsapp = "com.whatsapp";
        
        bool VerificarApp(String uri)
        {
            try
            {
                ApplicationContext.PackageManager.GetPackageInfo(uri, PackageInfoFlags.Activities);
                return true;
            }
            catch (PackageManager.NameNotFoundException e)
            {
                return false;
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            btnEnviar = FindViewById<Button>(Resource.Id.btnEnviar);
            edtMensaje = FindViewById<EditText>(Resource.Id.edtMensaje);
            btnEnviar.Click += BtnEnviar_Click;
        }

        private void BtnEnviar_Click(object sender, System.EventArgs e)
        {
            if (VerificarApp(whatsapp))
            {
                Intent intent = new Intent();
                intent.SetAction(Intent.ActionSend);
                intent.PutExtra(Intent.ExtraText, edtMensaje.Text);
                intent.SetType("text/plain");
                intent.SetPackage(whatsapp);
                StartActivity(intent);
            }
            else
            {
                Toast.MakeText(this, "WhatsApp no está instalado. No se pudo enviar el mensaje", ToastLength.Long).Show();
            }
        }
    }
}