using Android.App;
using Android.OS;
using Android.Support.V7.App;
using System.Threading.Tasks;
using System.Collections.Generic;
using Android.Widget;
using AutoCompleteTextViewSample.Adapter;
using System;
using System.Linq;
using AutoCompleteTextViewSample.Models;

namespace AutoCompleteTextViewSample
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            List<Unidade> dataSource = GetAllUnidades(true, "teste1");

            AutoCompleteTextView autoComplete = FindViewById<AutoCompleteTextView>(Resource.Id.autoCompleteTextView1);
                                    
            try
            {
                if (dataSource != null && dataSource.Count > 0)
                {
                    List<string> nomes = dataSource.Select(u => u.Nome).ToList();
                    //var adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, nomes);
                    var adapter = new AutoCompleteTextViewUnidadesAdapter(this, dataSource);
                    
                    autoComplete.Adapter = adapter;
                }
                
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }

        private List<Unidade> GetAllUnidades(bool isOnline, string prefixo)
        {
            List<Unidade> dataSource = new List<Unidade>
            {
                new Unidade { id = 1, Nome = "Barra" },
                new Unidade{ id = 2, Nome = "Leblon" },
                new Unidade { id = 3, Nome = "Tijuca" },
                new Unidade{ id = 4, Nome = "Maracanã" },
                new Unidade { id = 5, Nome = "São Cristovão" },
                new Unidade{ id = 6, Nome = "Ipanema" },
            };
            return dataSource;
        }
    }
}

