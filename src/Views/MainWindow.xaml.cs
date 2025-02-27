using System.Windows;
using System.Windows.Navigation;
using LenovoSerialToModelNumber.Services;
using Microsoft.Win32;

namespace LenovoSerialToModelNumber.Views;

public partial class MainWindow : Window
{
    private readonly ILenovoApiService lenovoApiService;
    private readonly ICsvService csvService;
    private string? csvFilePath;
    
    public MainWindow(ILenovoApiService lenovoApiService, ICsvService csvService)
    {
        this.lenovoApiService = lenovoApiService;
        this.csvService = csvService;
        InitializeComponent();
    }
    
    private async void FetchDataButton_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(csvFilePath))
        {
            StatusTextBlock.Text = "Please create a CSV file first.";
            StatusTextBlock.TextAlignment = TextAlignment.Center;
            return;
        }
        
        var serialNumber = SerialNumberTextBox.Text;

        var product = await lenovoApiService.GetProductBySerialNumberAsync(serialNumber);

        if (product != null)
        {
            StatusTextBlock.Text = $"Product: {serialNumber} has been written to the CSV file.";

            await csvService.WriteToCsvAsync(csvFilePath, product);
        }
        else
        {
            StatusTextBlock.Text = "Could not get model number.";
        }

        StatusTextBlock.TextAlignment = TextAlignment.Center;
    }
    
    private async void CreateCsvButton_Click(object sender, RoutedEventArgs e)
    {
        var saveFileDialog = new SaveFileDialog
        {
            Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
            DefaultExt = ".csv",
            FileName = "LenovoSerialToModelNumber.csv"
        };

        if (saveFileDialog.ShowDialog() != true)
            return;
        
        csvFilePath = saveFileDialog.FileName;

        try
        {
            await csvService.CreateCsvFileAsync(csvFilePath);
                
            MessageBox.Show($"CSV file created successfully at {csvFilePath}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error creating CSV file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
    }
}