using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using Syncfusion.Maui.Core;

namespace ExportMAUIChartToPDF;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        PdfDocument document = new PdfDocument();
        PdfPage page = document.Pages.Add();
        PdfGraphics graphics = page.Graphics;

        float width = (float)chart.Width + 75;
        float height = (float)chart.Height + 125;

        //To reduce the width and height of the Windows and MAC platform
#if !IOS && !ANDROID
        width = (float)chart.Width / 2.5f;
        height = (float)chart.Height / 1.5f;
#endif

        PdfImage img = new PdfBitmap((await chart.GetStreamAsync(ImageFileFormat.Png)));
        graphics.DrawImage(img, new RectangleF(0, 0, width, height));
        MemoryStream stream = new MemoryStream();
        document.Save(stream);
        document.Close(true);
        stream.Position = 0;
        SavePDF("ChartAsPDF.pdf", stream);
        stream.Flush();
        stream.Dispose();
    }

    private async void SavePDF(string fileName, Stream stream)
    {
        fileName = Path.GetFileNameWithoutExtension(fileName) + ".pdf";

#if ANDROID
        string path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments).ToString();
#else
        string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
#endif
        string filePath = Path.Combine(path, fileName);
        using FileStream fileStream = new(filePath, FileMode.Create, FileAccess.ReadWrite);
        await stream.CopyToAsync(fileStream);
        fileStream.Flush();
        fileStream.Dispose();
    }
}

