<%@ page language="java" import="java.util.*" pageEncoding="UTF-8"%>
<%@taglib prefix="s" uri="/struts-tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jstl/core_rt"%>

<html lang="zh-TW">
	<head>
		<title>帳號管理∕使用者</title>
		<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
		<link type="text/css" rel="stylesheet" href="../css/form.css" />
		<link type="text/css" rel="stylesheet" href="../css/layout.css" />
		<script src="../js/noRightButton.js" language="javascript">&nbsp;</script>
		<script src="../js/upload.js" language="javascript">&nbsp;</script>
		<script src="../js/mootools.js" language="javascript">&nbsp;</script>
		<script src="../js/buttonCheck.js" language="javascript">&nbsp;</script>
		<script src="../js/SS_Popup.js">&nbsp;</script>

		<script language="javascript">
			//檢查各欄位內容
			function checkOnSubmit(){
		
		
		     //var myRegEnpassword = /^(?=.*[a-zA-Z])(?=.*[0-9]).*$/i;
              
                var pwd=$('htx_password').value
                 
                if(pwd!=''){
                  if(pwd.length<8  ||  pwd.length>20){
                   alert("密碼請設定八碼以上,二十碼以下");
                    return false;
                  }
                 // else if(!myRegEnpassword.test(pwd)){
                 //  alert("密碼請具備英數字混合，大小寫系統均視為不同，請重新設定密碼");
                 //  return false;
                 //}
		 }
		
  			if( $('htx_oldpassword').value == '' ){
  				alert('請輸入 密碼');
  				return false;
  			}
  			
  			if( $('htx_password').value == '' ){
  				alert('請輸入 新密碼');
  				return false;
  			}
  			
  			if( $('htx_checkpassword').value == '' ){
  				alert('請輸入 確認密碼');
  				return false;
  			}
  			
  			
  			
  		
  			if($('htx_password').value != null &&  
			  	$('htx_password').value.length  >20 ){
  				alert('密碼長度不能超過20');
  				return false;
  			}
  			
  			
  			
  			
  			//if( $('htx_oldpassword').value == $('htx_password').value  ){
  			//	alert('新舊密碼不可相同');
  			//	return false;
  			//}
  			
  		
  			if( $('htx_password').value != $('htx_checkpassword').value  ){
  				alert('密碼與確認密碼不同');
  				return false;
  			}
  		
               		
	  		  
  			
  		 
			}
		</script>

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
				帳號管理∕密碼修改
			</h1>
			<div id="Nav">
				<a title="回前頁" href="Javascript:window.history.back();">回前頁</A>
			</div>
		</div>
		<div id="FormName">
			【密碼修改】
		</div>
		<form onSubmit="return checkOnSubmit()" name="reg" method="POST"
			id="Form1" action="user_pwd_save.do">
			<table cellspacing="0">

				<input class="InputText" type="hidden" name="userinfo.userid"
					value='<s:property escape="false" value="#session.UserInfo.userInfo.userid"/>' />


				<tr>
					<td class="Label" align="right">
						<span class="Must">*</span>舊密碼
					</td>
					<td class="whitetablebg">
						<input class="InputText" type="password" id="htx_oldpassword"
							name="oldPassword" size="20">
					</td>

				</tr>
				<tr>
					<td class="Label" align="right">
						<span class="Must">*</span>新密碼
					</td>
					<td class="whitetablebg">
						<input class="InputText" type="password" id="htx_password"
							name="userinfo.password" size="20">
					</td>
				</tr>
				<tr>
					<td class="Label" align="right">
						<span class="Must">*</span> 確認密碼
					</td>
					<td class="whitetablebg">
						<input class="InputText" type="password" id="htx_checkpassword"
							name="checkpassword" size="20">
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

