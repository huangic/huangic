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
		<div id="Page">

			<display:table name="users" pagesize="30" id="ListTable"
				requestURI="user_search_result.do">
				<display:column property="account" title="帳號" />

				<display:column property="username" title="姓名" />

				<display:column property="dept" title="單位" />

				<display:column title="編輯">
					<a title="修改" href='user_edit.do?userinfo.userid=<c:out value="${ListTable.userid}"/>'>修改</a>
				</display:column>




			</display:table>





			<form name="list" action="#">
				 共 <em>7</em> 筆資料，
	第 <a href="#">1</a> <a href="#">2</a>
					<a href="#">3</a> 頁
			</form>
		</div>
		<form name="reg" id="reg" method="post" action="">
			<table id="ListTable">
				<tbody>

					<tr>
						<th>
							帳號
						</th>
						<th>
							姓名
						</th>
						<th>
							單位
						</th>
						<th>
							編輯
						</th>

					</tr>
					<tr>
						<td>
							system
						</td>
						<td>
							系統管理員
						</td>
						<td>
							系統管理
						</td>
						<td>
							<a href="user_edit.htm">編輯</a>
						</td>
					</tr>
					<tr>
						<td>
							system
						</td>
						<td>
							系統管理員
						</td>
						<td>
							系統管理
						</td>
						<td>
							<a href="user_edit.htm">編輯</a>
						</td>
					</tr>
					<tr>
						<td>
							system
						</td>
						<td>
							系統管理員
						</td>
						<td>
							系統管理
						</td>
						<td>
							<a href="user_edit.htm">編輯</a>
						</td>
					</tr>
					<tr>
						<td>
							system
						</td>
						<td>
							系統管理員
						</td>
						<td>
							系統管理
						</td>
						<td>
							<a href="user_edit.htm">編輯</a>
						</td>
					</tr>
					<tr>
						<td>
							system
						</td>
						<td>
							系統管理員
						</td>
						<td>
							系統管理
						</td>
						<td>
							<a href="user_edit.htm">編輯</a>
						</td>
					</tr>
					<tr>
						<td>
							system
						</td>
						<td>
							系統管理員
						</td>
						<td>
							系統管理
						</td>
						<td>
							<a href="user_edit.htm">編輯</a>
						</td>
					</tr>
					<tr>
						<td>
							system
						</td>
						<td>
							系統管理員
						</td>
						<td>
							系統管理
						</td>
						<td>
							<a href="user_edit.htm">編輯</a>
						</td>
					</tr>

				</tbody>

			</table>
		</form>
	</body>
</html>

