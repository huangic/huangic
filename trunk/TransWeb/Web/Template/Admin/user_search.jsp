<%@ page language="java" import="java.util.*" pageEncoding="UTF-8"%>
<%@taglib prefix="s" uri="/struts-tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jstl/core_rt"%>


<html>
<head>
<META http-equiv="Content-Type" content="text/html; charset=UTF-8">
<link type="text/css" rel="stylesheet" href="../css/form.css">
<link type="text/css" rel="stylesheet" href="../css/layout.css">
<link type="text/css" rel="stylesheet" href="../css/setstyle.css">
<script src="js/noRightButton.js" language="javascript">&nbsp;</script>
<script language="javascript" src="../js/DeptTree2.js">
                            var test = '';
</script>
<script language="javascript" src="../js/SS_Popup.js">
                            var test = '';
</script>
<script src="../js/mootools.js" language="javascript">&nbsp;</script>
</head>

<body>
<div id="FuncName">
<h1>系統管理∕使用者</h1>
<div id="Nav"></div>
</div>
<div id="FormName">【帳號查詢】</div>
<form method="post" id="Form1" action="user_search_result.do" name="queryForm">


<table>
	<tr>
		<td class="Label">使用帳號</td>
		<td><input type="text" name="userinfo.account" id="q_userId"></td>
	</tr>
	
	<tr>
		<td class="Label">姓名</td>
		<td><input type="text" name="userinfo.username" id="q_userName"></td>
	</tr>
	<tr>
		<td class="Label">角色</td>
		<td><select name="userinfo.role">
		   <option value="">不指定</option>
							<s:iterator value="userRole.keySet()" id="id">
							    <option value='<s:property  escape="false" value="id"/>'>
								<s:property  escape="false" value="userRole.get(#id)"/>
								</option>
							</option>
							
							
							</s:iterator>
			
		</select></td>

	</tr>
	
	<tr>
		<td class="Label">系統權限</td>
		<td><select name="userinfo.priority">
			<option value="">不指定</option>
			<s:iterator value="permissionRole.keySet()" id="id">
							    <option value='<s:property  escape="false" value="id"/>'>
								<s:property  escape="false" value="permissionRole.get(#id)"/>
								</option>
							</option>
							</s:iterator>
		</select></td>

	</tr>
	
	

</table>
<input class="cbutton" value="送出" type="submit" onclick="">&nbsp;
<input class="cbutton" value="清除重填" type="reset"></form>
<div id="Explain">
<h1>說明</h1>

</div>
</body>
</html>

