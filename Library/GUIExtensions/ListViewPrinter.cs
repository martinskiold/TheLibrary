//Library
//Martin Skiöld
//Version 1.0 2015-11-02
using Library.Models;
using Library.Repositories;
using Library.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Library
{
    public static class ListViewPrinter
    {
        /// <summary>
        /// Prints the ColumnDataMatrix to the ListView.
        /// </summary>
        /// <param name="items"></param>
        public static void UpdateAllRows(List<string[]> columnDataMatrix, ListView lv)
        {
            if (lv != null)
            {
                // Clears Data in the ListView.
                lv.Items.Clear();

                if (columnDataMatrix != null)
                {
                    // Print if there is any data to print.
                    if (columnDataMatrix.Count > 0)
                    {
                        foreach (string[] rowData in columnDataMatrix)
                        {
                            lv.Items.Add(new ListViewItem(rowData));
                        }
                    }

                }
            }
            else
            {
                MessageBox.Show("ListViewNullReferenceError","TheLibraryApplication has encountered a Non-Critical Error when printing data to the screen. Please notify Support and then Continue normally.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Shell to extend the class behaviour with single row updates.
        /// </summary>
        /// <param name="columnData"></param>
        /// <param name="lv"></param>
        // public static void SingleRowUpdate(string[] columnData, ListView lv) 
        // {
        // }
    }
}
