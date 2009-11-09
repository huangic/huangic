			
			//上傳檔案
			function openFileDialog(contextPath, fieldName, dispFieldName){
				var link = contextPath+ '/gipadmin/GipEdit/UploadFile.jsp?fileColName='+fieldName+'&dispFieldName='+dispFieldName;
				var properties = 'scrollbars=yes,toolbar=no,location=no,directory=no,status=no,menuber=no,width=450,height=150';
				window.open(link,'NewWindows',properties);
			}
			
			//移除上傳檔案
			function removeFile(fieldName, dispFieldName){
			  var input = $(dispFieldName);
			  input.innerHTML = '無檔案';
			  $(fieldName).value='';
			}
			
			//上傳圖檔
			function openImageDialog(contextPath, fieldName, dispFieldName){
				var link = contextPath+ '/gipadmin/GipEdit/UploadImage.jsp?fileColName='+fieldName+'&dispFieldName='+dispFieldName;
				var properties = 'scrollbars=yes,toolbar=no,location=no,directory=no,status=no,menuber=no,width=450,height=150';
				window.open(link,'NewWindows',properties);
			}
			
			//移除圖檔
			function removeImage(fieldName, dispFieldName){
			  var input = $(dispFieldName);
			  input.innerHTML = '無圖片';
			  $(fieldName).value='';
			}	
