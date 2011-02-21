using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace NXEIP.FileManager
{

    /// <summary>
    /// FileManagerUtil 的摘要描述
    /// </summary>
    public class FileManagerUtil
    {
        public FileManagerUtil()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }


        public static int GetParentId(String pid)
        {
            //如果可以轉INT 那就不用處理
            if (pid == null) {
                return 0;
            }
            
            
            
            
            int result;

            if (int.TryParse(pid, out result))
            {

                return result;
            }
            else
            {
                String[] value = pid.Split('_');

                return int.Parse(value[1]);
            }


            //無法轉INT 就取_後面的數字

        }

    }
}