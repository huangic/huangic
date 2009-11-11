package idv.trans.model;

public class Message implements Cloneable {
   private String errorMessage;
   private String errorCode;
   private String systemMessage;
   
 public Message(){
	 
 }  
   
public Message(String errorMessage){
	this.errorMessage=errorMessage;
}
   
public String getErrorMessage() {
	return errorMessage;
}
public void setErrorMessage(String errorMessage) {
	this.errorMessage = errorMessage;
}
public String getErrorCode() {
	return errorCode;
}
public void setErrorCode(String errorCode) {
	this.errorCode = errorCode;
}
public String getSystemMessage() {
	return systemMessage;
}
public void setSystemMessage(String systemMessage) {
	this.systemMessage = systemMessage;
}
   
   
   
}
