<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="350205.aspx.cs" Inherits="_35_350200_350205" %>

<%@ Register src="../../lib/tree/jQueryDepartTree.ascx" tagname="jQueryDepartTree" tagprefix="uc1" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
  <tr>
    <td><!-- InstanceBeginEditable name="EditRegion3" -->
      <table width="100%" height="500" border="1" cellpadding="0" cellspacing="20">
        <tr>
          <td height="22" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td width="17"><img src="../../image/b01.gif" width="17" height="22" /></td>
                <td background="../../image/b01-1.gif" class="b01">帳號管理 / 人員管理 /<strong>  人事資料建檔<asp:ToolkitScriptManager 
                        ID="ToolkitScriptManager1" runat="server">
                    </asp:ToolkitScriptManager>
&nbsp;<asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    </strong></td>
              </tr>
          </table></td>
        </tr>
        <tr>
          <td valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td width="17"><img src="../../image/b02.gif" width="17" height="29" /></td>
              <td background="../../image/b02-1.gif" class="a02-15"> 人事資料建檔</td>
              <td background="../../image/b02-1.gif"><div align="right"></div></td>
              <td width="17"><img src="../../image/b02-2.gif" width="17" height="29" /></td>
            </tr>
          </table>
            <table width="100%" border="0" cellpadding="3" cellspacing="3" bgcolor="#FFFFFF">
              <tr>
                <td width="100" bgcolor="#eeeeee" class="a-letter-2"><div align="right">
                    <span class="a-letter-Red">* </span>身分證字號
                </div></td>
                <td width="338" bgcolor="#EEEEEE" class="a-letter-1"><input name="textfield222222242" type="text" size="15" /></td>
                <td width="82" bgcolor="#eeeeee" class="a-letter-2"><div align="right"><span class="a-letter-Red">* </span>姓名</div></td>
                <td colspan="2" bgcolor="#EEEEEE" class="a-letter-1"><input name="textfield22222223" type="text" size="10" /></td>
                </tr>
              <tr>
                <td bgcolor="#EEEEEE" class="a-letter-2"><div align="right"><span class="a-letter-Red">* </span>員工帳號</div></td>
                <td bgcolor="#EEEEEE" class="a-letter-1"><input name="textfield22222224" type="text" size="15" /></td>
                <td bgcolor="#EEEEEE" class="a-letter-2"><div align="right"><span class="a-letter-Red">* </span>人事編號</div></td>
                <td colspan="2" bgcolor="#EEEEEE" class="a-letter-1"><input name="textfield2222222" type="text" size="15" /></td>
              </tr>
              <tr>
                <td bgcolor="#EEEEEE" class="a-letter-2"><div align="right"><span class="a-letter-Red">* </span>服務單位</div></td>
                <td bgcolor="#EEEEEE" class="a-letter-1">
                
                    <uc1:jQueryDepartTree ID="jQueryDepartTree1" runat="server" />
                
                </td>
                <td align="right" bgcolor="#EEEEEE" class="a-letter-2">個人照片</td>
                <td width="101" bgcolor="#EEEEEE" class="a-letter-1">&nbsp;</td>
                <td width="287" bgcolor="#EEEEEE" class="a-letter-1"><span class="a-letter-2">照片大小限制為50KB以內，<br>
                  解析度建議為70*80 DPI</span><br><span class="table-b">
                  &nbsp;
                  </span><br></td>
                </tr>
              <tr>
                <td bgcolor="#EEEEEE" class="a-letter-2"><div align="right"><span class="a-letter-Red">* </span>職稱</div></td>
                <td bgcolor="#EEEEEE" class="a-letter-1"><select name="select3">
                  <option>總經理</option>
                </select></td>
                <td bgcolor="#EEEEEE" class="a-letter-2"><div align="right">人員類別</div></td>
                <td colspan="2" bgcolor="#EEEEEE" class="a-letter-1"><select name="select2">
                  <option>正式人員</option>
                </select></td>
              </tr>
              <tr>
                <td bgcolor="#EEEEEE" class="a-letter-2"><div align="right">
                  <div align="right">到職日期</div>
                </div></td>
                <td bgcolor="#EEEEEE" class="a-letter-1"><input name="textfield2" type="text" size="8" />
                  <img src="../../image/day.gif" width="18" height="18" align="absmiddle" />
 (日期格式YY-mm-dd) </td>
                <td bgcolor="#EEEEEE" class="a-letter-2"><div align="right">生日</div></td>
                <td colspan="2" bgcolor="#EEEEEE" class="a-letter-1"><input name="textfield22" type="text" size="8" />
                  <img src="../../image/day.gif" width="18" height="18" align="absmiddle" /> <span class="a-letter-2">(日期格式YY-mm-dd) </span></td>
              </tr>
              <tr>
                <td bgcolor="#EEEEEE" class="a-letter-2"><div align="right">連絡地址</div></td>
                <td bgcolor="#EEEEEE" class="a-letter-1"><input name="textfield222222243" type="text" size="40" /></td>
                <td bgcolor="#EEEEEE" class="a-letter-2"><div align="right">連絡電話</div></td>
                <td colspan="2" bgcolor="#EEEEEE" class="a-letter-1"><input name="textfield" type="text" size="25" /></td>
                </tr>
              <tr>
                <td bgcolor="#EEEEEE" class="a-letter-2"><div align="right">電子郵件</div></td>
                <td bgcolor="#EEEEEE" class="a-letter-1"><input name="textfield2222222432" type="text" size="25" /></td>
                <td bgcolor="#EEEEEE" class="a-letter-2"><div align="right">辦公室電話</div></td>
                <td colspan="2" bgcolor="#EEEEEE" class="a-letter-1"><input name="textfield3" type="text" size="15" />
                  <span class="a-letter-2">分機：</span>                  <input name="textfield4" type="text" size="10" /></td>
                </tr>
            </table>
            <table width="100%" border="0" cellpadding="0" cellspacing="10" bgcolor="#FFFFFF">
              <tr>
                <td><div align="center">
                    <input name="Submit4" type="button" class="b-input" value="確定" />
                  &nbsp;&nbsp;&nbsp;&nbsp;
                  <input name="Submit23" type="button" class="a-input" onclick="MM_goToURL('parent','javascript:close();');return document.MM_returnValue" value="取消" />
                  &nbsp;</div></td>
              </tr>
            </table></td>
        </tr>
      </table>
      <!-- InstanceEndEditable --></td>
  </tr>
</table>
</asp:Content>

