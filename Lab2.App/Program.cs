using System;
using System.Globalization;
using System.Text;
using Lab2.App.Task1;

Console.OutputEncoding = Encoding.UTF8;
CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;

Console.WriteLine("Лабораторная работа №2");

const int primeThreads = 4;
PrimeCounting.RunAllVersions(primeThreads);

const int maxConcurrentThreads = 3;
const string datasetsFile = "lab2_datasets.csv";
DatasetsProcessing.Run(datasetsFile, maxConcurrentThreads);
