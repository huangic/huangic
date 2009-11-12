<%@ page language="java" contentType="text/html; charset=UTF8"
	pageEncoding="UTF8"%>
<%@taglib uri="/struts-tags" prefix="s"%>

<html>

<head>
	<title>轉檔平台</title>
	<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
</head>

<frameset rows="60,*" cols="*" frameborder="NO" border="0" framespacing="0">
    <frame name="topFrame" scrolling="NO" noresize src="topmenu.do"
        marginwidth="0" marginheight="0" frameborder="0">
    <frameset cols="167,*" frameborder="NO" border="0" framespacing="0" rows="*" name="f">

    
		<frame name="leftFrame" noresize scrolling="NO" src="function.do"
			marginwidth="0" marginheight="0" frameborder="0" />
	

	  
	   
	
	 
		<frame name="mainFrame" marginwidth="10" marginheight="11" src="Record/record_search.do" />
	
	
	</frameset>
</frameset>

<noframes>
	<body bgcolor="#FFFFFF">
	</body>
</noframes>

</html>
