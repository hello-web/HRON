﻿using HRONLib;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;

namespace HRON.Views
{
    /// <summary>
    /// Interaktionslogik für MasterDataDocumentations.xaml
    /// </summary>
    public class MasterDataDocumentations<T, TEntity> : MasterData<T, TEntity> where T : DbSet<TEntity> where TEntity : baseEntity
    {
        public MasterDataDocumentations(T e, HRONEntities context) : base(e, context)
        {
            grdMasterData.AutoGeneratedColumns += GrdMasterData_AutoGeneratedColumns;
        }

        private void GrdMasterData_AutoGeneratedColumns(object sender, EventArgs e)
        {
            DataGridTemplateColumn dc = new DataGridTemplateColumn();

            FrameworkElementFactory factory1 = new FrameworkElementFactory(typeof(Button));
            factory1.AddHandler(Button.ClickEvent, new RoutedEventHandler(Btn_Click));
            //factory1.Content = "Load File";
            factory1.SetValue(Button.ContentProperty, "Load File");
            DataTemplate cellTemplate1 = new DataTemplate();
            cellTemplate1.VisualTree = factory1;
            dc.CellTemplate = cellTemplate1;

            grdMasterData.Columns.Insert(3, dc);

            foreach (DataGridColumn d in grdMasterData.Columns)
            {
                if (d.SortMemberPath == "documentationExpireTime")
                {
                    DataGridTextColumn tc = (DataGridTextColumn)d;
                    System.Windows.Data.Binding bind = (System.Windows.Data.Binding)tc.Binding;
                    bind.Converter = new TimeSpanConverter();
                    MessageBox.Show(d.ToString());
                }
            }
        }


        protected void Btn_Click(object sender, RoutedEventArgs e)
        {
            if (grdMasterData.SelectedItem is baDocumentation)
            {
                baDocumentation d = (baDocumentation)grdMasterData.SelectedItem;
                OpenFileDialog newFile = new OpenFileDialog();
                newFile.CheckFileExists = true;
                newFile.DereferenceLinks = true;
                newFile.Multiselect = false;
                newFile.Filter = "Word Templates(*.dot, *dotx) |*.dot;*.dotx| All Files(*.*) |*.*";
                newFile.Title = "Please choose your files...";
                if (newFile.ShowDialog() == true)
                {
                    FileInfo fi = new FileInfo(newFile.FileName);

                    d.documentationDocument = File.ReadAllBytes(newFile.FileName);
                    d.documentationDocumentName = fi.Name;
                }
            }
            grdMasterData.Items.Refresh();
        }
    }
    public class TimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
                return value + " Days";
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            String t = value.ToString();
            t = t.Replace("Days", "").Trim();
            return Int32.Parse(t);
        }
    }
}