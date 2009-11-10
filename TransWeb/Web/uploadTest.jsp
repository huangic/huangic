<%@ taglib prefix="s" uri="/struts-tags" %>
<html>
<head>
<title>File Upload Example</title>
<link href="<s:url value="/css/main.css"/>" rel="stylesheet" type="text/css"/>

</head>

<body>

<s:actionerror />
<s:fielderror />
<s:form action="uploadFile.do" method="POST" enctype="multipart/form-data">
<tr>
<td colspan="2"><h1>File Upload Example</h1></td>
</tr>

<s:file name="upload" label="File"/>
<s:textfield name="caption" label="Caption"/>
<s:submit />
</s:form>
</body>
</html>