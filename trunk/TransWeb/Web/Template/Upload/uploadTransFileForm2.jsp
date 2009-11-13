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
				//總個數
				var items = 21;
				var n = new Array(items);
				var reg = new Array(items);
				
				
				n[1] = '1-1.個案開立資料檔(檔名：F01_OPEN_CASE_YYYYMMDD.TXT)';
				reg[1] = /.*F01_OPEN_CASE_\d{8}.[tT][xX][tT]/;
				n[2] = '1-2.個案資料檔(檔名：F01_CHILDREN_CASE_YYYYMMDD.TXT)';
				reg[2] = /.*F01_CHILDREN_CASE_\d{8}.[tT][xX][tT]/;
				n[3] = '1-3.原生家庭－家庭資料檔(檔名：F01_CHILDREN_DATA_YYYYMMDD.TXT)';
				reg[3] = /.*F01_CHILDREN_DATA_\d{8}.[tT][xX][tT]/;
				n[4] = '1-4.原生家庭－家庭成員檔(檔名：F01_CHILDREN_MEMBER_YYYYMMDD.TXT)';
				reg[4] = /.*F01_CHILDREN_MEMBER_\d{8}.[tT][xX][tT]/;
				n[5] = '1-5.個案訪視紀錄檔(檔名：F01_CHILDREN_VISIT_YYYYMMDD.TXT)';
				reg[5] = /.*F01_CHILDREN_VISIT_\d{8}.[tT][xX][tT]/;
				n[6] = '1-6.津貼補助紀錄檔(檔名：F01_CHILDREN_SUBSIDIES_YYYYMMDD.TXT)';
				reg[6] = /.*F01_CHILDREN_SUBSIDIES_\d{8}.[tT][xX][tT]/;
				n[7] = '1-7.安置紀錄檔(檔名：F01_CHILDREN_SETTLE_YYYYMMDD.TXT)';
				reg[7] = /.*F01_CHILDREN_SETTLE_\d{8}.[tT][xX][tT]/;
				n[8] = '1-8.身心狀況紀錄檔(檔名：F01_CHILDREN_MIND_YYYYMMDD.TXT) ';
				reg[8] = /.*F01_CHILDREN_MIND_\d{8}.[tT][xX][tT]/;
				n[9] = '1-9.醫療紀錄檔(檔名：F01_CHILDREN_MEDICAL_YYYYMMDD.TXT)';
				reg[9] = /.*F01_CHILDREN_MEDICAL_\d{8}.[tT][xX][tT]/;
				n[10] = '1-10.服務紀錄檔(檔名：F01_CHILDREN_SERVICE_YYYYMMDD.TXT)';
				reg[10] = /.*F01_CHILDREN_SERVICE_\d{8}.[tT][xX][tT]/;
				
				n[11] = '1-11.費用紀錄檔(檔名：F01_CHILDREN_EXPENSE_YYYYMMDD.TXT)';
				reg[11] = /.*F01_CHILDREN_EXPENSE_\d{8}.[tT][xX][tT]/;
				n[12] = '1-12.家庭聯繫紀錄檔(檔名：F01_CHILDREN_CONTACT_YYYYMMDD.TXT)';
				reg[12] = /.*F01_CHILDREN_CONTACT_\d{8}.[tT][xX][tT]/;
				n[13] = '1-13.年度評估紀錄檔(檔名：F01_CHILDREN_ESTIMATE_YYYYMMDD.TXT)';
				reg[13] = /.*F01_CHILDREN_ESTIMATE_\d{8}.[tT][xX][tT]/;
				n[14] = '1-14.結案資料檔(檔名：F01_CHILDREN_CLOSE_YYYYMMDD.TXT)';
				reg[14] = /.*F01_CHILDREN_CLOSE_\d{8}.[tT][xX][tT]/;
				
				
				n[15] = '2-1.申請基本資料檔(檔名：F01_FAMILY_YYYYMMDD.TXT)';
				reg[15] = /.*F01_FAMILY_\d{8}.[tT][xX][tT]/;
				n[16] = '2-2.寄養家庭-家庭資料檔(檔名：F01_FAMILY_DATA_YYYYMMDD.TXT)';
				reg[16] = /.*F01_FAMILY_DATA_\d{8}.[tT][xX][tT]/;
				n[17] = '2-3.寄養家庭-家庭成員檔(檔名：F01_FAMILY_MEMBER_YYYYMMDD.TXT)';
				reg[17] = /.*F01_FAMILY_MEMBER_\d{8}.[tT][xX][tT]/;
				n[18] = '2-4.訓練紀錄檔(檔名：F01_FAMILY_TRAIN_YYYYMMDD.TXT)';
				reg[18] = /.*F01_FAMILY_TRAIN_\d{8}.[tT][xX][tT]/;
				n[19] = '2-5.訪視紀錄檔(檔名：F01_FAMILY_VISIT_YYYYMMDD.TXT)';
				reg[19] = /.*F01_FAMILY_VISIT_\d{8}.[tT][xX][tT]/;
				n[20] = '2-6.考評表揚紀錄檔(檔名：F01_FAMILY_EXAM_YYYYMMDD.TXT)';
				reg[20] = /.*F01_FAMILY_EXAM_\d{8}.[tT][xX][tT]/;
				n[21] = '2-7.結案資料檔(檔名：F01_FAMILY_CLOSE_YYYYMMDD.TXT)';
				reg[21] = /.*F01_FAMILY_CLOSE_\d{8}.[tT][xX][tT]/;
				
				
				var emptyCount = 0;
				for(var i=1;i<=items;i++){
					if($('up'+i).value == ''){
						emptyCount++;
					}
		  			if($('up'+i).value != '' && !$('up'+i).value.match(reg[i]) ){
	  					alert('【'+n[i]+'】檔案格式錯誤');
	  					return false;
	  				}
  				}
  				
  				if(emptyCount == items){
  					alert('請選擇檔案上傳');
  				}
  				
  		
  
			}
		</script>

