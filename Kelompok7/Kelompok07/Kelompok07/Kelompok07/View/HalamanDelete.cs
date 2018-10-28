using Kelompok07.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Kelompok07.View
{
    public class HalamanDelete : ContentPage
    {
        private ListView _listView;
        private Button _simpan;

        DataMahasiswa _datamahasiswa = new DataMahasiswa();

        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db4");

        public HalamanDelete()
        {
            this.Title = "Edit Data";

            var db = new SQLiteConnection(_dbPath);

            StackLayout stackLayout = new StackLayout();

            _listView = new ListView();
            _listView.ItemsSource = db.Table<DataMahasiswa>().OrderBy(x => x.Nama).ToList();
            _listView.ItemSelected += _listView_ItemSelected;
            stackLayout.Children.Add(_listView);

            _simpan = new Button();
            _simpan.Text = "Hapus";
            _simpan.Clicked += _simpan_Clicked;
            stackLayout.Children.Add(_simpan);

            Content = stackLayout;
        }

        private void _button_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _datamahasiswa = (DataMahasiswa)e.SelectedItem;

        }
        private async void _simpan_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            db.Table<DataMahasiswa>().Delete(x => x.Id == _datamahasiswa.Id);
            await DisplayAlert(null, "Data " + _datamahasiswa.Nama + " Berhasil di Hapus", "Ok");
            await Navigation.PopAsync();
        }
    }
}