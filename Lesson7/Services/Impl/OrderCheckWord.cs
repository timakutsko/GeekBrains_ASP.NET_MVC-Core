using Lesson7.Services.IReports;
using Orders.DAL.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateEngine.Docx;

namespace Lesson7.Services.Impl
{
    internal class OrderCheckWord : IReport, IOrderReport
    {
        private const string _fieldBuyerName = "BuyerName";
        private const string _fieldOrderDescription = "OrderDescription";
        private const string _fieldCreationDate = "CreationDate";
        private const string _fieldProduct = "Product";
        private const string _fieldProductName = "ProductName";
        private const string _fieldProductPrice = "ProductPrice";
        private const string _fieldProductCount = "ProductCount";
        private const string _fieldOrderTotal = "OrderTotal";
        private readonly FileInfo _templateFile;

        public OrderCheckWord(string templateFile)
        {
            _templateFile = new FileInfo(templateFile);
        }

        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public ICollection<OrderItem> Items { get; set; }

        public FileInfo Create(string reportFilePath)
        {
            if (!_templateFile.Exists)
                throw new FileNotFoundException();

            FileInfo reportFile = new FileInfo(reportFilePath);
            reportFile.Delete();
            _templateFile.CopyTo(reportFile.FullName);

            TableRowContent[] rows = Items.Select(item => new TableRowContent(new List<FieldContent>
                {
                    new FieldContent(_fieldProductName, item.Product.Name),
                    new FieldContent(_fieldProductCount, item.Quantity.ToString()),
                    new FieldContent(_fieldProductPrice, item.Product.Price.ToString("c"))
                })).ToArray();

            Content content = new Content(
                new FieldContent(_fieldBuyerName, Name),
                new FieldContent(_fieldOrderDescription, Description),
                new FieldContent(_fieldCreationDate, CreationDate.ToString("dd.MM.yyyy HH:mm:ss")),
                TableContent.Create(_fieldProduct, rows),
                new FieldContent(_fieldOrderTotal, Items.Sum(item => item.Product.Price * item.Quantity).ToString("c"))
                );

            TemplateProcessor processor = new TemplateProcessor(reportFile.FullName)
                .SetRemoveContentControls(true);

            processor.FillContent(content);
            processor.SaveChanges();
            reportFile.Refresh();

            return reportFile;
        }
    }
}
