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
            try
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
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        /// <summary>
        /// 重新命名Deploy群組
        /// </summary>
        /// <param name="item"></param>
        public static void RenameDeployGroupItem(model.Deploy_M item) {
            try
            {
                //取得修改後的名稱
                string name = dialog.ShowInputGroupNameForm("重新命名", "請輸入群組名稱 \n 原名稱(" + item.Name + ")");
                if (!string.IsNullOrEmpty(name))
                {
                    item.Name = name;
                    new db().UpdateDataToDB(item);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
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
        /// <summary>
        /// 取得檔案清單listbox所有路徑底下的所有檔案
        /// </summary>
        /// <param name="lbFileList"></param>
        /// <param name="OnlyGetHighLightPath">true:只搜尋有反白的路徑</param>
        /// <returns></returns>
        public static List<string> GetAllPath(ListBox lbFileList,bool OnlyGetHighLightPath = false) {
            List<string> files = new List<string>();
            file file = new file();

            //如果只取selected的path
            if (OnlyGetHighLightPath)
            {
                ListBox.SelectedObjectCollection selectedItems = new ListBox.SelectedObjectCollection(lbFileList);
                selectedItems = lbFileList.SelectedItems;
                foreach (model.Deploy_D item in selectedItems)
                {
                    List<string> temp = file.getAllFiles(item.Path);
                    //取得檔案listBox中的所有檔案
                    files = files.Concat(temp).ToList();
                }
            }
            else
            {
                foreach (model.Deploy_D item in lbFileList.Items)
                {
                    List<string> temp = file.getAllFiles(item.Path);
                    //取得檔案listBox中的所有檔案
                    files = files.Concat(temp).ToList();
                }
            }
            return files;
        }
    }
}
