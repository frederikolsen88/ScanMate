using ScanMate.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ScanMate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BackgroundWorker bwScanMate = new BackgroundWorker();

        public MainWindow()
        {
            InitializeComponent();

            bwScanMate.DoWork += bwScanMate_DoWork;
            bwScanMate.RunWorkerCompleted += bwScanMate_RunWorkerCompleted;
        }

        private void imgPDF_Drop(object sender, DragEventArgs e)
        {
            try
            { 
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                    if (files.Length > 1)
                        throw new Exception("Please drop only one PDF file at a time!");

                    object[] bwArgs = { files, chkBoxDelSrc.IsChecked.Value, btnSplitPDF.IsChecked.Value, btnReversePageOrder.IsChecked.Value };

                    Mouse.OverrideCursor = Cursors.Wait;
                    bwScanMate.RunWorkerAsync(bwArgs);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void bwScanMate_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] args = (object[])e.Argument;
            string[] files = (string[])args[0];
            bool delSrc = (bool)args[1];
            bool splitPDF = (bool)args[2];
            bool reversePageOrder = (bool)args[3];

            ScanMateController.GetInstance.HandleFileDrop(files, delSrc, splitPDF, reversePageOrder);   
        }

        private void bwScanMate_RunWorkerCompleted(object sender,
                                               RunWorkerCompletedEventArgs e)
        {
            Mouse.OverrideCursor = null;
        }
    }
}
