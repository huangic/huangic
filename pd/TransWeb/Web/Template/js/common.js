	function righttrim(contentstr){
  	  var contenttemp=contentstr;
  	  var iswhile=true;
  	  var temp="";
  	  while (iswhile){
	    temp=contenttemp.substr(contenttemp.length-1,1);
	    if (temp==" "||temp==" "){
	      contenttemp=contenttemp.substr(0,contenttemp.length-1);
              iswhile=true;	}
	    else{
	      return contenttemp;
	    }
          }
        }
        
	function isNumberByNull(pObj,isNull){
	 if(isNull==true){
	   if(righttrim(pObj.value)==""){
	     return false;
	   }
	 }
	 else{
	   if(righttrim(pObj.value)==""){
	     return true;
	   }
	 }
	 var obj = eval(pObj);
	 var strRef = "1234567890";
	 var tempChar;
	 for (i=0;i<obj.value.length;i++) {
	  tempChar= obj.value.substring(i,i+1);
	  if (strRef.indexOf(tempChar,0)==-1) {
	   if(obj.type=="text")
	    obj.focus();
	   return false;
	  }
	 }
	 return true;
	}

	function isNullOrNumber(pObj){
	  return isNumberByNull(pObj,false);
	}
	
	function isNumber(pObj){
	  return isNumberByNull(pObj,true);
	}