using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;




namespace WordPad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public static string HandleDialogThenGetFileName(Microsoft.Win32.FileDialog fileDialog)
        {
            fileDialog.DefaultExt = ".txt";
            fileDialog.Filter = "Text Files(*txt)|*txt";

            return fileDialog.ShowDialog() == true ? fileDialog.FileName : string.Empty;
        }
        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        { 
            string fileName =HandleDialogThenGetFileName(new Microsoft.Win32.OpenFileDialog());

            if (fileName != string.Empty)
            {
                TxtBlckFilePath.Text = fileName;
                TxtBxData.Text = File.ReadAllText(fileName);
            }
            TxtBxData.Focus();
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TxtBlckFilePath.Text))
            {
                File.WriteAllText(TxtBlckFilePath.Text, TxtBxData.Text);
                System.Windows.Forms.MessageBox.Show("Saved successfully.");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Invalid file path.");
            }

            TxtBxData.Focus();
        }
        private void TxtBxData_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(ToggleButtonAutoSave.IsChecked != true))
            {
                if (!string.IsNullOrWhiteSpace(TxtBlckFilePath.Text))
                {
                    File.WriteAllText(TxtBlckFilePath.Text, TxtBxData.Text);
                }
            }
        }
        private void BtnSaveAs_Click(object sender, RoutedEventArgs e)
        {
            string fileName = HandleDialogThenGetFileName(new Microsoft.Win32.SaveFileDialog());
            if (fileName != string.Empty)
            {
                File.WriteAllText(fileName, TxtBxData.Text);

                System.Windows.Forms.MessageBox.Show("Saved successfully.");
            }
            TxtBxData.Focus();
        }
        private void BtnCut_Click(object sender, RoutedEventArgs e)
        {
            TxtBxData.Cut();
            TxtBxData.Focus();
        }
        private void BtnCopy_Click(object sender, RoutedEventArgs e)
        {
            TxtBxData.Copy();
            TxtBxData.Focus();
        }
        private void BtnPaste_Click(object sender, RoutedEventArgs e)
        {
            TxtBxData.Paste();
        }
        private void BtnSelectAll_Click(object sender, RoutedEventArgs e)
        {
            TxtBxData.SelectAll();
            TxtBxData.Focus();
        }
        private void BtnUndo_Click(object sender, RoutedEventArgs e)
        {
            if (TxtBxData.CanUndo)
            {
                TxtBxData.Undo();
            }
            TxtBxData.Focus();
        }
        private void BtnFont_Click(object sender, RoutedEventArgs e)
        {
            var font = new FontDialog();

            if (font.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                return;

            TxtBxData.FontFamily = new System.Windows.Media.FontFamily(font.Font.Name);
            TxtBxData.FontSize = font.Font.Size;
            TxtBxData.FontWeight = font.Font.Bold ? FontWeights.Bold : FontWeights.Regular;
            TxtBxData.FontStyle = font.Font.Italic ? FontStyles.Italic : FontStyles.Normal;

            TextDecorationCollection tdc = new TextDecorationCollection();
            if (font.Font.Underline) tdc.Add(TextDecorations.Underline);
            if (font.Font.Strikeout) tdc.Add(TextDecorations.Strikethrough);
            TxtBxData.TextDecorations = tdc;
        }

        private void BtnColor_Click(object sender, RoutedEventArgs e)
        {
            var color = new ColorDialog();

            if (color.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                return;

            TxtBxData.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb((byte)color.Color.A,
                                                                        (byte)color.Color.R,
                                                                        (byte)color.Color.G,
                                                                        (byte)color.Color.B));
        }
    }
}
