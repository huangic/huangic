<%@ page language="java" import="java.util.*" pageEncoding="UTF-8"%>
<%@taglib prefix="s" uri="/struts-tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jstl/core_rt"%>

<html>
	<head>
		<title>系統管理∕使用者</title>
		<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
		<link type="text/css" rel="stylesheet" href="../css/form.css" />
		<link type="text/css" rel="stylesheet" href="../css/layout.css" />
		<script src="../js/upload.js" language="javascript">&nbsp;</script>
		<script src="../js/mootools.js" language="javascript">&nbsp;</script>
		<script src="../js/globals.js" type="text/javascript">&nbsp;</script>

		<script src="../js/tabpanel1_class.js" type="text/javascript">&nbsp;</script>
		<script src="../js/widgets_class.js" type="text/javascript">&nbsp;</script>

		<script src="../js/buttonCheck.js" language="javascript">&nbsp;</script>
		<script src="../js/SS_Popup.js">&nbsp;</script>

		<script language="javascript"><%--
			//檢查各欄位內容
			function checkOnSubmit(){
				
  			if( $('htx_upload').value == '' ){
  				alert('請選擇上傳檔案');
  				return false;
  			}
  			if( $('htx_name').value == '' ){
  				alert('請輸入 檔案名稱');
  				return false;
  			}
  			
  			if($('htx_name').value != null &&  
			  	$('htx_deptId').value.length  >50 ){
  				alert('檔案名稱長度不能超過50');
  				return false;
  			}
  		
  
			}
		--%></script>

	</head>
	<body>
		<s:if test="message != null">
			<script language="JavaScript" type="text/JavaScript">
             alert('<c:out value="${message.errorMessage}"/>');
			</script>
		</s:if>



		<div id="FuncName">
			<h1>
				系統管理∕使用者
			</h1>
			<div id="Nav">
				<a title="回前頁" href="Javascript:window.history.back();">回前頁</A>
			</div>
		</div>
		<div id="FormName">
			【新增使用者】
		</div>
		<form onSubmit="return checkOnSubmit()" name="reg" id="Form1"
			action="uploadFile.do" method="POST" enctype="multipart/form-data">


			<table cellspacing="0">



				<tr>
					<td class="Label" align="right">
						<span class="Must">*</span>檔案上傳
					</td>
					<td class="whitetablebg">
						<input class="InputText" type="file" id="htx_upload" value="" name="upload"/>
					</td>
				</tr>
				<tr>
					<td class="Label" align="right">
						<span class="Must">*</span>檔案名稱
					</td>
					<td class="whitetablebg">
						<input class="InputText" type="text" id="htx_name"
							name="name" size="20"
							value="">
					</td>

				</tr>
				<tr>
					<td class="Label" align="right">
						<span class="Must">*</span>系統權限
					</td>
					<td class="whitetablebg">
						<select id="htx_tdataCat" name="priority">
							<s:iterator value="permissionRole.keySet()" id="id">
								<option value='<s:property  escape="false" value="id"/>'>
									<s:property escape="false" value="permissionRole.get(#id)" />
								</option>
							</s:iterator>
						</select>
					</td>
				</tr>

			</table>




			<input class="cbutton" value="新增存檔" type="submit">
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

