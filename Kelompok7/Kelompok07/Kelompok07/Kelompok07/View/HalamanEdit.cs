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
    public class HalamanEdit : ContentPage
    {
        private ListView _listView;
        private Entry _id;
        private Entry _nama;
        private Entry _jurusan;
        private Button _simpan;

        DataMahasiswa _datamahasiswa = new DataMahasiswa();

        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db4");

        public HalamanEdit()
        {
            this.Title = "Edit Data";

            var db = new SQLiteConnection(_dbPath);

            StackLayout stackLayout = new StackLayout();

            _listView = new ListView();
            _listView.ItemsSource = db.Table<DataMahasiswa>().OrderBy(x => x.Nama).ToList();
            _listView.ItemSelected += _listView_ItemSelected;
            stackLayout.Children.Add(_listView);

            _id = new Entry();
            _id.Placeholder = "ID";
            _id.IsVisible = false;
            stackLayout.Children.Add(_id);

            _nama = new Entry();
            _nama.Keyboard = Keyboard.Text;
            _nama.Placeholder = "Nama Mahasiswa";
            stackLayout.Children.Add(_nama);

            _jurusan = new Entry();
            _jurusan.Keyboard = Keyboard.Text;
            _jurusan.Placeholder = "Jurusan";
            stackLayout.Children.Add(_jurusan);

            _simpan = new Button();
            _simpan.Text = "Edit";
            _simpan.Clicked += _simpan_Clicked;
            stackLayout.Children.Add(_simpan);

            Content = stackLayout;
        }

        private async void _simpan_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            DataMahasiswa dataMahasiswa = new DataMahasiswa()
            {
                Id = Convert.ToInt32(_id.Text),
                Nama = _nama.Text,
                Jurusan = _jurusan.Text
            };
            db.Update(dataMahasiswa);
            await DisplayAlert(null, "Data " + dataMahasiswa.Nama + " Berhasil di Edit", "Ok");
            await Navigation.PopAsync();
        }

        private void _listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _datamahasiswa = (DataMahasiswa)e.SelectedItem;
            _id.Text = _datamahasiswa.Id.ToString();
            _nama.Text = _datamahasiswa.Nama;
            _jurusan.Text = _datamahasiswa.Jurusan;
        }
    }
}