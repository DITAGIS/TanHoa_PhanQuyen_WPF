﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PhanQuyen
{
    /// <summary>
    /// Interaction logic for InPhieuKiemtRaWindow.xaml
    /// </summary>
    public partial class InPhieuKiemTraWindow : Window
    {
        public InPhieuKiemTraWindow()
        {
            InitializeComponent();
        }
        public InPhieuKiemTraWindow(string danhBa)
        {
            InitializeComponent();
            gridMain.Children.Add(new UC_InPhieuTieuThuKH(danhBa));
        }
    }
}
