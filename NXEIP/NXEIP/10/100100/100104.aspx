<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="100104.aspx.cs" Inherits="_10_100100_100104" %>

<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/JavaScript">
<!--
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
                    var pinNum = document.forms[0].<%=tbox_pin.ClientID %>.value;
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
                    document.forms[0].<%=cardDN.ClientID %>.value = cardDN.substring(0, cardDN.length);
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="100104" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
            </div>
            <div class="h3">
            </div>
        </div>
        <table>
            <tbody>
                <tr>
                    <th>
                        請輸入自然人憑證卡片密碼：
                    </th>
                    <td>
                        <asp:TextBox ID="tbox_pin" name="tbox_pin" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="footer">
            <div class="f1">
            </div>
            <div class="f2">
            </div>
            <div class="f3">
            </div>
        </div>
        <div class="bottom">
            <asp:Button ID="btn_ok" runat="server" CssClass="b-input" Text="確定" OnClick="btn_ok_Click"
                OnClientClick="startCheckCert()" />
            &nbsp;&nbsp;
            <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="取消" UseSubmitBehavior="False" />
        </div>
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
            </div>
            <div class="h3">
            </div>
        </div>
        <table>
            <tbody>
                <tr>
                    <th>
                        作業說明：
                    </th>
                    <td>
                        
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="footer">
            <div class="f1">
            </div>
            <div class="f2">
            </div>
            <div class="f3">
            </div>
        </div>
        <asp:HiddenField ID="cardDN" runat="server" />
    </div>
</asp:Content>
