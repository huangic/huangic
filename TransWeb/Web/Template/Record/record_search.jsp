<%@ page language="java" import="java.util.*" pageEncoding="UTF-8"%>
<%@taglib prefix="s" uri="/struts-tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jstl/core_rt"%>
<%@ taglib prefix="fn" uri="http://java.sun.com/jsp/jstl/functions"%>

<html>
	<head>
		<META http-equiv="Content-Type" content="text/html; charset=UTF-8">
		<link type="text/css" rel="stylesheet" href="../css/form.css">
		<link type="text/css" rel="stylesheet" href="../css/layout.css">
		<link type="text/css" rel="stylesheet" href="../css/setstyle.css">


		<script language="javascript" src="../js/SS_Popup.js">
                            var test = '';
</script>
		<script src="../js/mootools.js" language="javascript">&nbsp;</script>
	</head>

	<body>





		<script language="javascript">
			//檢查各欄位內容
			function checkOnSubmit(){
				
  			var obj=document.getElementsByName("status");
           	var len = obj.length;
       		
       		//alert(len);
       		 var checked = false;

       		 for (i = 0; i < len; i++)
       		 {
           		 if (obj[i].checked == true)
           		 {
              	  checked = true;
             	   break;
          	 	 }
       		 }
            
            //alert(checked);
  		
  		     if(!checked){
  		      alert("請最少選擇一種狀態");
  		       return false;
  		      }
  
			}
		</script>








		<div id="FuncName">
			<h1>
				記錄查詢/轉檔記錄
			</h1>
			<div id="Nav"></div>
		</div>
		<div id="FormName">
			【轉檔記錄查詢】
		</div>
		<form method="post"  onSubmit="return checkOnSubmit()" id="Form1" action="record.do" name="queryForm">

			<table>
				<s:if test="#session.UserInfo.userInfo.role!= 3">
					<tr>
						<td class="Label">
							帳號
						</td>
						<td>
							<input type="text" name="account">
						</td>
					</tr>
				</s:if>

				<s:if test="#session.UserInfo.userInfo.role== 3">
                  <input type="hidden" name="account" value="<c:out value='${sessionScope.userInfo.userinfo.account}'/>"/>
                  
                  
						
					<tr>
						<td class="Label">
							檔案名稱
						</td>
						<td>
							
							
							<input type="text" name="filename">
						</td>
					</tr>
				</s:if>


				<s:if test="#session.UserInfo.userInfo.role!= 3">
					<tr>
						<td class="Label">
							角色
						</td>
						<td>

							<select id="htx_tdataCat" name="role">

								<!--
								<s:if test="#session.UserInfo.userInfo.role== 1">
									<option>
										不指定
									</option>
								</s:if>

                                

								<s:iterator value="userRole.keySet()" id="id">




									<option value='<s:property  escape="false" value="id"/>'>
										<s:property escape="false" value="userRole.get(#id)" />
									</option>
									


								</s:iterator>

                             -->
                             
                             <option value='3'>
										一般使用者
									</option>
                             

							</select>


						</td>

					</tr>

					<tr>
						<td class="Label">
							系統
						</td>
						<td>
							<select id="htx_tdataCat" name="priority">
								<s:if
									test="#session.UserInfo.userInfo.role== 1&& #session.UserInfo.userInfo.priority==0">
									<option>
										不指定
									</option>
								</s:if>
								<s:iterator value="permissionRole.keySet()" id="id">
									<option value='<s:property  escape="false" value="id"/>'>
										<s:property escape="false" value="permissionRole.get(#id)" />
									</option>
									</option>
								</s:iterator>
							</select>

						</td>

					</tr>
				</s:if>
				<tr>
					<td class="Label">
						上傳日期
					</td>
					<td>
						<input type="text" readonly="readonly" size="10" class=""
							name="startDate" id="pcShowq_xpostDate_S">
						<a
							onclick="fPopUpCalendarDlgFormat($('pcShowq_xpostDate_S'),$('q_xpostDate_S'),0,event)"
							onKeypress="fPopUpCalendarDlgFormat($('pcShowq_xpostDate_S'),$('q_xpostDate_S'),0,event)"><img
								align="absmiddle" border="0" name="BTN_date"
								src="../images/icon_cal.gif" width="22" height="17" alt="請選擇上傳日"
								title="請選擇上傳日"> </a> ～
						<input type="text" readonly="readonly" size="10" class=""
							name="endDate" id="pcShowq_xpostDate_E">
						<a
							onclick="fPopUpCalendarDlgFormat($('pcShowq_xpostDate_E'),$('q_xpostDate_E'),0,event)"><img
								align="absmiddle" border="0" name="BTN_date"
								src="../images/icon_cal.gif" width="22" height="17" alt="請選擇上傳日"
								title="請選擇上傳日"> </a>
					</td>

				</tr>

				<tr>
					<td class="Label">
						狀態
					</td>
					<td align="left" class="n">
						<input  type="checkbox" checked="checked" value="1" name="status" />
						待處理
						<input  type="checkbox" checked="checked" value="2" name="status" />
						轉入成功
						<input  type="checkbox" checked="checked" value="3" name="status" />
						轉入失敗
						<input  type="checkbox" checked="checked" value="4" name="status" />
						取消轉入
					</td>

				</tr>







			</table>
			<input class="cbutton" value="送出" type="submit" onclick="">
			&nbsp;
			<input class="cbutton" value="清除重填" type="reset">
		</form>
		<div id="Explain">
			<h1>
				說明
			</h1>

		</div>
	</body>
</html>

