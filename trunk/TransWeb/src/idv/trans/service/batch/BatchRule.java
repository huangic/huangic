package idv.trans.service.batch;

import java.util.LinkedHashMap;

public class BatchRule {
  
	String batchURL;
	String batchDB;
	String remoteCharset;
	public String getRemoteCharset() {
		return remoteCharset;
	}
	public void setRemoteCharset(String remoteCharset) {
		this.remoteCharset = remoteCharset;
	}
	public String getBatchDB() {
		return batchDB;
	}
	public void setBatchDB(String batchDB) {
		this.batchDB = batchDB;
	}
	public void setBatchPattern(LinkedHashMap<String, String> batchPattern) {
		this.batchPattern = batchPattern;
	}
	LinkedHashMap<String,String> batchPattern;
	
	public String getBatchURL() {
		return batchURL;
	}
	public void setBatchURL(String batchURL) {
		this.batchURL = batchURL;
	}
	public LinkedHashMap getBatchPattern() {
		return batchPattern;
	}
	
	
	
}
