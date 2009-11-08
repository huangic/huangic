
/**

 * 彈出窗口

 * theURL：窗口的URL位址

 * winName：視窗標題顯示的視窗名稱

 * features：窗口的特性

 */

function MM_openBrWindow(theURL,winName,features) 

{ //v2.0

  window.open(theURL,winName,features);

}





/**

 * 將游標定位于表單(Form)的某個欄

 * name：被定位欄目的ID名稱

 */

function focusto(name)

{

    //document.getElementsByName(name).item(name).focus();

    name.focus();

}





/**

 * 將字元型資料轉化成整型

 * value：被轉化的資料，為字元型

 */

function MyParseInt(value)

{

     if(value=="0")

       return 0;



	var firstchar = value.charAt(0);



	while(firstchar == '0')

	{

		value = value.substr(1);

		firstchar = value.charAt(0);

	}
      

	return parseInt(value);

}





/**

 * 判斷一個字串是否為空

 * value：要判斷的字串

 * name：該字串所在的欄目的ID名稱

 * msg：字串為空時的提示資訊

 */

function HasValue(value,name,msg)

{

  if(value == "")

  {

     focusto(name);

     alert(msg);

     return false;

  }

  

  return true;

}

   



/**

 * 判斷一個字串是否為正數、整數或正整數

 * msg：字串不滿足條件時的提示資訊中的欄目名稱

 * name：該字串所在的欄目的ID名稱

 * value：要判斷的字串

 * bInt：如果為true表示進行整數的判斷；如果為false表示不進行整數的判斷

 * bPositive：如果為true表示進行正數的判斷；如果為false表示不進行正數的判斷

 */

function ValueIsDecimal(msg,name,value,bInt,bPositive)

{

	

       var result = MyParseInt(value);

       var mark = value.indexOf(".");



	if(isNaN(result))

	{

		focusto(name);

		alert(msg+"必須為數位.");

		return false;

	}



	if(bPositive && (result <= 0))

	{

		focusto(name);

		alert(msg+"必須為正數.");

		return false;

	}



	if(bInt && mark >= 0)

	{

		focusto(name);

		alert(msg+"必須為整數.");

		return false;

	}



	return true;

}







/**

 * 向資料庫插入字元時，用於判斷不可以為單引號’

 * name：字串不滿足條件時的提示資訊中的欄目名稱

 * id：該字串所在的欄目的ID名稱

 * value：要判斷的字串

 */

function TextIsValid1(name,id,value)

{

    var offset = value.indexOf("'");



    if(offset >= 0)

    {

		focusto(id);

		alert(name+"不能含有單引號:'");

		return false;

    }



	return true;

}







/**

 * 向資料庫插入字元時，用於判斷不可以為單引號’、雙引號"以及&、#、+、%的符號

 * name：字串不滿足條件時的提示資訊中的欄目名稱

 * id：該字串所在的欄目的ID名稱

 * value：要判斷的字串

 */

function TextIsValid2(name,id,value)

{

    var offset = value.indexOf("'");



    if(offset >= 0)

    {

		focusto(id);

		alert(name+"不能含有單引號:'");

		return false;

    }



  /*  offset = value.indexOf("\"");



    if(offset >= 0)

    {

		focusto(id);

		alert(name+"不能含有雙引號:\"");

		return false;

    }



    offset = value.indexOf("&");



    if(offset >= 0)

    {

		focusto(id);

		alert(name+"不能含有字元:&");

		return false;

    }



    offset = value.indexOf("#");



    if(offset >= 0)

    {

		focusto(id);

		alert(name+"不能含有字元:#");

		return false;

    }



    offset = value.indexOf("+");



    if(offset >= 0)

    {

		focusto(id);

		alert(name+"不能含有字元:+");

		return false;

    }


*/


    offset = value.indexOf("%");



    if(offset >= 0)

    {

		focusto(id);

		alert(name+"不能含有字元:%");

		return false;

    }





	return true;

}







/**

 * 選擇人員時，用於判斷是否選擇了多個人

 * name：字串不滿足條件時的提示資訊中的欄目名稱

 * id：該字串所在的欄目的ID名稱

 * value：要判斷的字串

 */

function JustOneEmp(name,id,value)

