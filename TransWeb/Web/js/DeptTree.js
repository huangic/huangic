	var ppcIE=((navigator.appName == "Microsoft Internet Explorer") || ((navigator.appName == "Netscape") && (parseInt(navigator.appVersion)==5)));
	
var ppcNN6=((navigator.appName == "Netscape") && (parseInt(navigator.appVersion)==5));
	var ppcNN=((navigator.appName == "Netscape")&&(document.layers));
	
	function openDeptWindow(img,formdeptcode,formdeptname){

		var ppcX;
		var ppcY;
		if ( ppcIE ) {
            
			ppcX = getOffsetLeft(document.images[0]);    
            
			ppcY = getOffsetTop(document.images[0]) + document.images[0].height;
        
		}
else if (ppcNN){
            
			ppcX = document.images[0].x; 
            
			ppcY = document.images[0].y + document.images[0].height;
  
		}
		ppcX+=self.screenLeft;
		ppcY+=self.screenTop;
        //TreeList.do
		var properties = 'scrollbars=yes,toolbar=no,location=no,directory=no,status=no,menuber=no,width=250,height=300,left='+ppcX+',top='+ppcY;
		var link = '../DeptRealTimeEditXdoc.jsp?treeType=gipadmin/xmlspec/tree/ChooseDeptSepc.xml&param1='+ formdeptcode +'&param2='+ formdeptname;
		window.open(link,'Dept',properties);

	}
	
	function getOffsetLeft (el) {
    
		var ol = el.offsetLeft;
    
		while ((el = el.offsetParent) != null)
        
		ol += el.offsetLeft;
    
		return ol;
	
}
	
	function getOffsetTop (el) {
    
		var ot = el.offsetTop;
    
		while((el = el.offsetParent) != null)
        
		ot += el.offsetTop;
    
		return ot;
	}