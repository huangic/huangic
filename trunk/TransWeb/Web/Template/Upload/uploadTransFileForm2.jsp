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


</head>
<body>
<div id="FuncName">
<h1>檔案上傳∕寄養系統</h1>
<div id="Nav"><a title="回前頁"
	href="Javascript:window.history.back();">回前頁</A></div>
</div>
<div id="FormName">【發展遲緩兒早期療育個案管理 - 上傳界面 】</div>
<form onSubmit="return checkOnSubmit()" name="reg" method="POST"
	id="Form1" action="#">
<table cellspacing="0">
	<tr>
		<td>1.寄養兒童及少年</td>
	</tr>
	<tr>
		<td class="Label" align="right">1-1.寄養兒童個案開立檔
		(檔名：F01_OPEN_CASE_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">1-2.寄養兒童個案資料檔 (檔名：F01_
		CHILDREN_CASE_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">1-3.原生家庭－家庭資料檔
		(檔名：F01_CHILDREN_DATA_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file"></input></td>
	</tr>

	<tr>
		<td class="Label" align="right">1-4.原生家庭－家庭成員檔
		(檔名：F01_CHILDREN_MEMBER_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">1-5.個案訪視紀錄檔
		(檔名：F01_CHILDREN_VISIT_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">1-6.津貼補助記錄檔
		(檔名：F01_CHILDREN_SUBSIDIES_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file"></input></td>
	</tr>

	<tr>
		<td class="Label" align="right">1-7.安置記錄檔
		(檔名：F01_CHILDREN_SETTLE_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">1-8.身心狀況記錄檔
		(檔名：F01_CHILDREN_MIND_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file"></input></td>
	</tr>

	<tr>
		<td class="Label" align="right">1-9.醫療記錄檔
		(檔名：F01_CHILDREN_MEDICAL_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">1-10.服務記錄檔
		(檔名：F01_CHILDREN_SERVICE_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file"></input></td>
	</tr>

	<tr>
		<td class="Label" align="right">1-11.寄養費用記錄檔
		(檔名：F01_CHILDREN_EXPENSE_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">1-11.寄養費用記錄檔
		(檔名：F01_CHILDREN_EXPENSE_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">1-12.寄庭聯繫記錄檔
		(檔名：F01_CHILDREN_CONTACT_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">1-13.年度評估記錄檔
		(檔名：F01_CHILDREN_ESTIMATE_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">1-14.結案資料檔
		(檔名：F01_CHILDREN_CLOSE_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file"></input></td>
	</tr>
	<tr>
		<td>2.寄養家庭</td>
	</tr>

	<tr>
		<td class="Label" align="right">2-1.申請基本資料
		(檔名：F01_FAMILY_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">2-2.寄養家庭-家庭資料檔
		(檔名：F01_FAMILY_DATA_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">2-3.寄養家庭-家庭成員檔
		(檔名：F01_FAMILY_MEMBER_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">2-4.訓練紀錄檔
		(檔名：F01_FAMILY_TRAIN_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">2-5.訪視紀錄檔
		(檔名：F01_FAMILY_VISIT_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">2-6.考評表揚紀錄檔
		(檔名：F01_FAMILY_EXAM_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file"></input></td>
	</tr>

	<tr>
		<td class="Label" align="right">2-7.結案資料檔
		(檔名：F01_FAMILY_CLOSE_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file"></input></td>
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


