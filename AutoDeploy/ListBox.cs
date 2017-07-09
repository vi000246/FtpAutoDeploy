using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/// <summary>
/// 用來處理跟listBox相關的function
/// </summary>
namespace AutoDeploy
{
    public class ListBoxUtility
    {
        /// <summary>
        /// loop listBox裡被選中的值 呼叫委派處理item
        /// </summary>
        /// <typeparam name="T">此listBox用的Class</typeparam>
        /// <param name="listBox">listBox的控制項</param>
        /// <param name="deleteFromDb">從DB處理資料的委派</param>
        public void LoopListBoxItem<T>(ListBox listBox,Action<T> DoSomeThingFromDb)
        {
            //如果是listBox就loop被selected的item
            if (listBox.GetType() == typeof(ListBox))
            {
                ListBox.SelectedObjectCollection selectedItems = new ListBox.SelectedObjectCollection(listBox);
                selectedItems = listBox.SelectedItems;

                if (listBox.SelectedIndex != -1)
                {
                    foreach (T item in selectedItems)
                    {
                        DoSomeThingFromDb(item);
                    }
                }
            }
            //如果是checkedListBox 就loop被勾選的值
            else if (listBox.GetType() == typeof(CheckedListBox))
            {
                foreach (T itemChecked in (listBox as CheckedListBox).CheckedItems)
                {
                    DoSomeThingFromDb(itemChecked);
                }
            }
        }

        /// <summary>
        /// 重新命名Deploy群組
        /// </summary>
        /// <param name="item"></param>
        public static void RenameDeployGroupItem(model.Deploy_M item) {
            //取得修改後的名稱
            string name = dialog.ShowInputGroupNameForm("重新命名", "請輸入群組名稱 \n 原名稱("+item.Name+")");
            if (!string.IsNullOrEmpty(name))
            {
                item.Name = name;
                new db().UpdateDataToDB(item);
            }
        }

        /// <summary>
        /// 將checkedListBox的所有項目unchecked
        /// </summary>
        /// <param name="listbox"></param>
        public static void unCheckAll(CheckedListBox listbox) {
            //將所有項目unchecked
            foreach (int i in listbox.CheckedIndices)
            {
                listbox.SetItemCheckState(i, CheckState.Unchecked);
            }
        }
    }
}
