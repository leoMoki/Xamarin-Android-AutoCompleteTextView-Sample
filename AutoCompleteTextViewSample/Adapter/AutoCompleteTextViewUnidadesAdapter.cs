using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AutoCompleteTextViewSample.Models;
using Java.Lang;

namespace AutoCompleteTextViewSample.Adapter
{
    public class AutoCompleteTextViewUnidadesAdapter : BaseAdapter, IFilterable
    {
        Context context;

        private List<Unidade> _originalData;
        private List<Unidade> _items;

        public AutoCompleteTextViewUnidadesAdapter(Context context, List<Unidade> unidades)
        {
            this.context = context;
            this._items = unidades;

            Filter = new UnidadesFilter(this);
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            AutoCompleteTextViewUnidadesAdapterViewHolder holder = null;

            if (view != null)
                holder = view.Tag as AutoCompleteTextViewUnidadesAdapterViewHolder;

            if (holder == null)
            {
                holder = new AutoCompleteTextViewUnidadesAdapterViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();

                //view
                view = inflater.Inflate(Resource.Layout.list_view_unidades_template, parent, false);

                //holder
                holder.NomeUnidade = view.FindViewById<TextView>(Resource.Id.list_view_unidades_txtUnidade);


                view.Tag = holder;
            }

            holder.NomeUnidade.Text = _items[position].Nome;

            return view;

        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get { return _items.Count; }
        }

        
        public override void NotifyDataSetChanged()
        {
            base.NotifyDataSetChanged();
        }

        public Filter Filter { get; private set; }


        private class UnidadesFilter : Filter
        {
            private readonly AutoCompleteTextViewUnidadesAdapter _adapter;

            public UnidadesFilter(AutoCompleteTextViewUnidadesAdapter adapter)
            {
                _adapter = adapter;
            }


            protected override FilterResults PerformFiltering(ICharSequence constraint)
            {
                var returnObj = new FilterResults();
                var results = new List<Unidade>();
                if (_adapter._originalData == null)
                    _adapter._originalData = _adapter._items;

                if (constraint == null) return returnObj;

                if (_adapter._originalData != null && _adapter._originalData.Any())
                {
                    results.AddRange(
                        _adapter._originalData.Where(
                            u => u.Nome.ToLower().Contains(constraint.ToString().ToLower())));
                }

                //Cast JavaObject
                returnObj.Values = FromArray(results.Select(r => r.ToJavaObject()).ToArray());
                returnObj.Count = results.Count;

                constraint.Dispose();

                return returnObj;


            }

            protected override void PublishResults(ICharSequence constraint, FilterResults results)
            {
                using (var values = results.Values)
                {
                    if (values != null)
                    {
                        _adapter._items = values.ToArray<Java.Lang.Object>()
                            .Select(r => r.ToNetObject<Unidade>())
                            .ToList();
                                                
                        _adapter.NotifyDataSetChanged();

                        
                        constraint.Dispose();
                        results.Dispose();
                    }

                }




            }
        }
    }

    class AutoCompleteTextViewUnidadesAdapterViewHolder : Java.Lang.Object
    {
        public TextView NomeUnidade { get; set; }
    }


}