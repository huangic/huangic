<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login2.aspx.cs" Inherits="login2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <%Response.Write(System.Configuration.ConfigurationManager.AppSettings["WebName"]); %>
    </title>
    <link href="./style/Default/css/login.css" rel="stylesheet" type="text/css" />
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
        function startCheckCert() {
            var iCardType = '1';

            var errorMsg = "";
            var testPath = "C:\\odxc\\gcaCrypto\\";
            var sourceFile = testPath + "testFunction.htm";
            var p7SignFile = sourceFile + ".sign";
            var p7EnvFile = sourceFile + ".env";
            var rEncPubCert = testPath + "rEncPubCert.cer";
            var sourceFileRe = testPath + "testFunctionRe.htm";
            var sSignPubCert = testPath + "sSignPubCert.cer";
            var gcaCrypto = null;
            try {
                var errCode = 0;
                var nextStep = 0;
                gcaCrypto = new ActiveXObject("GCA12CryptoCom.GCA12Com.1");

                //*
                if (nextStep >= 0) {
                    var version = gcaCrypto.GetVersion();
                    if (gcaCrypto.GetErrorCode() != 0) {
                        nextStep = -1;
                        errorMsg += "\n GetVersion 失敗, code: " + gcaCrypto.GetErrorCode() + ", msg: " + gcaCrypto.GetErrorMsg();
                    } else {
                        nextStep++;
                        errorMsg += "\n GetVersion 成功: version: " + version;
                    }
                }
                //*/
                //*
                if (nextStep >= 0) {
                    errCode = gcaCrypto.SetCardType(iCardType);
                    if (gcaCrypto.GetErrorCode() != 0) {
                        nextStep = -1;
                        errorMsg += "\n SetCardType 失敗, code: " + gcaCrypto.GetErrorCode() + ", msg: " + gcaCrypto.GetErrorMsg();
                    } else {
                        nextStep++;
                        errorMsg += "\n SetCardType 成功: cardType " + iCardType;
                    }
                }
                var cardType = 0;
                if (nextStep >= 0) {
                    cardType = gcaCrypto.GetCardType();
                    if (gcaCrypto.GetErrorCode() != 0) {
                        nextStep = -1;
                        errorMsg += "\n GetCardType 失敗, code: " + gcaCrypto.GetErrorCode() + ", msg: " + gcaCrypto.GetErrorMsg();
                    } else {
                        nextStep++;
                        errorMsg += "\n GetCardType 成功: cardType " + cardType;
                    }
                }
                //*/
                //*
                if (nextStep >= 0) {
                    var pinNum = document.forms[0].tbox_pin.value;
                    var unitCode = gcaCrypto.Login(pinNum);
                    if (gcaCrypto.GetErrorCode() != 0) {
                        nextStep = -1;
                        errorMsg += "\n Login 失敗, code: " + gcaCrypto.GetErrorCode() + ", msg: " + gcaCrypto.GetErrorMsg();
                    } else {
                        nextStep++;
                        errorMsg += "\n Login 成功!!";
                    }
                }
                //*/
                //*
                if ((cardType == 1) && (nextStep >= 0)) {
                    var certOid = gcaCrypto.GetCertOID();
                    if (gcaCrypto.GetErrorCode() != 0) {
                        nextStep = -1;
                        errorMsg += "\n GetCertOID, code: 失敗, code: " + gcaCrypto.GetErrorCode() + ", msg: " + gcaCrypto.GetErrorMsg();
                    } else {
                        nextStep++;
                        errorMsg += "\n GetCertOID 成功: certOid " + certOid;
                        if (certOid == "2.16.886.1.100.3.1.1") {
                            errorMsg += "(此為新版自然人憑證)";
                        } else if (certOid == "2.16.886.1.100.3.2.1.1") {
                            errorMsg += "(此為新版政府機關憑證)";
                        } else if (certOid == "2.16.886.1.100.3.2.1.2") {
                            errorMsg += "(此為新版政府單位憑證)";
                        } else if (certOid == "2.16.886.1.100.3.2.2.1.1") {
                            errorMsg += "(此為新版公司憑證)";
                        } else if (certOid == "2.16.886.1.100.3.2.3.3.1") {
                            errorMsg += "(此為新版分公司憑證)";
                        } else if (certOid == "2.16.886.1.100.3.2.3.1") {
                            errorMsg += "(此為新版行號憑證)";
                        } else if (certOid == "2.16.886.1.100.3.3.1") {
                            errorMsg += "(此為新版伺服器憑證)";
                        }
                    }
                }

                if (nextStep >= 0) {
                    var unitId = gcaCrypto.GetUnitID();
                    if (gcaCrypto.GetErrorCode() != 0) {
                        nextStep = -1;
                        errorMsg += "\n GetUnit失敗, code: 失敗, code: " + gcaCrypto.GetErrorCode() + ", msg: " + gcaCrypto.GetErrorMsg();
                    } else {
                        nextStep++;
                        switch (cardType) {
                            case 0: errorMsg += "\n GetUnitID(GCA 4.0): 成功: unitcode: " + unitId; break;
                            case 1: errorMsg += "\n GetUnitID(GCA 5.0): 成功: unitOID: " + unitId; break;
                        }
                    }
                }

                if (nextStep >= 0) {
                    var serialNum = gcaCrypto.GetSerialNumber();
                    if (gcaCrypto.GetErrorCode() != 0) {
                        nextStep = -1;
                        errorMsg += "\n GetSerialNumber 失敗, code: " + gcaCrypto.GetErrorCode() + ", msg: " + gcaCrypto.GetErrorMsg();
                    } else {
                        nextStep++;
                        errorMsg += "\n GetSerialNumber: 成功: serialNum " + serialNum;
                        //document.forms[0].serialNumber.value = serialNum;
                    }
                }
                //*/
                //*
                if (nextStep >= 0) {
                    errCode = gcaCrypto.MakeSignature(
				sourceFile,   // in, 本文
				p7SignFile); // out, P7 簽章檔
                    if (gcaCrypto.GetErrorCode() != 0) {
                        nextStep = -1;
                        errorMsg += "\n MakeSignuature 失敗, code: " + gcaCrypto.GetErrorCode() + ", msg: " + gcaCrypto.GetErrorMsg();
                    } else {
                        nextStep++;
                        errorMsg += "\n MakeSignuature 成功!!";
                    }
                }
                //*/
                //*
                if (nextStep >= 0) {
                    errCode = gcaCrypto.VerifySignature(
				sourceFile,   // in, 本文
				p7SignFile); // in, P7 簽章檔
                    if (gcaCrypto.GetErrorCode() != 0) {
                        nextStep = -1;
                        errorMsg += "\n VerifySignature 失敗, code: " + gcaCrypto.GetErrorCode() + ", msg: " + gcaCrypto.GetErrorMsg();
                    } else {
                        nextStep++;
                        errorMsg += "\n VerifySignature 成功!!";
                    }
                }
                //*/
                //*
                if (nextStep >= 0) {
                    errCode = gcaCrypto.SaveEncPubCert(rEncPubCert);
                    if (gcaCrypto.GetErrorCode() != 0) {
                        nextStep = -1;
                        errorMsg += "\n SaveEncPubCert 失敗, code: " + gcaCrypto.GetErrorCode() + ", msg: " + gcaCrypto.GetErrorMsg();
                    } else {
                        nextStep++;
                        errorMsg += "\n SaveEncPubCert 成功!!";
                    }
                }
                //*/
                //*
                if (nextStep >= 0) {
                    errCode = gcaCrypto.SignDE(
				sourceFile,  // in, 本文
				rEncPubCert, // in, 收文方加密憑證
				p7EnvFile); // out, p7數位簽章
                    if (gcaCrypto.GetErrorCode() != 0) {
                        nextStep = -1;
                        errorMsg += "\n SignDE 失敗, code: " + gcaCrypto.GetErrorCode() + ", msg: " + gcaCrypto.GetErrorMsg();
                    } else {
                        nextStep++;
                        errorMsg += "\n SignDE 成功!!";
                    }
                }
                //*/
                //*
                if (nextStep >= 0) {
                    errCode = gcaCrypto.UnsignDE(
				p7EnvFile,     // in, p7數位簽章
				sourceFileRe,    // out, 本文
				sSignPubCert); // out, 發文方加簽憑證
                    if (gcaCrypto.GetErrorCode() != 0) {
                        nextStep = -1;
                        errorMsg += "\n UnsignDE 失敗, code: " + gcaCrypto.GetErrorCode() + ", msg: " + gcaCrypto.GetErrorMsg();
                    } else {
                        nextStep++;
                        errorMsg += "\n UnsignDE 成功!!";
                    }
                }
                //*/
                //*
                var loginSignStr = "";
                if (nextStep >= 0) {
                    loginSignStr = gcaCrypto.MakeLoginSign();
                    if (gcaCrypto.GetErrorCode() != 0) {
                        nextStep = -1;
                        errorMsg += "\n MakeLoginSign 失敗, code: " + gcaCrypto.GetErrorCode() + ", msg: " + gcaCrypto.GetErrorMsg();
                    } else {
                        nextStep++;
                        errorMsg += "\n MakeLoginSign 成功!!";
                    }
                }

                //取身分證欄位
                if (nextStep > 0) {
                    var userId = gcaCrypto.GetUserID();
                    if (gcaCrypto.GetErrorCode() != 0) {
                        nextStep = -1;
                        errorMsg += "\n GetUserID, code: 失敗, code: " + gcaCrypto.GetErrorCode() + ", msg: " + gcaCrypto.GetErrorMsg();
                    } else {
                        nextStep++;
                        //document.forms[0].userID.value = userId;
                    }
                }


                //取subject
                var cardDN = "";
                if (nextStep >= 0) {
                    cardDN = gcaCrypto.GetSubjectDNFromCert(sSignPubCert); // in, 發文方加簽憑證
                    document.forms[0].cardDN.value = cardDN.substring(0, cardDN.length);
                    if (gcaCrypto.GetErrorCode() != 0) {
                        nextStep = -1;
                        errorMsg += "\n GetSubjectDN 失敗, code: " + gcaCrypto.GetErrorCode() + ", msg: " + gcaCrypto.GetErrorMsg();
                    } else {
                        nextStep++;
                        errorMsg += "\n GetSubjectDN 成功, DN: " + cardDN;
                    }
                }
                //*/

                if (nextStep >= 0) {
                    errorMsg += "\n\n 您的讀卡機加解密元件已可正常運作!!";
                } else {
                    errorMsg += "\n\n 您的讀卡機加解密元件有問題，請聯絡客服人員!!";
                }
            } catch (e) {
                alert('error: ' + Error(e));
            } finally {
                if (gcaCrypto != null) {
                    gcaCrypto.Destory();
                    gcaCrypto = null;
                } else if (gcaCrypto == null) {
                    alert("已取消Active X 控制項");
                    window.location.reload();
                }
                gcaCrypto = null;
            }
            return;
        }
//-->
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <br />
        <br />
        <br />
        <br />
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
                                                PIN碼：&nbsp;<asp:TextBox 
                                                    name="tbox_pin" ID="tbox_pin" runat="server" MaxLength="30"
                                                    Width="150px" TextMode="Password"></asp:TextBox>
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
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton1" runat="server" 
                                                            ImageUrl="~/image/login-06.gif" OnClientClick="startCheckCert()" 
                                                            onclick="ImageButton1_Click" AlternateText="登入" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div align="right">
                                                <a href="#" class="login-a">帳號申請</a><img src="image/login-08.gif" width="19" height="19"
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
                                                <a href="#" class="login-a">公文附件下載</a><img src="image/login-08.gif" width="19" height="19"
                                                    align="absmiddle" /></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <div style="text-align: right">
                        <a href="login.aspx">
                            <img border="0" id="Image1" src="~/image/login_PIN_1.jpg" alt="使用帳號密碼登入" runat="server" />
                        </a>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="cardDN" runat="server" />
    </form>
</body>
</html>
