<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ImageUpload.ascx.cs" Inherits="lib_ImageUpload" %>
<script type="text/javascript" src="/NXEIP/js/lytebox.js"></script>
<table cellpadding="0" cellspacing="0" border="0">
    <tr>
        <td colspan="2">
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <asp:Button ID="btn_preview" runat="server" Text="預覽照片" OnClick="btn_preview_Click" CssClass="b-input" /><br />
            
        </td>
    </tr>
    <tr>
      <td class="a-letter-2">
            檔案類型：<asp:Label ID="lab_pictype" runat="server">jpg</asp:Label>
            <br />
            大小限制：<asp:Label ID="lab_size" runat="server" Text="50"></asp:Label>
                KB<br />
                解析度：<asp:Label 
                ID="lab_width" runat="server" Text="70"></asp:Label>
            *<asp:Label ID="lab_height" runat="server" Text="80"></asp:Label>
            DPI<br />
            </td>
            <td><div id="div_pic" runat="server"></div></td>
    </tr>
</table>
<asp:Label ID="lab_filename" runat="server" Visible="False"></asp:Label>
<asp:Label ID="lab_ext" runat="server" Visible="False"></asp:Label>
<asp:Label ID="lab_path" runat="server" Visible="False"></asp:Label>
<asp:Label ID="lab_checksize" runat="server" Visible="False"></asp:Label>
<asp:Label ID="lab_checkftype" runat="server" Visible="False"></asp:Label>

