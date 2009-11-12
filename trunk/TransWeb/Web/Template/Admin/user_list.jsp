<%@ page language="java" import="java.util.*" pageEncoding="UTF-8"%>
<%@taglib prefix="s" uri="/struts-tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jstl/core_rt"%>
<%@ taglib uri="http://displaytag.sf.net" prefix="display"%>


<html>
	<head>
		<META http-equiv="Content-Type" content="text/html; charset=UTF-8">
		<link type="text/css" rel="stylesheet" href="../css/list.css">
		<link type="text/css" rel="stylesheet" href="../css/layout.css">
		<link type="text/css" rel="stylesheet" href="../css/setstyle.css">

		<script src="js/noRightButton.js" language="javascript">&nbsp;</script>
		<script src="js/mootools.js" language="javascript">&nbsp;</script>

	</head>

	<body>
		<div id="FuncName">
			<h1>
				系統管理∕權限群組
			</h1>
			<div id="Nav">
				<a href="user_search.do" target="">重設查詢</a> &nbsp;
			</div>
		</div>
		<div id="FormName">
			【使用者列表】
			<br>

		</div>
		

			<display:table name="users" pagesize="30" id="ListTable"
				requestURI="user_search_result.do">
				<display:column property="account" title="帳號" />

				<display:column property="username" title="姓名" />

				<display:column property="dept" title="單位" />

				<display:column title="編輯">
					<a title="修改" href='user_edit.do?userinfo.userid=<c:out value="${ListTable.userid}"/>'>修改</a>
				</display:column>




			</display:table>





		
	</body>
</html>

