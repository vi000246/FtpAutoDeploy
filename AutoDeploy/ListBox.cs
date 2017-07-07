using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 用來處理跟listBox相關的function
/// </summary>
namespace AutoDeploy
{
    public class ListBox
    {
        //刪除listBox選中的Item
        public void deleteListBoxItem(System.Windows.Forms.ListBox listBox)
        {
            System.Windows.Forms.ListBox.SelectedObjectCollection selectedItems = new System.Windows.Forms.ListBox.SelectedObjectCollection(listBox);
            selectedItems = listBox.SelectedItems;

            if (listBox.SelectedIndex != -1)
            {
                for (int i = selectedItems.Count - 1; i >= 0; i--)
                    listBox.Items.Remove(selectedItems[i]);
            }
        }
    }
}
