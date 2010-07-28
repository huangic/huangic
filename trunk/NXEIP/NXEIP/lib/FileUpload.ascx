<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FileUpload.ascx.cs" Inherits="lib_FileUpload" %>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="100px" bgcolor="#EEEEEE" class="a-letter-1">
            <div id="div_pic" runat="server"></div>
        </td>
        <td width="287" bgcolor="#EEEEEE" class="a-letter-1">
            <span class="a-letter-2">照片大小限制為50KB以內，<br>
                解析度建議為70*80 DPI</span><br>
            <asp:FileUpload ID="FileUpload1" runat="server" />
            &nbsp;<asp:Button ID="Button1" runat="server" Text="預覽照片" OnClick="Button1_Click"
                 />
        </td>
    </tr>
</table>

