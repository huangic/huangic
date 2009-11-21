<%@ page language="java" import="java.util.*" pageEncoding="UTF-8"%>
<%@taglib prefix="s" uri="/struts-tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jstl/core_rt"%>

<html lang="zh-TW">
	<head>
		<title>系統管理∕設定</title>
		<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
		<link type="text/css" rel="stylesheet" href="../css/form.css" />
		<link type="text/css" rel="stylesheet" href="../css/layout.css" />
		<script src="../js/noRightButton.js" language="javascript">&nbsp;</script>
		<script src="../js/upload.js" language="javascript">&nbsp;</script>
		<script src="../js/mootools.js" language="javascript">&nbsp;</script>
		<script src="../js/buttonCheck.js" language="javascript">&nbsp;</script>
		<script src="../js/SS_Popup.js">&nbsp;</script>

		

	</head>
	<body>


		<s:if test="message != null">
			<script language="JavaScript" type="text/JavaScript">
             alert('<c:out value="${message.errorMessage}"/>');
			
			<s:if test="redirect">
			window.open('../index.htm','_top');
			</s:if>
			</script>
		</s:if>




		<div id="FuncName">
			<h1>
				系統管理∕設定
			</h1>
			
		</div>
		<div id="FormName">
			【系統設定】
		</div>
		<form onSubmit="return checkOnSubmit()" name="reg" method="POST"
			id="Form1" action="install/save.do">
			<table cellspacing="0">

				<tr>
					<td class="Label" align="right">
						<span class="Must">*</span>資料庫連線
					</td>
					<td class="whitetablebg">
						<input class="InputText" type="password" id="htx_oldpassword"
							name="dbstring" size="20">
					</td>

				</tr>
				<tr>
					<td class="Label" align="right">
						<span class="Must">*</span>帳號
					</td>
					<td class="whitetablebg">
						<input class="InputText" type="password" id="htx_password"
							name="dbaccount" size="20">
					</td>
				</tr>
				<tr>
					<td class="Label" align="right">
						<span class="Must">*</span>密碼
					</td>
					<td class="whitetablebg">
						<input class="InputText" type="password" id="htx_checkpassword"
							name="dbpassword" size="20">
					</td>
				</tr>
				
				<tr>
					<td class="Label" align="right">
						<span class="Must">*</span>Schema
					</td>
					<td class="whitetablebg">
						<input class="InputText" type="password" id="htx_checkpassword"
							name="dbpassword" size="20">
					</td>
				</tr>
				
				<tr>
					<td class="Label" align="right">
						<span class="Must">*</span>上傳路徑
					</td>
					<td class="whitetablebg">
						<input class="InputText" type="password" id="htx_checkpassword"
							name="upload" size="20">
					</td>
				</tr>
				
				<tr>
					<td class="Label" align="right">
						<span class="Must">*</span>BACKUP路徑
					</td>
					<td class="whitetablebg">
						<input class="InputText" type="password" id="htx_checkpassword"
							name="upload" size="20">
					</td>
				</tr>
				
				<tr>
					<td class="Label" align="right">
						<span class="Must">*</span>LOG路徑
					</td>
					<td class="whitetablebg">
						<input class="InputText" type="password" id="htx_checkpassword"
							name="upload" size="20">
					</td>
				</tr>
				
				<tr>
					<td class="Label" align="right">
						<span class="Must">*</span>轉檔程式網址
					</td>
					<td class="whitetablebg">
						<input class="InputText" type="password" id="htx_checkpassword"
							name="upload" size="20">
					</td>
				</tr>
				<tr>
					<td class="Label" align="right">
						<span class="Must">*</span>轉檔編碼
					</td>
					<td class="whitetablebg">
						<input class="InputText" type="password" id="htx_checkpassword"
							name="upload" size="20">
					</td>
				</tr>
				
				
				

			</table>


			<input class="cbutton" value="存檔" type="submit">
			<input value="清除重填" class="cbutton" type="reset">
		</form>
		<div id="Explain">
			<h1>
				說明
			</h1>
			<ul>
				<li>
					<span class="Must">*</span>為必要欄位
				</li>
			</ul>
		</div>
	</body>
</html>

