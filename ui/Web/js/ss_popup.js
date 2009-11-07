var gcToggle = "#ffff00";
var gcBG = "#CCCCFF";

var yearShift = 0;

function fPopUpCalendarDlg(ctrlobj,ctrlobj1,event)
{
	fPopUpCalendarDlgBase(ctrlobj,ctrlobj1,0,'calendar',event)
}

function fPopUpCalendarDlgFormat(ctrlobj,ctrlobj1,typenum,event)
{
	fPopUpCalendarDlgBase(ctrlobj,ctrlobj1,typenum,'calendar',event)
}

function fPopUpCalendarTimeDlgFormat(ctrlobj,ctrlobj1,typenum,event)
{
	fPopUpCalendarDlgBase(ctrlobj,ctrlobj1,typenum,'calendarTime',event)
}

function fPopUpCalendarDlgBase(ctrlobj,ctrlobj1,typenum,typeName,event)
{
	event = (event)?event:window.event;
	
	//showx = event.screenX - event.offsetX - 4 //- 210 ; // + deltaX;
	//showy = event.screenY - event.offsetY + 18; // + deltaY;
	//newWINwidth = 210 + 4 + 18;
	
	if (event.offsetX == undefined)
	{
	    var evtOffsets = getOffset(event);
		
		showx = event.screenX - evtOffsets.offsetX - 4 
		showy = event.screenY - evtOffsets.offsetY + 18; 
	}
	else
	{
	    showx = event.screenX - event.offsetX - 4 
		showy = event.screenY - event.offsetY + 18; 
	}
	
	var path = location.pathname;
	
	if(path.charAt(0)=='/') path = path.substring(1,path.length);
	path = path.substring(0,path.indexOf('/'));
	//alert(path);
	if( path != 'gipadmin')
		path = '../js/'+typeName+'/CalendarDlg.htm';
	else
		path = '/js/'+typeName+'/CalendarDlg.htm';
	
	retval = window.showModalDialog(path, "", "dialogWidth:197px; dialogHeight:210px; dialogLeft:"+showx+"px; dialogTop:"+showy+"px; status:no; directories:yes;scrollbars:no;Resizable=no; "  );
	
	if( retval != null ){		
		if (typenum == 0){
			ctrlobj.value = retval; 
			
			var dd = retval.split("/");
			var dstr = (parseInt(dd[0])+yearShift) + "/" + dd[1] + "/" + dd[2];
			ctrlobj1.value = dstr;
		}
		else if (typenum == 1){
			if (retval != "")
				ctrlobj.value = retval.split("-")[0] + retval.split("-")[1] + retval.split("-")[2]; 
			else
				ctrlobj.value = retval;
		}	
	}else{
	       	//alert("canceled");
	}
}

function getOffset(evt)
{
  var target = evt.target;
  if (target.offsetLeft == undefined)
  {
    target = target.parentNode;
  }
  var pageCoord = getPageCoord(target);
  var eventCoord =
  { 
    x: window.pageXOffset + evt.clientX,
    y: window.pageYOffset + evt.clientY
  };
  var offset =
  {
    offsetX: eventCoord.x - pageCoord.x,
    offsetY: eventCoord.y - pageCoord.y
  };
  return offset;
}

function getPageCoord(element)
{
  var coord = {x: 0, y: 0};
  while (element)
  {
    coord.x += element.offsetLeft;
    coord.y += element.offsetTop;
    element = element.offsetParent;
  }
  return coord;
}
