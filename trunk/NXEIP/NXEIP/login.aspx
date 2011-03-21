<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <%Response.Write(System.Configuration.ConfigurationManager.AppSettings["WebName"]); %>
    </title>
    <link href="style/Green/css/login.css" rel="stylesheet" type="text/css" />
    <link href="style/Green/css/thickbox.css" rel="stylesheet" type="text/css" />
    
    <script src="js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="js/thickbox.js" type="text/javascript"></script>
    <script type="text/JavaScript">
<!--
        function MM_preloadImages() { //v3.0
            var d = document; if (d.images) {
                if (!d.MM_p) d.MM_p = new Array();
                var i, j = d.MM_p.length, a = MM_preloadImages.arguments; for (i = 0; i < a.length; i++)
                    if (a[i].indexOf("#") != 0) { d.MM_p[j] = new Image; d.MM_p[j++].src = a[i]; } 
            }
        }

        function MM_swapImgRestore() { //v3.0
            var i, x, a = document.MM_sr; for (i = 0; a && i < a.length && (x = a[i]) && x.oSrc; i++) x.src = x.oSrc;
        }

        function MM_findObj(n, d) { //v4.01
            var p, i, x; if (!d) d = document; if ((p = n.indexOf("?")) > 0 && parent.frames.length) {
                d = parent.frames[n.substring(p + 1)].document; n = n.substring(0, p);
            }
            if (!(x = d[n]) && d.all) x = d.all[n]; for (i = 0; !x && i < d.forms.length; i++) x = d.forms[i][n];
            for (i = 0; !x && d.layers && i < d.layers.length; i++) x = MM_findObj(n, d.layers[i].document);
            if (!x && d.getElementById) x = d.getElementById(n); return x;
        }

        function MM_swapImage() { //v3.0
            var i, j = 0, x, a = MM_swapImage.arguments; document.MM_sr = new Array; for (i = 0; i < (a.length - 2); i += 3)
                if ((x = MM_findObj(a[i])) != null) { document.MM_sr[j++] = x; if (!x.oSrc) x.oSrc = x.src; x.src = a[i + 2]; }
        }

        function update(msg) {
            tb_remove();
            alert(msg);
        }

        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                //  reapply the thick box stuff
                tb_init('a.thickbox');
            }
        }
//-->
    </script>
</head>
<body onload="MM_preloadImages('image/login-06-1.gif','image/login-07-1.gif')">
    <form id="form1" runat="server">
    <div style="text-align:center">
        
        <br />
        <br />
        <br />
        <br />
        <br />
        
    </div>
    <div class="tableDiv">
    <table width="610" border="0" align="center" cellpadding="18" cellspacing="1" bgcolor="#9C9A9C">
            <tr>
                <td bgcolor="#FFFFFF">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="219">
                                <img src="image/login-04.gif" width="219" height="333" />
                            </td>
                            <td height="125" valign="top" bgcolor="#E7E7E7">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <img src="image/login-02.jpg" width="354" height="125" />
                                        </td>
                                    </tr>
                                </table>
                                <table width="95%" border="0" cellspacing="4" cellpadding="0">
                                    <tr>
                                        <td>
                                            <div align="right">
                                                帳號：&nbsp;<asp:TextBox ID="tbox_id" runat="server" MaxLength="30" Width="150px"></asp:TextBox>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div align="right">
                                                密碼：&nbsp;<asp:TextBox ID="tbox_pw" runat="server" MaxLength="20" TextMode="Password"
                                                    Width="150px"></asp:TextBox>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div align="right">
                                                驗證碼：&nbsp;<asp:TextBox ID="tbox_code" runat="server" Width="83px"></asp:TextBox>
                                                <img src="lib/ValidateCode.ashx" width="63" height="21" align="absmiddle" alt=""
                                                    runat="server" id="img1" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="33">
                                            <table border="0" align="right" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="77">
                                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/image/login-06.gif"
                                                            OnClick="ImageButton1_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/image/login-07.gif" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div align="right">
                                                <a href="./public/applys.aspx?TB_iframe=true&modal=true&width=777" class="thickbox login-a">帳號申請</a><img src="image/login-08.gif" width="19" height="19"
                                                    align="absmiddle" /></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div align="right">
                                                <a href="#" class="login-a">帳號申請進度查詢</a><img src="image/login-08.gif" width="19"
                                                    height="19" align="absmiddle" /></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div align="right">
                                                <a href="./public/200104.aspx" class="login-a">公文附件下載</a><img src="image/login-08.gif" width="19" height="19"
                                                    align="absmiddle" /></div>
                                        </td>
                                    </tr>
                                   

                                </table>
                            </td>
                        </tr>
                    </table>
                    <div style="text-align: right">
                        <a href="login2.aspx" >
                            <img border="0" id="Image1" src="~/image/login_ID_1.jpg" alt="使用自然人憑證登入" runat="server" />
                        </a>
                    </div>
                </td>
            </tr>
        </table>
        </div>
    </form>
</body>
</html>
