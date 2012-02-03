using System;
using System.Collections.Generic;
using System.Text;

namespace CloudPaperApp
{
    public class TempMemoryVariable
    {
        public Dictionary<string, Dictionary<string, object>> MemoryTable = new Dictionary<string, Dictionary<string, object>>();
		
		public void SetDetail(string id ,string detail)
		{
			if (detail == null || detail.Length == 0) return;
			
			string[] arr = null;
			if (detail.IndexOf("|") != -1) {
				arr = detail.Split('|');
			}
			else 
			{
				arr = detail.Split('&');
			}
		
			
			//¥ýclear
            if(this.MemoryTable.ContainsKey(id))
            {
                this.MemoryTable[id] = new Dictionary<string, object>();
            }
			
			for (int i = 0; i < arr.Length; i++)
			{
				string[] arr2 = arr[i].Split('=');
				if (arr2.Length < 2) continue;
				
				string k = arr2[0];
				string v = arr2[1];
				
				if (k.Length > 0) 
				{
                    Dictionary<string, object> table = null;
					if (this.MemoryTable.ContainsKey(id) == false) 
					{
                        table = new Dictionary<string, object>();
                        this.MemoryTable.Add(id, table);
					}
                    table = this.MemoryTable[id];
                    table[k] = v;
				}
			}
		}
		
        /*
		public function SetVar(id:String, data:Object):void
		{
			this.MemoryTable[id] = data;
		}
        */

       
		public object GetVar(string id, string k) 
		{
			if (this.MemoryTable.ContainsKey(id)) 
			{
                Dictionary<string, object> table = this.MemoryTable[id];
                
                return table[k];
			}
			return null;
		}
    }
}
