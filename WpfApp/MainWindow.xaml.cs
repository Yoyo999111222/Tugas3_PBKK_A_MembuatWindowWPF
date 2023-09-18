using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonAddName_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text) && !lstNames.Items.Contains(txtName.Text))
            {
                lstNames.Items.Add(txtName.Text);
                txtName.Clear();
            }
        }

        private void ButtonEditName_Click(object sender, RoutedEventArgs e)
        {
            if (lstNames.SelectedItem != null)
            {
                int selectedIndex = lstNames.SelectedIndex;
                lstNames.Items[selectedIndex] = txtName.Text;
                txtName.Clear();
            }
        }

        private void ButtonDeleteName_Click(object sender, RoutedEventArgs e)
        {
            if (lstNames.SelectedItem != null)
            {
                lstNames.Items.Remove(lstNames.SelectedItem);
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == true)
            {
                string fileName = saveFileDialog.FileName;
                List<string> names = new List<string>();

                foreach (var item in lstNames.Items)
                {
                    names.Add(item.ToString());
                }

                try
                {
                    File.WriteAllLines(fileName, names);
                    MessageBox.Show("Data saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving the file: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;
                List<string> names = new List<string>();

                try
                {
                    string[] lines = File.ReadAllLines(fileName);
                    names.AddRange(lines);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error reading the file: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                lstNames.Items.Clear();
                foreach (var name in names)
                {
                    lstNames.Items.Add(name);
                }

                MessageBox.Show("Data loaded successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            lstNames.Items.Clear();
        }

        private void ButtonSearchName_Click(object sender, RoutedEventArgs e)
        {
            string searchQuery = txtName.Text.Trim();

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                foreach (var item in lstNames.Items)
                {
                    if (item.ToString().Contains(searchQuery))
                    {
                        lstNames.SelectedItem = item;
                        lstNames.ScrollIntoView(item);
                        return; // Keluar dari loop setelah menemukan hasil pertama
                    }
                }

                // Jika tidak ada hasil yang ditemukan, beri tahu pengguna
                MessageBox.Show("Name not found.", "Search Result", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                // Jika input pencarian kosong, beri tahu pengguna
                MessageBox.Show("Please enter a search query.", "Search Result", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
