using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Globalization;
using Core;

namespace ClubSchool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static CultureInfo Culture { get; set; } = new CultureInfo("ru-RU");
        public static Teacher Teacher { get; set; }
    }
}
