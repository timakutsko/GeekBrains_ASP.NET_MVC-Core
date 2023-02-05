using System;
using System.Collections.Generic;
using System.IO;

namespace MyProject.Services.IReports
{
    /// <summary>
    /// Интерфейс для генерации отчета по продукту
    /// </summary>
    public interface IProductReport
    {
        IEnumerable<(int id, string name, string category, decimal price)> Products { get; set; }
    }
}
