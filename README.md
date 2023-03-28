# How to export the chart to PDF in the .NET MAUI Charts
This article describes how to export the chart to the PDF document in the [.NET MAUI Charts](https://www.syncfusion.com/maui-controls/maui-cartesian-charts). To export the chart to the PDF document, you can use the [GetStreamAsync](https://help.syncfusion.com/maui/cartesian-charts/exporting#get-the-stream-of-chart) method of the [SfCartesianChart](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.Charts.SfCartesianChart.html) and Syncfusion.Pdf.PdfDocument library. First, obtain the chart as an image stream using the [GetStreamAsync](https://help.syncfusion.com/maui/cartesian-charts/exporting#get-the-stream-of-chart) method. Then, use the image stream and Syncfusion.Pdf.PdfDocument library to generate the PDF document. Here are the steps to export the chart to the PDF document:
<br>Step 1: Create the cartesian chart with the help of this [guideline](https://help.syncfusion.com/maui/cartesian-charts/getting-started).</br>
Step 2: Add the Syncfusion.Pdf.Net package to your project.</br>
Step 3: Generate a pdf document for the chart.</br>
**[C#]**
```
private async void Button_Clicked(object sender, EventArgs e)
    {
        PdfDocument document = new PdfDocument();
        PdfPage page = document.Pages.Add();
        PdfGraphics graphics = page.Graphics;
       
        float width = (float)chart.Width + 75;
        float height = (float)chart.Height +125;

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
```
> **_Note:_**
The chart can be exported as a PDF document only when the chart view is added to the visual tree.

<br>Step 4: Save the PDF document in the default Documents directory.</br>
**[C#]**
```
private async void SavePDF(string fileName, Stream stream)
    {
        fileName = Path.GetFileNameWithoutExtension(fileName) + ".pdf";
        
#if ANDROID
        string path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments).ToString();
#else
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
#endif
        string filePath = Path.Combine(path, fileName);
        using FileStream fileStream = new(filePath, FileMode.Create, FileAccess.ReadWrite);
        await stream.CopyToAsync(fileStream);
        fileStream.Flush();
        fileStream.Dispose();
    }
```
> **_Note:_**
To save the PDF document on Android and Windows Phone devices, you must enable file writing permissions on the device storage.

**Output:**

<img width="475" alt="ExportedChartAsPDF" src="https://user-images.githubusercontent.com/105496706/228247790-33e5b940-ef32-4c91-bff0-94bc263610f4.png">

