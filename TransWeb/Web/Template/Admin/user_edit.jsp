<%@ page language="java" import="java.util.*" pageEncoding="UTF-8"%>
<%@taglib prefix="s" uri="/struts-tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jstl/core_rt"%>

<html>
	<head>
		<title>帳號管理∕使用者</title>
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

		<script language="javascript">
			//檢查各欄位內容
			function checkOnSubmit(){
				
  			//if( $('htx_userId').value == '' ){
  			//	alert('請輸入 使用帳號');
  			//	return false;
  			//}
  		
  			if($('htx_userId').value != null &&  
			  	$('htx_userId').value.length  >20 ){
  				alert('使用帳號長度不能超過20');
  				return false;
  			}
  		
  			if( $('htx_userName').value == '' ){
  				alert('請輸入 使用者名稱');
  				return false;
  			}
  		
  			if($('htx_userName').value != null &&  
			  	$('htx_userName').value.length  >20 ){
  				alert('使用者名稱長度不能超過20');
  				return false;
  			}
  		
  			//if( $('htx_password').value == '' ){
  			//	alert('請輸入 密碼');
  			//	return false;
  			//}
  			
  			if( $('htx_uid').value == '' ){
  				alert('請輸入身分證字號');
  				return false;
  			}
  			
  			
  		
  			if($('htx_password').value != null &&  
			  	$('htx_password').value.length  >20 ){
  				alert('密碼長度不能超過20');
  				return false;
  			}
  		
  			if( $('htx_password').value != $('htx_checkpassword').value  ){
  				alert('密碼與確認密碼不同');
  				return false;
  			}
  		
                 //var myRegEnpassword = /^(?=.*[a-zA-Z])(?=.*[0-9]).*$/i;
              
                var pwd=$('htx_password').value
                 
                if(pwd!=''){
                  if(pwd.length<8  ||  pwd.length>20){
                    alert("密碼請設定八碼以上,二十碼以下");
                    return false;
                  }
                  }
                 // else if(!myRegEnpassword.test(pwd)){
                //   alert("密碼請具備英數字混合，大小寫系統均視為不同，請重新設定密碼");
                //   return false;
                // }	
	  		
  			if( $('htx_dept').value == '' ){
  				alert('請輸入 單位');
  				return false;
  			}
  		
  			  		
  		    if($('htx_role').value!='1' && $('htx_priority').value=='0'){
  		      alert('非系統管理員，系統權限不可設定為不指定');
  		      return false;
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
			<h1>
				帳號管理/修改使用者
			</h1>
			<div id="Nav">
				<a title="回前頁" href="Javascript:window.history.back();">回前頁</A>
			</div>
		</div>
		<div id="FormName">
			【修改使用者】
		</div>
		<form onSubmit="return checkOnSubmit()" name="reg" method="POST"
			id="Form1" action="user_add_save.do">
			<s:hidden name="userinfo.userid" />


			<table cellspacing="0">



				<tr>
					<td class="Label" align="right">
						<span class="Must">*</span>使用帳號
					</td>
					<td class="whitetablebg">
						<input class="InputText" type="text" id="htx_userId" size="20"
							value="<c:out value='${userinfo.account}'/>" disabled="disable" />

						<s:hidden name="userinfo.account" />
					</td>
				</tr>
				<tr>
					<td class="Label" align="right">
						<span class="Must">*</span>使用者名稱
					</td>
                   <td class="whitetablebg">
					<s:if test="#session.UserInfo.userInfo.role==1">
                         <input class="InputText" type="text" id="htx_userName"
							name="userinfo.username" size="20"
							value="<c:out value='${userinfo.username}'/>">
					</s:if>
					<s:else>
                         <input class="InputText" type="text" id="htx_userName"
							 size="20"
							value="<c:out value='${userinfo.username}'/>"
							
							disabled="disable"
							/>
                         <s:hidden name="userinfo.username" />
                         
                         
                         
					</s:else>
					</td>

					
						
					




				</tr>
				<tr>
					<td class="Label" align="right">
						密碼
					</td>
					<td class="whitetablebg">
						<input class="InputText" type="password" id="htx_password"
							name="userinfo.password" size="20" value="********"/>
					</td>
				</tr>
				<tr>
					<td class="Label" align="right">
						確認密碼
					</td>
					<td class="whitetablebg">
						<input class="InputText" type="password" id="htx_checkpassword"
							name="checkpassword" size="20" value="********">
					</td>
				</tr>
				<tr>

					<td class="Label" align="right">
						<span class="Must">*</span>身分證字號
					</td>
					<td class="whitetablebg">
						
						
						
						
					   
					<s:if test="#session.UserInfo.userInfo.role==1">
                        <input class="InputText" id="htx_uid" name="userinfo.uid"
							onblur="javascript:this.value=this.value.toUpperCase();"
							type="text" value="<c:out value='${userinfo.uid}'/>" />
					
					</s:if>
					<s:else>
                         <input class="InputText" id="htx_tdataCat" 
							type="text" value="<c:out value='${userinfo.uid}'/>" disabled="disableed"/>
					
                         <s:hidden name="userinfo.uid" />
                         
                         
                         
					</s:else>
					
					
					
					</td>
				</tr>
				<tr>
					<td class="Label" align="right">
						<span class="Must">*</span>單位名稱
					</td>
					<td class="whitetablebg">
						<input class="InputText" type="text" id="htx_dept"
							name="userinfo.dept" size="30"
							value="<c:out value='${userinfo.dept}'/>">
					</td>
				</tr>
				<tr>
					<td class="Label" align="right">
						電子信箱
					</td>
					<td class="whitetablebg">
						<input class="InputText" type="text" id="htx_email"
							name="userinfo.email" size="50"
							value="<c:out value='${userinfo.email}'/>">
					</td>
				</tr>
				<tr>
					<td class="Label" align="right">
						聯絡電話
					</td>
					<td class="whitetablebg">
						<input class="InputText" type="text" id="htx_telephone"
							name="userinfo.tel" size="30"
							value="<c:out value='${userinfo.tel}'/>">
					</td>
				</tr>
				<tr>
					<td class="Label" align="right">
						<span class="Must">*</span>角色
					</td>
					<td class="whitetablebg">
						<select id="htx_role" name="userinfo.role">

							<s:iterator value="userRole.keySet()" id="id">
								<option value='<s:property  escape="false" value="id"/>'
									<s:if test="userinfo.role== #id">
								selected
								</s:if>>
									<s:property escape="false" value="userRole.get(#id)" />
								</option>
								


							</s:iterator>

						</select>
					</td>
				</tr>
				<tr>

					<td class="Label" align="right">
						<span class="Must">*</span>系統權限
					</td>
					<td class="whitetablebg">
						<select id="htx_priority" name="userinfo.priority">
							<s:if test="#session.UserInfo.userInfo.priority== 0">
								<option value="0">
									不指定
								</option>
							</s:if>


							<s:iterator value="permissionRole.keySet()" id="id">
								<option value='<s:property  escape="false" value="id"/>'
									<s:if test="userinfo.priority== #id">
								selected
								</s:if>>
									<s:property escape="false" value="permissionRole.get(#id)" />
								</option>
							</s:iterator>
						</select>
					</td>
				</tr>


				<tr>
					<td class="Label" align="right">
						<span class="Must">*</span>啟用/停用
					</td>
					<td class="whitetablebg">
						<select id="htx_tdataCat" name="userinfo.status">
							<option value="1"
								<s:if test="userinfo.status==1">
								selected
								</s:if>>
								啟用
							</option>
							<option value="2"
								<s:if test="userinfo.status== 2">
								selected
								</s:if>>
								停用
							</option>
						</select>
					</td>
				</tr>

				<tr>
					<td class="Label" align="right">
						備註
					</td>
					<td class="whitetablebg">
						<textarea class="InputText" name="userinfo.note" cols="50"
							rows="5"></textarea>
					</td>
				</tr>


			</table>




			<input class="cbutton" value="修改存檔" type="submit">
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

