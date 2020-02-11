using PeterKApplication.Annotations;
using PeterKApplication.Data;
using PeterKApplication.Models;
using PeterKApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using PeterKApplication.Extensions;
using PeterKApplication.Services;
using PeterKApplication.Shared.Enums;

using Syncfusion.Pdf;
using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using System.IO;
using System.Reflection;
using PeterKApplication.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;
/*using Java.IO;*/
using File = System.IO.File;
using System.Threading;
using Plugin.BLE;
using System.Diagnostics;
using Plugin.BLE.Abstractions.Exceptions;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.Toasts;

namespace PeterKApplication.ViewModels
{
    class CheckoutViewModel : INotifyPropertyChanged
    {
        private List<PaymentType> _paymentTypes;
        private List<Product> _products;
        private decimal _total;
        private string _totalTextWithTax;
        private List<PairedListPair> _pairedPaymentTypes;


        private bool _isEdit = false;
        private String _deliveryAddress;
        private decimal _deliveryPrice = 0;
        private decimal _discount = 0;

        private AppUser _me;

        private string _currencyFormat;
        private string _checkoutImage;

        public event PropertyChangedEventHandler PropertyChanged;


        public CheckoutViewModel()
        {
            Me = AuthService.CurrentUser();
            CurrencyFormat = Me.CurrencyFormat;
        }


        public async Task InitializeAsync()
        {
            using (var db = new LocalDbContext())
            {
                PaymentTypes = db.PaymentTypes.ToList();
            }
        }

        

        public string CheckoutImage
        {
            get => _checkoutImage;
            set
            {
                _checkoutImage = value;
                OnPropertyChanged(nameof(CheckoutImage));
            }
        }

        public AppUser Me
        {
            get => _me;
            set
            {
                _me = value;
                OnPropertyChanged(nameof(Me));
            }
        }


        public string DiscountLabel => "Discount( "  + CurrencyFormat + ") : ";
        public string DeliveryLabel => "Price (" + CurrencyFormat + ") : ";
        public string TotalTextWithTax => "(" + CurrencyFormat + $"{Math.Round(Total + Total * (decimal)(Me.Tax / 100)):N2})" + CurrencyFormat + $"{Math.Round(Total * (decimal)(Me.Tax / 100)):N2}";
        public decimal Total 
        { 
            get => Products != null ? Products.Sum(s => s.Price * s.Quantity) + _deliveryPrice - _discount : 0;
            set
            {
                _total = value;
                OnPropertyChanged(nameof(Total));
            }
        } 

        public string TotalLabel => CurrencyFormat + $"{Total:N2}";
        public string TaxLabel => "Tax ( VAT " + Me.Tax + "%)";



        public List<PaymentType> PaymentTypes
        {
            get => _paymentTypes;
            set
            {
                _paymentTypes = value;
                OnPropertyChanged(nameof(PaymentTypes));
                PairedPaymentTypes = value?.Aggregate(new List<PairedListPair>
                {
                    new PairedListPair()
                }, (list, paymenttype) =>
                {
                    var item = new PairedListItem
                    {
                        Id = paymenttype.Id,
                        Name = paymenttype.Name
                    };

                    var c = list.Count - 1;

                    if (list[c].Item1 == null)
                    {
                        list[c].Item1 = item;
                    }
                    else if (list[c].Item2 == null)
                    {
                        list[c].Item2 = item;
                        list.Add(new PairedListPair());
                    }

                    return list;
                });
            }
        }

