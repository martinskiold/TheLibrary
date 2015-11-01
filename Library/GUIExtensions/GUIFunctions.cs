//Library
//Martin Skiöld
//Version 1.0 2015-11-02
using Library.Models;
using Library.Repositories;
using Library.Services;
using Library.PromptForms;
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
    public static class GUIFunctions
    {
        /// <summary>
        /// Gets the selected item's id from the listview. Returns -1 if failed.
        /// </summary>
        /// <param name="lv"></param>
        /// <returns></returns>
        public static int GetSelectedItemIdFromListView(ListView lv)
        {
            // If the ListView is not empty.
            if (lv.SelectedItems.Count > 0)
            {
                // Gets the text of the selected item's first column.
                string itemId = lv.SelectedItems[0].SubItems[0].Text;
                int parsedId;
                if (int.TryParse(itemId, out parsedId))
                {
                    // Returns the Id of the selected item in the ListView.
                    return parsedId;
                }
            }
            return -1;
        }
    }
}
