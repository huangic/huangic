package idv.trans.service.system;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.LinkedHashMap;
import java.util.Map;
import java.util.Map.Entry;

public class Functions {

	HashMap permission;

	public Map getPermission() {
		return permission;
	}

	public Map getPermission(String userLevel, String systemLevel) {

		

		HashMap old = this.permission;
		LinkedHashMap newMap = new LinkedHashMap<String, ArrayList>();

		for (Iterator i = old.entrySet().iterator(); i.hasNext();) {
			Entry entry=(Entry)i.next();
			
			String key = (String)entry.getKey();

			ArrayList<Permission> list = (ArrayList<Permission>) entry.getValue();
			ArrayList newList = new ArrayList<Permission>();
			for (Permission permission : list) {
				if (permission.getUserLevel().contains(userLevel)) {
					// 一般使用者需要判斷上傳的系統
					if (!userLevel.equals("1")
							&& !permission.getPermissionID().equals("")) {

						if (permission.getPermissionID().equals(systemLevel)) {
							newList.add(permission);
						}

					} else {

						newList.add(permission);
					}
				}
				
			}
			if(newList.size()>0){
				newMap.put(key, newList);
			}
			
		}

		return newMap;

	}

	public void setPermission(Map permission) {
		this.permission = (HashMap) permission;
	}
}
