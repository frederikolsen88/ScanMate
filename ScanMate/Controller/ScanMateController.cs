using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.itextpdf.text.pdf;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Diagnostics;

namespace ScanMate.Controller
{
    class ScanMateController
    {
        private static ScanMateController _instance = new ScanMateController();

        private ScanMateController() { }

        public static ScanMateController GetInstance
        {
            get 
            { 
                return _instance; 
            }
        }

        public void HandleFileDrop(string[] fileList, bool deleteSource, bool split, bool reversePageOrder)
        {
            FileInfo pdfFile = new FileInfo(fileList[0]);

            if (pdfFile.Extension != ".pdf")
                return;
                

            if (split)
                SplitPDF(pdfFile);

            else if (reversePageOrder)
                ReversePDFPageOrder(pdfFile);   

            if (deleteSource)
                File.Delete(pdfFile.FullName);
        }

        private void SplitPDF(FileInfo pdfFile)
        {
            PdfReader pdfReader = new PdfReader(pdfFile.FullName);

            try
            {
                int pageCount = pdfReader.NumberOfPages;

                for (int i = 1; i <= pageCount; i++)
                {
                    string output = pdfFile.FullName.Substring(0, pdfFile.FullName.IndexOf(".pdf")) + "_page" + i + ".pdf";

                    Document document = new Document(pdfReader.GetPageSizeWithRotation(1));
                    PdfCopy pdfWriter = new PdfCopy(document, File.Create(output));

                    document.Open();

                    PdfImportedPage page = pdfWriter.GetImportedPage(pdfReader, i);
                    pdfWriter.AddPage(page);

                    document.Close();
                }
            }

            catch (Exception)
            {
                return;
            }

            finally
            {
                pdfReader.Close();
            }
        }

        private void ReversePDFPageOrder(FileInfo pdfFile)
        {
            PdfReader pdfReader = new PdfReader(pdfFile.FullName);
            Document document = new Document(pdfReader.GetPageSizeWithRotation(1));
            string output = pdfFile.FullName.Substring(0, pdfFile.FullName.IndexOf(".pdf")) + "_reverse.pdf";

            PdfCopy pdfWriter = new PdfCopy(document, File.Create(output));

            try
            {
                int pageCount = pdfReader.NumberOfPages;
                document.Open();

                for (int i = pageCount; i >= 1; i--)
                {
                    PdfImportedPage page = pdfWriter.GetImportedPage(pdfReader, i);
                    pdfWriter.AddPage(page);       
                }

                document.Close();
            }

            catch (Exception)
            {
                return;
            }

            finally
            {
                pdfReader.Close();
                pdfWriter.Close();
            }
        }
    }
}
