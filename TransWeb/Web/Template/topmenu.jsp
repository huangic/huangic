<%@ page language="java" import="java.util.*" pageEncoding="UTF-8"%>
<%@taglib prefix="s" uri="/struts-tags" %>
<%@taglib prefix="c" uri="http://java.sun.com/jstl/core_rt"%>

<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
<link href="css/tiltle.css" rel="stylesheet" type="text/css">
<title></title>
<script language="javascript" src="js/noRightButton.js"></script>
</head>

<body>

<div id="Title" style="">

	<div ><img src="images/logo.png" alt="內政部兒童局資訊轉檔平台"></div>
	<div id="SiteName" >內政部兒童局資訊轉檔平台</div>
	<div id="LogInfo" >
	  登入者:<c:out value="${sessionScope.UserInfo.userInfo.username}"/>(<c:out value="${sessionScope.UserInfo.userInfo.account}"/>)　　單位:<c:out value="${sessionScope.UserInfo.userInfo.dept}"/> (<c:out value="${sessionScope.UserInfo.userRoleName}"/>)
	</div>

	<a href="logout.do" class="Logout" target="_parent">登出</a>
</div>
</body>
</html>

