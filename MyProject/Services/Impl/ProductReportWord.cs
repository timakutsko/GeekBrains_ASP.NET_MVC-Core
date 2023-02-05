using MyProject.Services.IReports;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateEngine.Docx;

namespace MyProject.Services.Impl
{
    internal class ProductReportWord : IReport, IProductReport
    {
        private const string _fieldCatalogName = "CatalogName";
        private const string _fieldCatalogDescription = "CatalogDescription";
        private const string _fieldCreationDate = "CreationDate";
        private const string _fieldProduct = "Product";
        private const string _fieldProductId = "ProductId";
        private const string _fieldProductName = "ProductName";
        private const string _fieldProductCategory = "ProductCategory";
        private const string _fieldProductPrice = "ProductPrice";
        private const string _fieldProductTotal = "ProductTotal";
        private readonly FileInfo _templateFile;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="templateFile">Наименование файла-шаблона</param>
        public ProductReportWord(string templateFile)
        {
            _templateFile = new FileInfo(templateFile);
        }
        
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public IEnumerable<(int id, string name, string category, decimal price)> Products { get; set; }

        /// <summary>
        /// Создание отчета
        /// </summary>
        /// <param name="reportTemplateFile">Наименование файла-отчета</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public FileInfo Create(string reportFilePath)
        {
            if (!_templateFile.Exists)
                throw new FileNotFoundException();

            FileInfo reportFile = new FileInfo(reportFilePath);
            reportFile.Delete();
            _templateFile.CopyTo(reportFile.FullName);

            TableRowContent[] rows = Products.Select(product => new TableRowContent(new List<FieldContent>
                {
                    new FieldContent(_fieldProductId, product.id.ToString()),
                    new FieldContent(_fieldProductName, product.name),
                    new FieldContent(_fieldProductCategory, product.category),
                    new FieldContent(_fieldProductPrice, product.price.ToString("c"))
                })).ToArray();

            Content content = new Content(
                new FieldContent(_fieldCatalogName, Name),
                new FieldContent(_fieldCatalogDescription, Description),
                new FieldContent(_fieldCreationDate, CreationDate.ToString("dd.MM.yyyy HH:mm:ss")),
                TableContent.Create(_fieldProduct, rows),
                new FieldContent(_fieldProductTotal, Products.Sum(product => product.price).ToString("c"))
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
