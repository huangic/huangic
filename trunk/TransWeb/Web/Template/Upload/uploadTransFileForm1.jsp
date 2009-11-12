<%@ page language="java" import="java.util.*" pageEncoding="UTF-8"%>
<%@taglib prefix="s" uri="/struts-tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jstl/core_rt"%>

<html lang="zh-TW">
<head>
<title>系統管理∕使用者</title>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<link type="text/css" rel="stylesheet" href="../css/form.css" />
<link type="text/css" rel="stylesheet" href="../css/layout.css" />
<script src="../js/noRightButton.js" language="javascript">&nbsp;</script>
<script src="../js/upload.js" language="javascript">&nbsp;</script>
<script src="../js/mootools.js" language="javascript">&nbsp;</script>
<script src="../js/globals.js" type="text/javascript">&nbsp;</script>

<script src="../js/tabpanel1_class.js" type="text/javascript">&nbsp;</script>
<script src="../js/widgets_class.js" type="text/javascript">&nbsp;</script>

<script src="../js/buttonCheck.js" language="javascript">&nbsp;</script>
<script src="../js/SS_Popup.js">&nbsp;</script>


		<script language="javascript">
			//檢查各欄位內容
			function checkOnSubmit(){
				
	  			if( $('htx_upload').value == '' ){
	  				alert('請選擇上傳檔案');
	  				return false;
	  			}
  		
  
			}
		</script>

</head>
<body>
<div id="FuncName">
<h1>檔案上傳∕早療系統</h1>
<div id="Nav"><a title="回前頁"
	href="Javascript:window.history.back();">回前頁</A></div>
</div>
<div id="FormName">【發展遲緩兒早期療育個案管理 - 上傳界面 】</div>
<form onSubmit="return checkOnSubmit()" name="reg" method="POST"
	id="Form1" action="uploadTransFileSave.do" enctype="multipart/form-data">
<table cellspacing="0">
	<tr>
		<td class="Label" align="right">個案基本資料表
		(檔名:TB_OPENCASE_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file" name="upload" id="up1"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">個案初訪表1
		(檔名:TB_CASEBASICDATA1_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file" name="upload" id="up2"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">個案初訪表2
		(檔名:TB_CASEBASICDATA2_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file" name="upload" id="up3"></input></td>
	</tr>

	<tr>
		<td class="Label" align="right">個案初訪表3
		(檔名:TB_CASEBASICDATA3_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file" name="upload" id="up4"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">個案安置記錄表
		(檔名:TB_CASESERVEPLACE_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file" name="upload" id="up5"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">個案療育記錄表
		(檔名:TB_CASECARE_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file" name="upload" id="up6"></input></td>
	</tr>

	<tr>
		<td class="Label" align="right">個案轉銜轉介報告表
		(檔名:TB_CASETRANSDATA_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file" name="upload" id="up7"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">個案結案表
		(檔名:TB_CASECLOSED_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file" name="upload" id="up8"></input></td>
	</tr>





</table>


<input class="cbutton" value="存檔" type="submit"><input
	value="清除重填" class="cbutton" type="reset"></form>
<div id="Explain">
<h1>說明</h1>
<ul>
	<li><span class="Must">*</span>為必要欄位</li>
</ul>
</div>
</body>
</html>


