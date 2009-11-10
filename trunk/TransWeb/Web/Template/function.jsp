<%@ page language="java" contentType="text/html; charset=UTF8"
	pageEncoding="UTF8"%>
<%@taglib uri="/struts-tags" prefix="s"%>




<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
	<head>
		<title>主選單</title>
		<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
		<link href="css/function.css" rel="stylesheet" type="text/css">

	</head>

	<body>
		<div id="xFunction" style="display: block;">
			<s:iterator value="permissionMap.keySet()" id="id">
				<a title="<s:property  escape="false" value="id"/>" href="#" class="Cat1"><s:property escape="false" value="id"/></a>
				<div class="Cat2">
					<s:iterator value="permissionMap.get(#id)" id="list">
					
					<a title="#" href="<s:property escape="false" value="functionURL"/>" target="mainFrame"><s:property escape="false" value="functionName"/>
					</a>
                      </s:iterator>
				

					



				</div>


			</s:iterator>


		</div>


	</body>
</html>