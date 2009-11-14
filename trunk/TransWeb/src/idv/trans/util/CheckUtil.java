package idv.trans.util;

public class CheckUtil{
	
	public static boolean isNull(Object o){
		if(o==null){
			return true;
		}else{
			return false;
		}
	}
	
	public static String getNotEmpty(Object o){
		if(o==null){
			return "";
		}else{
			return String.valueOf(o);
		}
	}
}