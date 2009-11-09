<%@ page language="java" import="java.util.*" pageEncoding="BIG5"%>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
<title>歡迎登入內政部兒童局資訊轉檔平台管理系統</title>
<script>
  
  function Strreplace(strObj)
  {
     while( strObj.indexOf("'")>-1 )  strObj=strObj.replace("'","");    
     return strObj;
  }
  
  function fnOk()
  {
	 if(  xForm.account.value=='' )
     {
        alert("請輸入帳號!!");   
        xForm.account.focus();      
        return false;
     }
     
  }
  
  
  function fnClr()
  {
     xForm.x_account.value='';
     xForm.x_passwd.value='';
     xForm.account.value='';
     xForm.passwd.value='';
     xForm.account.focus();      
  }
  function fnEnter(){
     //if(event.keyCode==13) {
    //    fnOk();
     //}
  }

</script>


<style type="text/css">
<!--
body {
	background-color: #fff789;
	margin-left: 0%;
	margin-top: 50px;
	margin-right: 0%;
	margin-bottom: 50px;
}
table {
 border:
}
.fonts1 {
	color: #FFF;
}
-->
</style>

<link href="css/gip.css" rel="stylesheet" type="text/css" />

<script language="JavaScript" type="text/JavaScript">
<!--
function MM_openBrWindow(theURL,winName,features) { //v2.0
  window.open(theURL,winName,features);
}
-->
</script>

<script language="javascript" src="js/noRightButton.js"></script>
</head>

<body>
<form  name="xForm" onsubmit="return fnOk()" method="post" action="login.do">
<div align="center">
  <table cellspacing="0" cellpadding="0" border="0">
    <tbody><tr> 
      <td colspan="3"><img height="222" width="780" src="images/login-1.jpg"/></td>
    </tr>
    <tr> 
      <td><img height="92" width="303" src="images/login-2.jpg"/></td>
      <td bgcolor="#fffde3">
        <table border="0" width="100%">
          <tbody><tr> 
            <td align="right" width="17%"><font size="2" color="#ff6633">帳號</font></td>
            <td width="49%"> 
                <input type="text" maxlength="20" onblur="javascript:this.value=this.value.toUpperCase();" size="20" style="border: 1px solid rgb(255, 168, 0); background-color: rgb(255, 255, 255); font-family: Arial;" id="machine_no" name="account"/>
            </td>
            <td width="34%"></td>
          </tr>
          <tr> 
            <td align="right" width="17%"><font size="2" color="#ff6633">密碼</font></td>
            <td width="49%">
              <input type="password" maxlength="20" size="20" style="border: 1px solid rgb(255, 168, 0); background-color: rgb(255, 255, 255); font-family: Arial;" onkeydown="fnEnter();" id="machine_no" name="passwd"/>
            </td>
            <td width="34%">
              <div align="center"><input style="width:63px;background-color:transparent;background-image: url(images/login-i.gif)" type="submit" value="" title="登入" /></div>
            </td>
          </tr>
          <tr> 
            <td colspan="3"><font size="2" color="#ff6633"></font></td>
          </tr>
        </tbody></table>
      </td>
      <td><img height="92" width="173" src="images/login-6.jpg"/></td>
    </tr>
    <tr> 
      <td><img height="158" width="303" src="images/login-3.jpg"/></td>
      <td><img height="158" width="304" src="images/login-4.jpg"/></td>
      <td><img height="158" width="173" src="images/login-5.jpg"/></td>
    </tr>
  </tbody></table>
  
</div>
</form>

</body>
</html>