{

    var offset = value.indexOf(",");



    if(offset >= 0)

    {

		focusto(id);

		alert(name+"不能選擇多個人");

		return false;			    

    }

    

	return true;

}









/**

 * 判斷日期是否合法

 * name：日所在的欄目的ID名稱

 * year：年

 * month：月

 * day：日

 */

function isValidDate(name,year,month,day)

{

 var nYear = MyParseInt(year);

 var nMonth = MyParseInt(month);

 var nDay = MyParseInt(day);

 if(nMonth==4 | nMonth==6 | nMonth==9 | nMonth==11)

 {

		if(nDay > 30)

		{

			focusto(name);

			alert("沒有這個日期.");

			return false;

		}

 }





if(nMonth==2)

  {

    if((nYear%4)==0) // 閏年

    {

      if(nDay > 29)

		  {

			  focusto(name);

			  alert("沒有這個日期.");

			  return false;

      }

    }

    else

    {

      if(nDay > 28)

		  {

			  focusto(name);

			  alert("沒有這個日期.");

			  return false;

		  }

    }

  }



	return true;

}







/**

 * 判斷開始日期一定在截止日期之前

 * startyear：開始年

 * startmonth：開始月

 * startday：開始日

 * endyear：截止年

 * endmonth：截止月

 * endday：截止日

 */

function isValidDateScale(startyear,startmonth,startday,endyear,endmonth,endday)

{

   var nStartYear = MyParseInt(startyear);

   var nStartMonth = MyParseInt(startmonth);

   var nStartDay = MyParseInt(startday);

   var nEndYear = MyParseInt(endyear);

   var nEndMonth = MyParseInt(endmonth);

   var nEndDay = MyParseInt(endday);



  if(nStartYear>nEndYear){

    alert("起始日期晚於截止日期.");

    return false;

  }

  if(nStartYear==nEndYear){

    if(nStartMonth>nEndMonth){

      alert("起始日期晚於截止日期.");

      return false;

    }

    if(nStartMonth==nEndMonth){

      if(nStartDay>nEndDay){

        alert("起始日期晚於截止日期.");

        return false;

      }

    }

  }

return true;

}







/**

 * 判斷開始日期一定在截止日期之前

 * startdate：開始日期，格式必須為2002-03-22

 * enddate：截止日期，格式必須為2002-03-22

 */

function isValidDateScaleNew(startdate,enddate)

{

   var startyear = startdate.substring(0,4);

   var startmonth = startdate.substring(5,7);

   var startday = startdate.substring(8,10);

   var endyear = enddate.substring(0,4);

   var endmonth = enddate.substring(5,7);

   var endday = enddate.substring(8,10);



   var nStartYear = MyParseInt(startyear);

   var nStartMonth = MyParseInt(startmonth);

   var nStartDay = MyParseInt(startday);

   var nEndYear = MyParseInt(endyear);

   var nEndMonth = MyParseInt(endmonth);

   var nEndDay = MyParseInt(endday);



  if(nStartYear>nEndYear){

    alert("起始日期晚於截止日期.");

    return false;

  }

  if(nStartYear==nEndYear){

    if(nStartMonth>nEndMonth){

      alert("起始日期晚於截止日期.");

      return false;

    }

    if(nStartMonth==nEndMonth){

      if(nStartDay>nEndDay){

        alert("起始日期晚於截止日期.");

        return false;

      }

    }

  }

return true;

}







/**

 * 刪除之前要進行確認

 */

function Del_Confirm()

{

   var x=window.confirm("確定要刪除嗎！");

   return x;

}



/**

 *調用日曆所需的空函數

 */

function runNull(){

}



/**

 *AREATEXT判斷長度

 * msg：字串不滿足條件時的提示資訊中的欄目名稱

 * name：該字串所在的欄目的ID名稱

 * value：要判斷的字串

 * num:限制的字元數

 */

 function checkLength(value,num,name,msg)

 {

      if(value.length>num)

      {

      	alert(msg);

      	focusto(name);

      	return false;

       }

       return true;

 }

  function checkId( value,msg )
  {
  	var ret=true;
    if( value != null )
	{
		var str="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
		for( var i=0;i<value.length;i++)
		{
			if( str.indexOf(value.charAt(i))<0)
			{
				ret=false;
				break;
			}
		}
	}
	if( ret==false )
	   alert( msg+"只能是字母和數位");
	return ret;
  }