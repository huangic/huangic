package idv.trans.model;

public class Message implements Cloneable {
   private String ErrorMessage;
   private String ErrorCode;
   private String SystemMessage;
public String getErrorMessage() {
	return ErrorMessage;
}
public void setErrorMessage(String errorMessage) {
	ErrorMessage = errorMessage;
}
public String getErrorCode() {
	return ErrorCode;
}
public void setErrorCode(String errorCode) {
	ErrorCode = errorCode;
}
public String getSystemMessage() {
	return SystemMessage;
}
public void setSystemMessage(String systemMessage) {
	SystemMessage = systemMessage;
}
   
   
   
}
