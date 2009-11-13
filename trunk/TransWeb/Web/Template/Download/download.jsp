<%@ page language="java" import="java.util.*" pageEncoding="UTF-8"%>
<%@taglib prefix="s" uri="/struts-tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jstl/core_rt"%>
<%@ taglib uri="http://displaytag.sf.net" prefix="display"%>
<%@ taglib prefix="fn" uri="http://java.sun.com/jsp/jstl/functions" %> 

<html>
<head>
<META http-equiv="Content-Type" content="text/html; charset=UTF-8">
<link type="text/css" rel="stylesheet" href="../css/list.css">
<link type="text/css" rel="stylesheet" href="../css/layout.css">
<link type="text/css" rel="stylesheet" href="../css/setstyle.css">

<script src="js/noRightButton.js" language="javascript">&nbsp;</script>
<script src="js/mootools.js" language="javascript">&nbsp;</script>
<script type="text/javascript">
<!--
function sbar(st){st.style.backgroundColor='#eeeeee';}
function cbar(st){st.style.backgroundColor='';}
//-->
</script>

</head>

<body>
<div id="FuncName">
<h1>下載區∕下載區</h1>
</div>
<div id="FormName">【下載區】 <br/>

</div>
			
			<display:table name="downloads" pagesize="30" id="ListTable"
				requestURI="download.do">
				<display:column property="name" title="檔案名稱" ></display:column>
				<display:column title="系統" >
					<c:set var="key" value="${fn:trim(ListTable.priority)}"/>
					<c:out value="${permissionRole[key]}"/>
				
				</display:column>
				<display:column title="檔案下載">
					<a title="下載" target="_blank" href='../<c:out value="${ListTable.filepath}"/><c:out value="${ListTable.filename}"/>'>下載</a>
				</display:column>
			</display:table>

</body>
</html>



