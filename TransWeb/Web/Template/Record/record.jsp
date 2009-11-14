<%@ page language="java" import="java.util.*" pageEncoding="UTF-8"%>
<%@taglib prefix="s" uri="/struts-tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jstl/core_rt"%>
<%@ taglib uri="http://displaytag.sf.net" prefix="display"%>
<%@ taglib prefix="fmt" uri="http://java.sun.com/jsp/jstl/fmt" %> 


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
		<s:if test="message != null">
            <script language="JavaScript" type="text/JavaScript">
             alert('<c:out value="${message.errorMessage}"/>');
			</script>
        </s:if>
		
		
		
		<div id="FuncName">
			<h1>
				記錄查詢∕轉檔記錄
			</h1>
			<div id="Nav">
				<a href="record_export.do" target="_blank">匯出cvs</a>
				<a href="record_search.do" >重設查詢</a> &nbsp;
			</div>
		</div>
		<div id="FormName">
			【轉檔記錄】
			<br>

		</div>

		<display:table name="records" pagesize="30" id="ListTable"
			requestURI="record.do" >
					<display:column title="取消">
				<c:if test="${ListTable.status==1}">
                  <a title="取消轉入"
				      href='record_cancel.do?fileid=<c:out value="${ListTable.fileid}"/>'>修改</a>
			         
				</c:if>
				<c:if test="${ListTable.status!=1}">
                  &nbsp;
				</c:if>
				
					</display:column>



			<display:column title="檔名">
				<c:out escapeXml="false" value="${ListTable.filename}" default="&nbsp;" />


			</display:column>

			<display:column  title="上傳時間">
				
				<c:if test="${ListTable.uploaddate!=null}">
				
				<fmt:formatDate value="${ListTable.uploaddate}" type="both"/> 
				
				</c:if>
				<c:if test="${ListTable.uploaddate==null}">
				&nbsp; 
				</c:if>
				
				
				
				
				
			</display:column>

			<display:column  title="上傳人員" >
			<c:out escapeXml="false" value='${ListTable.uploaduser.username}'
				default="&nbsp;" />
          </display:column>

			<display:column  title="處理時間">
				
				<c:if test="${ListTable.transdate!=null}">
				
				<fmt:formatDate value="${ListTable.transdate}" type="both"/> 
				
				</c:if>
				<c:if test="${ListTable.transdate==null}">
				&nbsp; 
				</c:if>
				
				
			</display:column>
			<display:column  title="狀態">
				
				<c:if test="${ListTable.status==1}">
                  <c:set var="statusName" value="待處理"/>                
				</c:if>
				<c:if test="${ListTable.status==2}">
                  <c:set var="statusName" value="轉入成功"/>                
				  
				
				
				
				</c:if>
				<c:if test="${ListTable.status==3}">
                  <c:set var="statusName" value="轉入失敗"/>                
				</c:if>
				<c:if test="${ListTable.status==4}">
                  <c:set var="statusName" value="取消轉入"/>                
				</c:if>
				
				
				
				<c:out escapeXml="false" value='${statusName}'
					default="&nbsp;" />
					<c:remove var="statusName"/>  
			</display:column>
			<display:column  title="總筆數">
				<c:out escapeXml="false" value='${ListTable.allnum}'
					default="&nbsp;" />
			</display:column>
			<display:column  title="匯入筆數">
				<c:out escapeXml="false" value='${ListTable.successnum}'
					default="&nbsp;" />
			</display:column>
			<display:column  title="失敗筆數">
				<c:out escapeXml="false" value='${ListTable.errornum}'
					default="&nbsp;" />
			</display:column>
			<display:column  title="單位">
				<c:out escapeXml="false" value='${ListTable.uploaduser.dept}'
					default="&nbsp;" />
			</display:column>
		</display:table>




	
	</body>
</html>

