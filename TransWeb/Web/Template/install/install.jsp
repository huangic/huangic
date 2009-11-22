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
				<div id="Nav">
				<a title="回登入頁" href="../index.do">回登入頁</A>
			</div>
		</div>
		<div id="FormName">
			【系統設定】
		</div>
		<form name="reg" method="POST"
			id="Form1" action="save.do">
			<table cellspacing="0">

				<tr>
					<td class="Label" align="right">
						<span class="Must">*</span>資料庫連線
					</td>
					<td class="whitetablebg">
						<input class="InputText" type="text" id="htx_oldpassword"
							name="dbstring" size="50" value='<s:property escape="false" value="dbstring"/>'/>
					</td>

				</tr>
				<tr>
					<td class="Label" align="right">
						<span class="Must">*</span>帳號
					</td>
					<td class="whitetablebg">
						<input class="InputText" type="text" id="htx_password"
							name="dbaccount" size="50" value='<s:property escape="false" value="dbaccount"/>'>
					</td>
				</tr>
				<tr>
					<td class="Label" align="right">
						<span class="Must">*</span>密碼
					</td>
					<td class="whitetablebg">
						<input class="InputText" type="text" id="htx_checkpassword"
							name="dbpassword" size="50" value='<s:property escape="false" value="dbpassword"/>'>
					</td>
				</tr>
				
				<tr>
					<td class="Label" align="right">
						<span class="Must">*</span>Schema
					</td>
					<td class="whitetablebg">
						<input class="InputText" type="text" id="htx_checkpassword"
							name="dbschema" size="50" value='<s:property escape="false" value="dbschema"/>'>
					</td>
				</tr>
				
				<tr>
					<td class="Label" align="right">
						<span class="Must">*</span>建立初始資料庫
					</td>
					<td class="whitetablebg">
						<input class="InputText" type="checkbox" id="htx_checkpassword"
							name="createdb" size="50" value='true'>
					</td>
				</tr>
				
				
				<tr>
					<td class="Label" align="right">
						<span class="Must">*</span>上傳路徑
					</td>
					<td class="whitetablebg">
						<input class="InputText" type="text" id="htx_checkpassword"
							name="uploadpath" size="50" value='<s:property escape="false" value="uploadpath"/>'>
					</td>
				</tr>
				
				<tr>
					<td class="Label" align="right">
						<span class="Must">*</span>BACKUP路徑
					</td>
					<td class="whitetablebg">
						<input class="InputText" type="text" id="htx_checkpassword"
							name="backuppath" size="50" value='<s:property escape="false" value="backuppath"/>'>
					</td>
				</tr>
				
				<tr>
					<td class="Label" align="right">
						<span class="Must">*</span>LOG路徑
					</td>
					<td class="whitetablebg">
						<input class="InputText" type="text" id="htx_checkpassword"
							name="logpath" size="50" value='<s:property escape="false" value="logpath"/>'>
					</td>
				</tr>
				
				<tr>
					<td class="Label" align="right">
						<span class="Must">*</span>轉檔程式網址
					</td>
					<td class="whitetablebg">
						<input class="InputText" type="text" id="htx_checkpassword"
							name="batchurl" size="50" value='<s:property escape="false" value="batchurl"/>'>
					</td>
				</tr>
				<tr>
					<td class="Label" align="right">
						<span class="Must">*</span>轉檔編碼
					</td>
					<td class="whitetablebg">
						<input class="InputText" type="text" id="htx_checkpassword"
							name="batchcharset" size="50" value='<s:property escape="false" value="batchcharset"/>'>
					</td>
				</tr>
				
					
					
						
					
				

			</table>
              
             

			<input class="cbutton" value="存檔" type="submit">
			<input value="清除重填" class="cbutton" type="reset">
		</form>
		<div id="Explain">
			 <s:property escape="false" value="executemessage"/>
		</div>
	</body>
</html>

