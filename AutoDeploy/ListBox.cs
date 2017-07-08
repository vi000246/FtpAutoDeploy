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
        /// <summary>
        /// loop listBox裡被選中的值 呼叫委派deleteFromdb把值刪掉
        /// </summary>
        /// <typeparam name="T">此listBox用的Class</typeparam>
        /// <param name="listBox">listBox的控制項</param>
        /// <param name="deleteFromDb">從DB刪除資料的委派</param>
        public void deleteListBoxItem<T>(System.Windows.Forms.ListBox listBox,Action<T> deleteFromDb)
        {
            System.Windows.Forms.ListBox.SelectedObjectCollection selectedItems = new System.Windows.Forms.ListBox.SelectedObjectCollection(listBox);
            selectedItems = listBox.SelectedItems;

            if (listBox.SelectedIndex != -1)
            {
                foreach (T item in selectedItems)
                {
                    deleteFromDb(item);
                }
            }
        }
    }
}