</head>
<body>
		<s:if test="message != null">
			<script language="JavaScript" type="text/JavaScript">
             alert('<c:out value="${message.errorMessage}"/>');
			</script>
		</s:if>
		
		
<div id="FuncName">
<h1>檔案上傳∕寄養系統</h1>
<div id="Nav"><a title="回前頁"
	href="Javascript:window.history.back();">回前頁</A></div>
</div>
<div id="FormName">【發展遲緩兒早期療育個案管理 - 上傳界面 】</div>
<form onSubmit="return checkOnSubmit()" name="reg" method="POST"
	id="Form1" action="uploadTransFileSave.do" enctype="multipart/form-data">
<table cellspacing="0">
	<tr>
		<td>1.寄養兒童及少年</td>
	</tr>
	<tr>
		<td class="Label" align="right">1-1.個案開立資料檔(檔名：F01_OPEN_CASE_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file" name="upload" id="up1"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">1-2.個案資料檔(檔名：F01_CHILDREN_CASE_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file" name="upload" id="up2"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">1-3.原生家庭－家庭資料檔(檔名：F01_CHILDREN_DATA_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file" name="upload" id="up3"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">1-4.原生家庭－家庭成員檔(檔名：F01_CHILDREN_MEMBER_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file" name="upload" id="up4"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">1-5.個案訪視紀錄檔(檔名：F01_CHILDREN_VISIT_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file" name="upload" id="up5"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">1-6.津貼補助紀錄檔(檔名：F01_CHILDREN_SUBSIDIES_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file" name="upload" id="up6"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">1-7.安置紀錄檔(檔名：F01_CHILDREN_SETTLE_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file" name="upload" id="up7"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">1-8.身心狀況紀錄檔(檔名：F01_CHILDREN_MIND_YYYYMMDD.TXT) </td>
		<td class="whitetablebg"><input class="InputText" type="file" name="upload" id="up8"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">1-9.醫療紀錄檔(檔名：F01_CHILDREN_MEDICAL_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file" name="upload" id="up9"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">1-10.服務紀錄檔(檔名：F01_CHILDREN_SERVICE_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file" name="upload" id="up10"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">1-11.費用紀錄檔(檔名：F01_CHILDREN_EXPENSE_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file" name="upload" id="up11"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">1-12.家庭聯繫紀錄檔(檔名：F01_CHILDREN_CONTACT_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file" name="upload" id="up12"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">1-13.年度評估紀錄檔(檔名：F01_CHILDREN_ESTIMATE_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file" name="upload" id="up13"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">1-14.結案資料檔(檔名：F01_CHILDREN_CLOSE_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file" name="upload" id="up14"></input></td>
	</tr>
	<tr>
		<td>2.寄養家庭</td>
	</tr>
	<tr>
		<td class="Label" align="right">2-1.申請基本資料檔(檔名：F01_FAMILY_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file" name="upload" id="up15"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">2-2.寄養家庭-家庭資料檔(檔名：F01_FAMILY_DATA_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file" name="upload" id="up16"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">2-3.寄養家庭-家庭成員檔(檔名：F01_FAMILY_MEMBER_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file" name="upload" id="up17"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">2-4.訓練紀錄檔(檔名：F01_FAMILY_TRAIN_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file" name="upload" id="up18"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">2-5.訪視紀錄檔(檔名：F01_FAMILY_VISIT_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file" name="upload" id="up19"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">2-6.考評表揚紀錄檔(檔名：F01_FAMILY_EXAM_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file" name="upload" id="up20"></input></td>
	</tr>
	<tr>
		<td class="Label" align="right">2-7.結案資料檔(檔名：F01_FAMILY_CLOSE_YYYYMMDD.TXT)</td>
		<td class="whitetablebg"><input class="InputText" type="file" name="upload" id="up21"></input></td>
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