        public List<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
                OnPropertyChanged(nameof(Total));
                OnPropertyChanged(nameof(TotalLabel));
                OnPropertyChanged(nameof(TotalTextWithTax));
            }
        }

        public string CurrencyFormat
        {
            get => _currencyFormat;
            set 
            {
                _currencyFormat = value;
                OnPropertyChanged(nameof(CurrencyFormat));
            }
        }


        public static CheckoutViewModel Self => null;


           
        public List<PairedListPair> PairedPaymentTypes
        {
            get => _pairedPaymentTypes;
            set
            {
                _pairedPaymentTypes = value;
                OnPropertyChanged(nameof(PairedPaymentTypes));
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task Save(OrderStatus status)
        {
            using (var db = new LocalDbContext())
            {
                var payment = PairedPaymentTypes.Selected();
                PaymentType paymentType = payment != null
                    ? db.PaymentTypes.FirstOrDefault(f => f.Name.ToLower().Equals(payment.Name.ToLower()))
                    : db.PaymentTypes.FirstOrDefault(f => f.Name.ToLower().Equals("CASH".ToLower()));

                var order = new Order
                {
                    BusinessId = AuthService.BusinessId,
                    Id = Guid.NewGuid(),
                    AppUserId = AuthService.UserId,
                    OrderNumber = db.Orders.Any() ? db.Orders.Max(o => o.OrderNumber) + 1 : 1,
                    PaymentTypeId = paymentType.Id,
                    OrderedOn = DateTime.Now,
                    DeliveryAddress = DeliveryAddress,
                    Discount    = Discount,
                    OrderStatus = status,
                    IsSynced = false,
                };

                
                db.Orders.Add(order);
                await db.SaveChangesAsync();

                foreach (var product in Products)
                {
                    db.OrderItems.Add(new OrderItem
                    {
                        OrderId = order.Id,
                        Price = product.Price,
                        Quantity = Convert.ToInt32(product.Quantity),
                        ProductId = product.Id
                    });
                }

                await db.SaveChangesAsync();
            }
        }

        public bool IsEdit
        {
            get => _isEdit;
            set
            {
                _isEdit = value;
                OnPropertyChanged(nameof(IsEdit));
            }
        }


        public String DeliveryAddress
        {
            get => _deliveryAddress;
            set
            {
                _deliveryAddress = value;
                OnPropertyChanged(nameof(DeliveryAddress));
            }
        }

        public Decimal DeliveryPrice
        {
            get => _deliveryPrice;
            set 
            {
                _deliveryPrice = value;
                OnPropertyChanged(nameof(DeliveryPrice));
                OnPropertyChanged(nameof(Total));
                OnPropertyChanged(nameof(TotalTextWithTax));
            }
        }

        public Decimal Discount
        {
            get => _discount;
            set
            {
                _discount = value;
                OnPropertyChanged(nameof(Discount));
                OnPropertyChanged(nameof(Total));
                OnPropertyChanged(nameof(TotalTextWithTax));
            }
        }

        private void LoadStaffData() 
        {
            using (var db = new LocalDbContext())
            {
                /*List<AppUser> staffs = db.ProductCategories.ToList();*/
            }
        }

        public async Task SaveCartAsInvoiceAsync(string fileName)
        {
            AppUser currentUser = AuthService.CurrentUser();
            string currencyFormat = currentUser.CurrencyFormat;
            float tax = currentUser.Tax;
            string staffName = currentUser.FirstName + " " + currentUser.LastName;

            //Creates a new PDF document
            PdfDocument document = new PdfDocument();
            //Adds page settings
            document.PageSettings.Orientation = PdfPageOrientation.Portrait;
            document.PageSettings.Margins.All = 50;
            //Adds a page to the document
            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;

                 
            System.Diagnostics.Debug.Write("PDFdocument width : ", graphics.ClientSize.Width.ToString());
            System.Diagnostics.Debug.Write("PDFdocument height : ", graphics.ClientSize.Height.ToString());

            //Loads the image as stream

            Stream imageStream = typeof(App).GetTypeInfo().Assembly.GetManifestResourceStream("logo.png");
            RectangleF bounds = new RectangleF(176, 0, 390, 130);
          /*  PdfImage image = PdfImage.FromStream(imageStream);
            //Draws the image to the PDF page
            page.Graphics.DrawImage(image, bounds);*/


            PdfBrush solidBrush = new PdfSolidBrush(new PdfColor(126, 151, 173));
            bounds = new RectangleF(0, bounds.Top, graphics.ClientSize.Width, 30);


            //Creates a font for adding the heading in the page
            PdfFont subHeadingFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 20);  
            PdfTextElement element = new PdfTextElement("BUSINESS NAME", subHeadingFont);
            element.Brush = PdfBrushes.Black;
            PdfLayoutResult result = element.Draw(page, new PointF(0, bounds.Top));


            string currentDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm");
            PdfFont timesRoman = new PdfStandardFont(PdfFontFamily.TimesRoman, 14);

            //Measures the width of the text to place it in the correct location
            SizeF textSize = timesRoman.MeasureString(currentDate);
            PointF textPosition = new PointF(graphics.ClientSize.Width - textSize.Width - 10, result.Bounds.Bottom + 10);
            graphics.DrawString(currentDate, timesRoman, element.Brush, textPosition);


            //Creates text elements to add the address and draw it to the page.
            element = new PdfTextElement(fileName, timesRoman);
            element.Brush = PdfBrushes.Black;
            result = element.Draw(page, new PointF(0, result.Bounds.Bottom + 60));


            element = new PdfTextElement("DELIVERY : "  + (_deliveryAddress != null ? _deliveryAddress : ""), timesRoman);
            result = element.Draw(page, new PointF(0, result.Bounds.Bottom));


            // Create the `DataTable` structure according to your data source
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("Product Name", typeof(string));
            table.Columns.Add("Quantity", typeof(string));
            table.Columns.Add("UnitPrice (" + currencyFormat  + ")", typeof(string));
            table.Columns.Add("Price (" + currencyFormat  + ")", typeof(string));


            // Iterate through data source object and fill the table
            foreach (var item in Products)
            {
                table.Rows.Add(item.Name, item.Quantity, item.Price.ToString(), (item.Quantity * item.Price).ToString());
            }

            table.Rows.Add(" ", " ", " ", " ");
            table.Rows.Add("Delivery Price", "", "", DeliveryPrice.ToString());
            table.Rows.Add("Discount", "", "", Discount.ToString());

            table.Rows.Add("Sub Total", "", "", Total.ToString());
            table.Rows.Add("Total (VAT 16%)", "", "", $"{Math.Round(Total + Total * (decimal)tax ):N2} ({Math.Round(Total * (decimal)tax):N2})");

            System.Data.DataTable invoiceDetails = table;
            //Creates a PDF grid
            PdfGrid grid = new PdfGrid();
            //Adds the data source
            grid.DataSource = invoiceDetails;
            //Creates the grid cell styles
            PdfGridCellStyle cellStyle = new PdfGridCellStyle();
            cellStyle.Borders.All = PdfPens.White;
            PdfGridRow header = grid.Headers[0];


            //Creates the header style
            PdfGridCellStyle headerStyle = new PdfGridCellStyle();

            /*headerStyle.Borders.All = new PdfPen(new PdfColor(126, 151, 173));*/
            /*headerStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor(126, 151, 173));*/

            headerStyle.Borders.All = PdfPens.White;
            headerStyle.TextBrush = PdfBrushes.Black;
            headerStyle.Font = timesRoman;

            //Adds cell customizations
            for (int i = 0; i < header.Cells.Count; i++)
            {
                if (i == 0)
                    header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
                else
                    header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);
            }
            header.ApplyStyle(headerStyle);


            for (int i = 0; i < grid.Rows.Count; i++) {

                for (int j = 0; j < grid.Rows[i].Cells.Count; j++)

                    if(j == 0)
                        grid.Rows[i].Cells[j].StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
                    else
                        grid.Rows[i].Cells[j].StringFormat = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);

                grid.Rows[i].ApplyStyle(headerStyle);
            }
            

            
            //Creates the layout format for grid
            PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();
            // Creates layout format settings to allow the table pagination
            layoutFormat.Layout = PdfLayoutType.Paginate;
            
            //Draws the grid to the PDF page.
            PdfGridLayoutResult gridResult = grid.Draw(page, new RectangleF(new PointF(0, result.Bounds.Bottom + 40), new SizeF(graphics.ClientSize.Width, graphics.ClientSize.Height - 100)), layoutFormat);


            element = new PdfTextElement("PREPARED BY " + staffName, timesRoman);
            result = element.Draw(page, new PointF(0, graphics.ClientSize.Height - 120));


            PdfFont copyrightFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 9);
            element = new PdfTextElement("POWERED BY SEAMPOS", copyrightFont);
            result = element.Draw(page, new PointF(0, graphics.ClientSize.Height - 20));


            //Save the PDF document to stream.
            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            //Close the document.
            document.Close(true);


            //Save the stream as a file in the device

            string fileNameExtension = fileName + ".pdf";
            
            String filePath = "";
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..",
                        "Library", fileNameExtension);

                    //Create a file and write the stream into it.
                    FileStream fileStream = File.Open(filePath, FileMode.Create);
                    stream.Position = 0;
                    stream.CopyTo(fileStream);

                    fileStream.Flush();
                    fileStream.Close();

                    break;

                case Device.Android:
                    filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                        fileNameExtension);


                   /* Java.IO.File file = new Java.IO.File(Environment.GetFolderPath(Environment.SpecialFolder.Personal), fileNameExtension);

                    //Remove the file if exists.
                    if (file.Exists()) file.Delete();

                    //Write the stream into file.
                    FileOutputStream outs = new FileOutputStream(file);
                    outs.Write(stream.ToArray());

                    outs.Flush();
                    outs.Close();
*/

                    break;

                default:
                    throw new NotImplementedException("Platform not supported");
            }


            // Share Saved Invoice Pdf File 
            await Share.RequestAsync(new ShareFileRequest
            {
                Title = fileNameExtension,
                File = new ShareFile(filePath)
            });

        }

        public async Task ShareReceipt(string fileName)
        {
            AppUser currentUser = AuthService.CurrentUser();
            string currencyFormat = currentUser.CurrencyFormat;
            float tax = currentUser.Tax;
            string staffName = currentUser.FirstName + " " + currentUser.LastName;

            //Creates a new PDF document
            PdfDocument document = new PdfDocument();
            //Adds page settings
            document.PageSettings.Orientation = PdfPageOrientation.Landscape;
            document.PageSettings.Margins.All = 50;
            //Adds a page to the document
            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;

            //Loads the image as stream

            Stream imageStream = typeof(App).GetTypeInfo().Assembly.GetManifestResourceStream("logo.png");
            RectangleF bounds = new RectangleF(176, 0, 390, 130);
            /*  PdfImage image = PdfImage.FromStream(imageStream);
              //Draws the image to the PDF page
              page.Graphics.DrawImage(image, bounds);*/


            PdfBrush solidBrush = new PdfSolidBrush(new PdfColor(126, 151, 173));
            bounds = new RectangleF(0, bounds.Top - 20, graphics.ClientSize.Width, 30);
            //Draws a rectangle to place the heading in that region.
            graphics.DrawRectangle(solidBrush, bounds);

            //Creates a font for adding the heading in the page
            PdfFont subHeadingFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 14);

            //Creates a text element to add the invoice number
            PdfTextElement element = new PdfTextElement(fileName, subHeadingFont);
            element.Brush = PdfBrushes.White;

            //Draws the heading on the page
            PdfLayoutResult result = element.Draw(page, new PointF(10, bounds.Top));
            string currentDate = "DATE " + DateTime.Now.ToString("MM/dd/yyyy");

            //Measures the width of the text to place it in the correct location
            SizeF textSize = subHeadingFont.MeasureString(currentDate);
            PointF textPosition = new PointF(graphics.ClientSize.Width - textSize.Width - 10, result.Bounds.Y);

            //Draws the date by using DrawString method
            graphics.DrawString(currentDate, subHeadingFont, element.Brush, textPosition);
            PdfFont timesRoman = new PdfStandardFont(PdfFontFamily.TimesRoman, 10);


            //Creates text elements to add the address and draw it to the page.
            element = new PdfTextElement("BILL TO ", timesRoman);
            element.Brush = new PdfSolidBrush(new PdfColor(126, 155, 203));
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 25));


            element = new PdfTextElement(staffName, timesRoman);
            element.Brush = new PdfSolidBrush(new PdfColor(0, 0, 0));
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 25));


            /* PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
             graphics.DrawString(_deliveryAddress != null ? _deliveryAddress : "", font, PdfBrushes.Black, new PointF(0, 0));*/

            element = new PdfTextElement(_deliveryAddress != null ? _deliveryAddress : "", timesRoman);
            element.Brush = new PdfSolidBrush(new PdfColor(0, 0, 0));
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 25));


            PdfPen linePen = new PdfPen(new PdfColor(126, 151, 173), 0.70f);
            PointF startPoint = new PointF(0, result.Bounds.Bottom + 3);
            PointF endPoint = new PointF(graphics.ClientSize.Width, result.Bounds.Bottom + 3);
            //Draws a line at the bottom of the address
            graphics.DrawLine(linePen, startPoint, endPoint);


            //Creates the datasource for the table


            // Create the `DataTable` structure according to your data source
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("ProductID", typeof(string));
            table.Columns.Add("ProductName", typeof(string));
            table.Columns.Add("Quantity", typeof(string));
            table.Columns.Add("UnitPrice (" + currencyFormat + ")", typeof(string));
            table.Columns.Add("Price (" + currencyFormat + ")", typeof(string));


            // Iterate through data source object and fill the table
            foreach (var item in Products)
            {
                table.Rows.Add(item.Id, item.Name, item.Quantity, item.Price.ToString(), (item.Quantity * item.Price).ToString());
            }

            table.Rows.Add(" ", " ", " ", " ", " ");
            table.Rows.Add("Delivery Price", "", "", "", DeliveryPrice.ToString());
            table.Rows.Add("Discount", "", "", "", Discount.ToString());

            table.Rows.Add("Sub Total", "", "", "", Total.ToString());
            table.Rows.Add("Total (VAT 16%)", "", "", "", $"{Math.Round(Total + Total * (decimal)tax):N2} ({Math.Round(Total * (decimal)tax):N2})");


            System.Data.DataTable invoiceDetails = table;
            //Creates a PDF grid
            PdfGrid grid = new PdfGrid();
            //Adds the data source
            grid.DataSource = invoiceDetails;
            //Creates the grid cell styles
            PdfGridCellStyle cellStyle = new PdfGridCellStyle();
            cellStyle.Borders.All = PdfPens.White;
            PdfGridRow header = grid.Headers[0];
            //Creates the header style
            PdfGridCellStyle headerStyle = new PdfGridCellStyle();
            headerStyle.Borders.All = new PdfPen(new PdfColor(126, 151, 173));
            headerStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor(126, 151, 173));
            headerStyle.TextBrush = PdfBrushes.White;
            headerStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 14f, PdfFontStyle.Regular);

            //Adds cell customizations
            for (int i = 0; i < header.Cells.Count; i++)
            {
                if (i == 0 || i == 1)
                    header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
                else
                    header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);
            }

            //Applies the header style
            header.ApplyStyle(headerStyle);
            cellStyle.Borders.Bottom = new PdfPen(new PdfColor(217, 217, 217), 0.70f);
            cellStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12f);
            cellStyle.TextBrush = new PdfSolidBrush(new PdfColor(131, 130, 136));

            //Creates the layout format for grid
            PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();
            // Creates layout format settings to allow the table pagination
            layoutFormat.Layout = PdfLayoutType.Paginate;

            //Draws the grid to the PDF page.
            PdfGridLayoutResult gridResult = grid.Draw(page, new RectangleF(new PointF(0, result.Bounds.Bottom + 40), new SizeF(graphics.ClientSize.Width, graphics.ClientSize.Height - 100)), layoutFormat);



            //Save the PDF document to stream.
            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            //Close the document.
            document.Close(true);


            //Save the stream as a file in the device

            string fileNameExtension = fileName + ".pdf";

            String filePath = "";
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..",
                        "Library", fileNameExtension);

                    //Create a file and write the stream into it.
                    

                    break;

                case Device.Android:
                    filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                        fileNameExtension);


                   /* Java.IO.File file = new Java.IO.File(Environment.GetFolderPath(Environment.SpecialFolder.Personal), fileNameExtension);

                    //Remove the file if exists.
                    if (file.Exists()) file.Delete();

                    //Write the stream into file.
                    FileOutputStream outs = new FileOutputStream(file);
                    outs.Write(stream.ToArray());

                    outs.Flush();
                    outs.Close();*/

                    break;

                default:
                    throw new NotImplementedException("Platform not supported");
            }

            FileStream fileStream = File.Open(filePath, FileMode.Create);
            stream.Position = 0;
            stream.CopyTo(fileStream);

            fileStream.Flush();
            fileStream.Close();


            // Share Saved Invoice Pdf File 
            await Share.RequestAsync(new ShareFileRequest
            {
                Title = fileNameExtension,
                File = new ShareFile(filePath)
            });

        }

        public async Task PrintReceipt() {

            var ble = CrossBluetoothLE.Current;
            var adapter = CrossBluetoothLE.Current.Adapter;

            var state = ble.State;
            ble.StateChanged += (s, e) =>
            {
                Debug.WriteLine($"The bluetooth state changed to {e.NewState}");
            };

            List<IDevice> deviceList = new List<IDevice>();
            adapter.ScanTimeout = 3000;
            adapter.DeviceDiscovered += (s, a) => deviceList.Add(a.Device);


            await adapter.StartScanningForDevicesAsync();


            var notificator = DependencyService.Get<IToastNotificator>();

            var options = new NotificationOptions()
            {
                Title = "Infomation",
                Description = "Description"
            };


            if (deviceList.Count == 0)
            {
                options.Description = "There are no devices available to connect.";
                await notificator.Notify(options);
                return;
            }
            

            try
            {
                options.Description = string.Format("Connecting to device : {0}", deviceList[0]);
                await notificator.Notify(options);

                IDevice currentDevice = deviceList[0];
                await adapter.ConnectToDeviceAsync(currentDevice);

                var services = await currentDevice.GetServicesAsync();

                options.Description = string.Format("There are {0} services on connected Device", services.Count);
                await notificator.Notify(options);


                var characteristic = await services[0].GetCharacteristicAsync(Guid.Parse("d8de624e-140f-4a22-8594-e2216b84a5f2"));
                var bytes = await characteristic.ReadAsync();
                await characteristic.WriteAsync(bytes);

                // Characteristic notifications
                characteristic.ValueUpdated += (o, args) =>
                {
                    var bytes = args.Characteristic.Value;
                };

                await characteristic.StartUpdatesAsync();


                /*// Get descriptors
                var descriptors = await characteristic.GetDescriptorsAsync();

                options.Description = string.Format("There are {0} descriptors", descriptors.Count);
                await notificator.Notify(options);


                // Read descriptor
                var bytes = await descriptors[0].ReadAsync();

                // Write descriptor
                await descriptor.WriteAsync(bytes);*/


            }
            catch (DeviceConnectionException e)
            {
                // ... could not connect to device
            }
        }
    }
}