using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA;
using FISCA.Presentation;
using FISCA.Permission;

namespace ExampleLogAndPermission
{
    public class Program
    {
        [MainMethod()]
        static public void Main()
        {
            RibbonBarItem Print = FISCA.Presentation.MotherForm.RibbonBarItems["學生", "其它"];
            Print["加一筆Log"].Enable = 加一筆Log權限;
            Print["加一筆Log"].Click += delegate
            {
                //整理Log內容
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("記錄第一筆");
                sb.AppendLine("記錄第二筆");
                sb.AppendLine("記錄第三筆");

                //透過FISCA.LogAgent來進行LOG的記錄
                //第一欄為功能名稱
                //第二欄是行為動作(新增/修改/刪除)
                //第三欄是LOG內容
                FISCA.LogAgent.ApplicationLog.Log("LOG試範模組", "試範", sb.ToString());

            };

            Catalog detail1 = RoleAclSource.Instance["學生"]["功能按鈕"];

            //RibbonFeature 增加一個功能只有(執行/無)兩種權限設定
            detail1.Add(new RibbonFeature(加一筆Log, "加一筆Log"));

            //DetailItemFeature 增加一個具有(檢視/編輯/無)三種權限的設定
            //一般是用在資料項目上
            //detail1.Add(new DetailItemFeature(Permissions.社團基本資料, "基本資料"));
        }

        static public string 加一筆Log { get { return "ExampleLogAndPermission.Pro.cs"; } }
        static public bool 加一筆Log權限
        {
            get
            {
                //是否有執行權限
                return FISCA.Permission.UserAcl.Current[加一筆Log].Executable;
                
                //是否有編輯權限
                //return FISCA.Permission.UserAcl.Current[加一筆Log].Editable;
                //是否有檢視權限
                //return FISCA.Permission.UserAcl.Current[加一筆Log].Viewable;

            }
        }
    }
}
