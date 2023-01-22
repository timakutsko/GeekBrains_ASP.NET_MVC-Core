﻿using System;
using System.IO;

namespace MyProject.Services.IReports
{
    internal interface IReport
    {
        string Name { get; set; }

        string Description { get; set; }

        DateTime CreationDate { get; set; }

        FileInfo Create(string reportFilePath);
    }
}
