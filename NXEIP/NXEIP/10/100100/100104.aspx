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
            
                <tr>
                    <td style="width:19%; text-align:right">
                        請輸入自然人憑證卡片密碼：
                    </td>
                    <td>
                        <asp:TextBox ID="tbox_pin" name="tbox_pin" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                    
                </tr>
            
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
                        作業說明
                    </th>
                    <td>
                        <table width="630" border="0" cellspacing="2" cellpadding="0">
                            <tr>
                                <td width="2%">
                                    &nbsp;
                                </td>
                                <td colspan="2" class="unnamed1" align="center">
                                    <!-- 市政府自設訊息區塊開始 -->
                                    <p>
                                        &nbsp;</p>
                                    <p style="text-align: left; font-size: 15px">
                                        下載憑證讀取元件：<a href="../../download/GCACryptoCom2.8.exe">GCACryptoCom2.8.exe</a></p>
                                    <div style="text-align: left; width: 630px; padding: 20px; border: 1px gray dashed;
                                        background-color: #ffffcc; font-size: 15px">
                                        <p style="font-weight: bold">
                                            作業說明：</p>
                                        <p style="line-height: 28px">
                                            要完成本項作業，您必須在IE瀏覽器底下先啟用"ActiveX元件"及"安裝憑證讀取元件"後，再進行自然人憑証註冊。執行步驟分別說明如下：</p>
                                        <p style="font-weight: bold; color: blue">
                                            1. 啟用ActiveX元件：</p>
                                        <p style="line-height: 28px">
                                            1-1. 請點選工具-網際網路選項，此時畫面將出現網際網路選項對話視窗。<br />
                                            <img src="../../image/pic/fig01.jpg" /><br />
                                            <br />
                                            1-2. 請點選安全性標籤-再按下自定層級按鈕，畫面上會出現安全性設定對話式窗。<br />
                                            <img src="../../image/pic/fig02.jpg" /><br />
                                            <br />
                                            1-3. 拉動捲動軸，找到ActiveX控制項與插件這項目，請參考下圖將各子項目設定成啟用或提示，再按下確定按鈕，即完成ActiveX元件的啟用。<br />
                                            <img src="../../image/pic/fig03.jpg" /><br />
                                            <br />
                                            <img src="../../image/pic/fig04.jpg" /></p>
                                        <p>
                                            &nbsp;</p>
                                        <p style="font-weight: bold; color: blue">
                                            2. 安裝憑證讀取元件：</p>
                                        <p style="line-height: 28px">
                                            2-1. 請點選右邊的連結：<a href="../../download/GCACryptoCom2.8.exe">請按我安裝憑證讀取元件</a><br />
                                            2-2. 畫面出現下載對話視窗，請按下執行按鈕<br />
                                            <img src="../../image/pic/fig05.jpg" /><br />
                                            <br />
                                            2-2. 如果畫面出現安全性警告，請按下執行繼續。<br />
                                            <img src="../../image/pic/fig06.jpg" /><br />
                                            <br />
                                            2-3. 當畫面出現安裝畫面，請按下&quot;Next&quot;按鈕開始進行安裝，當出現完成安裝對話視窗時，請將勾選拿掉，再按下&quot;Finish&quot;
                                            按鈕以完成安裝。<br />
                                            <img src="../../image/pic/fig07.jpg" /><br />
                                            <br />
                                            <img src="../../image/pic/fig08.jpg" /><br />
                                        </p>
                                        <p>
                                            &nbsp;</p>
                                        <p style="font-weight: bold; color: blue">
                                            3. 自然人憑証註冊：</p>
                                        <p style="line-height: 28px">
                                            3-1. 請在文字方塊內輸入您的自然人憑證密碼(預設值通常是您的出生民國年月日共六碼，例如600101)，再按下送出按鈕，當畫面出現視窗，按下確定後即完成自然人憑證註冊程序。<br />
                                            <img src="../../image/pic/fig09.png" /></p>
                                    </div>
                                    <!-- 市政府自設訊息區塊結束 -->
                                </td>
                                <td width="2%" class="unnamed1">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
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
