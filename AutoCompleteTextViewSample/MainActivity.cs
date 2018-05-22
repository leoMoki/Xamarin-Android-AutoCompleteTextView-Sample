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
        AutoCompleteTextView autoComplete;
        List<Unidade> dataSource;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            dataSource = GetAllUnidades(true, "teste1");

            autoComplete = FindViewById<AutoCompleteTextView>(Resource.Id.autoCompleteTextView1);
                                    
            try
            {
                if (dataSource != null && dataSource.Count > 0)
                {
                    List<string> nomes = dataSource.Select(u => u.Nome).ToList();
                    //var adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, nomes);
                    var adapter = new AutoCompleteTextViewUnidadesAdapter(this, dataSource);
                    
                    autoComplete.Adapter = adapter;

                    autoComplete.ItemClick += AutoComplete_ItemClick;
                }
                
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }

        private void AutoComplete_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            AutoCompleteTextView autoText = (AutoCompleteTextView)sender;
            var idUnidade = Convert.ToInt32(autoText.Text);


            Unidade unidade = dataSource.FirstOrDefault(u => u.id == idUnidade);

            autoComplete = autoText;
            autoComplete.Text = unidade.Nome;
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

