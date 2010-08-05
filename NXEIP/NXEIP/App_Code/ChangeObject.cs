using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Collections;
using System.Globalization;

/// <summary>
/// ChangeObject 的摘要描述
/// </summary>
public class ChangeObject
{
	public ChangeObject()
	{
		
	}

    /// <summary>
    /// 回傳民國年格式日期
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public string _ADtoROC(DateTime dt)
    {
        TaiwanCalendar tc = new TaiwanCalendar( );
        
        return tc.GetYear(dt)+"-"+tc.GetMonth(dt)+"-"+tc.GetDayOfMonth(dt);
    }

    /// <summary>
    /// 回傳西元年格式日期
    /// </summary>
    /// <param name="roc">民國年</param>
    /// <returns></returns>
    public DateTime _ROCtoAD(string roc)
    {
        return Convert.ToDateTime(ROCDTtoADDT(roc));
    }

    #region 民國 轉西元年 ROCDTtoADDT
    /// <summary>
    /// 民國 轉西元年
    /// </summary>
    /// <param name="ROCDT"> 194-04-08-23-11-15  年-月-日-小時-分-秒</param>
    /// <returns></returns>
    public string ROCDTtoADDT(string ROCDT)
    {
        string aADDT = "";
        string[] aDT = ROCDT.Split('-');
        aADDT = ROCtoAD(aDT[0]);
        try
        {
            aADDT += "-" + aDT[1];
        }
        catch
        {

        }
        try
        {
            aADDT += "-" + aDT[2];
        }
        catch
        {

        }
        try
        {
            aADDT += " " + aDT[3];
        }
        catch
        {

        }
        try
        {
            aADDT += ":" + aDT[4];
        }
        catch
        {

        }
        try
        {
            aADDT += ":" + aDT[5];
        }
        catch
        {

        }
        return aADDT;
    }
    #endregion

    #region 西元年月日時間轉 民國年月日時間  ADDTtoROCDT
    /// <summary>
    /// 西元年月日時間轉 民國年月日時間
    /// </summary>
    /// <param name="ADDT"> 2005-05-06-23-11-15  西元年-月-日-小時-分-秒</param>
    /// <returns>民國年-月-日 時間 94-05-06 23:11:15</returns>
    public string ADDTtoROCDT(string ADDT)
    {
        string aROCDT = "";
        string[] aDT = ADDT.Split('-');
        aROCDT = ADtoROC(aDT[0]);
        try
        {
            aROCDT += "-" + aDT[1];
        }
        catch { }

        try
        {
            aROCDT += "-" + aDT[2];
        }
        catch { }

        try
        {
            aROCDT += " " + aDT[3];
        }
        catch { }

        try
        {
            aROCDT += ":" + aDT[4];
        }
        catch { }

        try
        {
            aROCDT += ":" + aDT[5];
        }
        catch { }

        return aROCDT;
    }
    #endregion

    #region 轉 民國XXX年XX月XX日時間 ROCto3ROC
    /// <summary>
    /// 轉 民國XXX年XX月XX日時間
    /// </summary>
    /// <param name="ROC"> 94-07-06 TO 094-07-06</param>
    /// <returns>民國年月日 094-05-06</returns>
    public string ROCto3ROC(string ROC)
    {
        string aROCDT = "";
        string[] aDT = ROC.Split('-');
        if (aDT[0].Length < 3)
        {
            aROCDT = "0" + aDT[0];
        }
        else
        {
            aROCDT = aDT[0];
        }

        try
        {
            aROCDT += "-" + aDT[1];
        }
        catch
        {

        }
        try
        {
            aROCDT += "-" + aDT[2];
        }
        catch
        {

        }

        return aROCDT;
    }
    #endregion

    #region 轉 民國XX年XX月XX日時間  ROCto2ROC
    /// <summary>
    /// 轉 民國XX年XX月XX日時間
    /// </summary>
    /// <param name="ROC">094-07-14 to 94-07-14</param>
    /// <returns>民國年月日 94-05-06</returns>
    public string ROCto2ROC(string ROC)
    {
        string aROCDT = "";
        string[] aDT = ROC.Split('-');
        if (aDT[0].Substring(0, 1).Equals("0"))
        {
            aROCDT = aDT[0].Substring(1, 2);
        }
        else
        {
            aROCDT = aDT[0];
        }

        try
        {
            aROCDT += "-" + aDT[1];
        }
        catch
        {

        }
        try
        {
            aROCDT += "-" + aDT[2];
        }
        catch
        {

        }

        return aROCDT;
    }
    #endregion

    #region yyy-MM-dd 轉成 yyMMdd  strFormat
    /// <summary>
    /// yyy-MM-dd 轉成 yyMMdd
    /// </summary>
    /// <param name="aStr"></param>
    /// <returns>yyMMdd</returns>
    public string strFormat(string aStr)
    {
        string[] aryStr;
        string rtnVal = "";
        aryStr = aStr.Split('-');
        rtnVal = aryStr[0].Trim() + aryStr[1].Trim() + aryStr[2].Trim();
        return rtnVal;
    }
    #endregion

    #region 西元(AD)年轉民國年(ROC)  ADtoROC
    /// <summary>
    /// 西元(AD)年轉民國年(ROC)
    /// </summary>
    /// <param name="aAD">西元年1990 如是非正確格調 則傳回 0</param>
    /// <returns>民國年</returns>
    public string ADtoROC(string aAD)
    {
        int aROC = 0;
        try
        {
            aROC = Convert.ToInt32(aAD);
            aROC -= 1911;
        }
        catch
        {
            aROC = 0;
        }
        return aROC.ToString();
    }
    #endregion

    #region 民國年(ROC)轉西元年(AD) ROCtoAD
    /// <summary>
    /// 民國年(ROC)轉西元年(AD)
    /// </summary>
    /// <param name="aROC"></param>
    /// <returns>西元</returns>
    public string ROCtoAD(string aROC)
    {
        int aAD = 0;
        try
        {
            aAD = Convert.ToInt32(aROC);
            aAD += 1911;
        }
        catch
        {
            aAD = 0;
        }

        return aAD.ToString();
    }
    #endregion

    #region 金額轉大寫 MoneytoChi
    /// <summary>
    /// 金額轉大寫
    /// </summary> 
    /// <param name="aMoney">傳入金額</param>
    /// <returns>傳回大寫</returns>
    public string MoneytoChi(string aMoney)
    {
        string toMoney = "";
        for (int i = 0; i < aMoney.Length; i++)
        {
            switch (Convert.ToInt32(aMoney.Substring(i, 1)))
            {
                case 9:
                    toMoney += "玖";
                    break;
                case 8:
                    toMoney += "捌";
                    break;
                case 7:
                    toMoney += "柒";
                    break;
                case 6:
                    toMoney += "陸";
                    break;
                case 5:
                    toMoney += "伍";
                    break;
                case 4:
                    toMoney += "肆";
                    break;
                case 3:
                    toMoney += "參";
                    break;
                case 2:
                    toMoney += "貳";
                    break;
                case 1:
                    toMoney += "壹";
                    break;
                case 0:
                    toMoney += "零";
                    break;
                default:
                    break;
            }

            switch (Convert.ToInt32(aMoney.Length) - i)
            {
                case 12:
                    toMoney += "仟";
                    break;
                case 11:
                    toMoney += "佰";
                    break;
                case 10:
                    toMoney += "拾";
                    break;
                case 9:
                    toMoney += "億";
                    break;
                case 8:
                    toMoney += "仟";
                    break;
                case 7:
                    toMoney += "佰";
                    break;
                case 6:
                    toMoney += "拾";
                    break;
                case 5:
                    toMoney += "萬";
                    break;
                case 4:
                    toMoney += "仟";
                    break;
                case 3:
                    toMoney += "佰";
                    break;
                case 2:
                    toMoney += "拾";
                    break;
                case 1:
                    toMoney += "元整";
                    break;
                default:
                    break;
            }
        }
        return toMoney;
    }
    #endregion

    #region 094-08-08 轉成 九十四年八月五日  ROCtoROCDT
    /// <summary>
    /// 094-08-08 轉成 九十四年八月五日
    /// </summary>
    /// <param name="date">傳入日期格式 094-08-08</param>
    /// <returns>回傳九十四年八月八日</returns>
    public string ROCtoROCDT(string date)
    {
        string[] arydate;
        string aROCD = " ";
        arydate = ROCto2ROC(date).Split('-');

        //年
        try
        {
            for (int i = 0; i < arydate[0].Length; i++)
            {
                if ((i == arydate[0].Length - 1) && (Convert.ToInt32(arydate[0].Substring(arydate[0].Length - 1, 1)) == 0))
                {

                }
                else
                {
                    switch (Convert.ToInt32(arydate[0].Substring(i, 1)))
                    {
                        case 9:
                            aROCD += "九";
                            break;
                        case 8:
                            aROCD += "八";
                            break;
                        case 7:
                            aROCD += "七";
                            break;
                        case 6:
                            aROCD += "六";
                            break;
                        case 5:
                            aROCD += "五";
                            break;
                        case 4:
                            aROCD += "四";
                            break;
                        case 3:
                            aROCD += "三";
                            break;
                        case 2:
                            aROCD += "二";
                            break;
                        case 1:
                            aROCD += "一";
                            break;
                        case 0:
                            aROCD += "零";
                            break;
                        default:
                            break;
                    }
                }

                if (arydate[0].Length == 3 && i == 0)
                {
                    aROCD += "百";
                }
                else if ((arydate[0].Length == 2 && i == 0) || (arydate[0].Length == 3 && i == 1))
                {
                    aROCD += "十";
                }

            }
            if (Convert.ToInt32(arydate[0]) % 100 == 0)
            {
                aROCD = aROCD.Substring(0, 3);
            }

            aROCD += "年";
        }
        catch
        {

        }

        //月
        try
        {
            switch (Convert.ToInt32(arydate[1]))
            {
                case 12:
                    aROCD += " 十二月 ";
                    break;
                case 11:
                    aROCD += " 十一月 ";
                    break;
                case 10:
                    aROCD += " 十月 ";
                    break;
                case 9:
                    aROCD += " 九月 ";
                    break;
                case 8:
                    aROCD += " 八月 ";
                    break;
                case 7:
                    aROCD += " 七月 ";
                    break;
                case 6:
                    aROCD += " 六月 ";
                    break;
                case 5:
                    aROCD += " 五月 ";
                    break;
                case 4:
                    aROCD += " 四月 ";
                    break;
                case 3:
                    aROCD += " 三月 ";
                    break;
                case 2:
                    aROCD += " 二月 ";
                    break;
                case 1:
                    aROCD += " 一月 ";
                    break;
                default:
                    break;
            }

        }
        catch
        {

        }

        //日
        try
        {
            for (int i = 0; i < arydate[2].Length; i++)
            {
                if ((i == 0) && ((Convert.ToInt32(arydate[2].Substring(i, 1)) == 0) || (Convert.ToInt32(arydate[2].Substring(i, 1)) == 1)))
                {

                }
                else
                {
                    switch (Convert.ToInt32(arydate[2].Substring(i, 1)))
                    {
                        case 9:
                            aROCD += "九";
                            break;
                        case 8:
                            aROCD += "八";
                            break;
                        case 7:
                            aROCD += "七";
                            break;
                        case 6:
                            aROCD += "六";
                            break;
                        case 5:
                            aROCD += "五";
                            break;
                        case 4:
                            aROCD += "四";
                            break;
                        case 3:
                            aROCD += "三";
                            break;
                        case 2:
                            aROCD += "二";
                            break;
                        case 1:
                            aROCD += "一";
                            break;
                        /*							case 0:
                                                        aROCD+="零";
                                                        break;
                        */
                        default:
                            break;
                    }
                }

                if (i == 0 && Convert.ToInt32(arydate[2].Substring(0, 1)) > 0)
                {
                    aROCD += "十";
                }
            }

            aROCD += "日";
        }
        catch
        {

        }
        return aROCD;
    }
    #endregion

    #region 094-07-01 轉成 94年07月01日 ROCtoChi
    /// <summary>
    /// 094-07-01 轉成 94年07月01日
    /// </summary>
    /// <param name="date">094-07-01</param>
    /// <returns>94年07月01日</returns>
    public string ROCtoChi(string date)
    {
        string[] arydate;
        string aROCD = " ";
        arydate = ROCto2ROC(date).Split('-');
        aROCD = arydate[0] + " 年 " + arydate[1] + " 月 " + arydate[2] + " 日 ";
        return aROCD;
    }
    #endregion

    #region 星期轉換
    /// <summary>
    /// 星期轉換
    /// </summary>
    /// <param name="DT">日期</param>
    /// <returns>代表星期的數字(1~7)</returns>
    public int ChangeWeek(DateTime DT)
    {
        int feedback = 7;
        if (DT.DayOfWeek == System.DayOfWeek.Monday)
        {
            feedback = 1;
        }
        else if (DT.DayOfWeek == System.DayOfWeek.Tuesday)
        {
            feedback = 2;
        }
        else if (DT.DayOfWeek == System.DayOfWeek.Wednesday)
        {
            feedback = 3;
        }
        else if (DT.DayOfWeek == System.DayOfWeek.Thursday)
        {
            feedback = 4;
        }
        else if (DT.DayOfWeek == System.DayOfWeek.Friday)
        {
            feedback = 5;
        }
        else if (DT.DayOfWeek == System.DayOfWeek.Saturday)
        {
            feedback = 6;
        }
        else if (DT.DayOfWeek == System.DayOfWeek.Sunday)
        {
            feedback = 7;
        }
        return feedback;
    }

    /// <summary>
    /// 星期轉換
    /// </summary>
    /// <param name="DOW">星期(DayOfWeek)</param>
    /// <returns>代表星期的文字(一~日)</returns>
    public string ChangeWeek(System.DayOfWeek DOW)
    {
        string feedback = "";
        if (DOW == System.DayOfWeek.Monday)
        {
            feedback = "一";
        }
        else if (DOW == System.DayOfWeek.Tuesday)
        {
            feedback = "二";
        }
        else if (DOW == System.DayOfWeek.Wednesday)
        {
            feedback = "三";
        }
        else if (DOW == System.DayOfWeek.Thursday)
        {
            feedback = "四";
        }
        else if (DOW == System.DayOfWeek.Friday)
        {
            feedback = "五";
        }
        else if (DOW == System.DayOfWeek.Saturday)
        {
            feedback = "六";
        }
        else if (DOW == System.DayOfWeek.Sunday)
        {
            feedback = "日";
        }
        return feedback;
    }

    /// <summary>
    /// 星期轉換
    /// </summary>
    /// <param name="nweek">代表星期的數字(1~7)</param>
    /// <returns>代表星期的文字(一~日)</returns>
    public string ChangeWeek(int nweek)
    {
        string feedback = "";
        if (nweek == 1)
        {
            feedback = "一";
        }
        else if (nweek == 2)
        {
            feedback = "二";
        }
        else if (nweek == 3)
        {
            feedback = "三";
        }
        else if (nweek == 4)
        {
            feedback = "四";
        }
        else if (nweek == 5)
        {
            feedback = "五";
        }
        else if (nweek == 6)
        {
            feedback = "六";
        }
        else if (nweek == 7)
        {
            feedback = "日";
        }
        return feedback;
    }
    #endregion

    #region 把身份證字母轉換成數字
    /// <summary>
    /// 把身份證字母轉換成數字
    /// </summary>
    /// <param name="ch">字母</param>
    /// <returns>轉換後的數字</returns>
    public int ChangeIDcardLetter(char ch)
    {
        int feedback = 0;
        switch (ch)
        {
            case 'A':
                feedback = 10;
                break;
            case 'B':
                feedback = 11;
                break;
            case 'C':
                feedback = 12;
                break;
            case 'D':
                feedback = 13;
                break;
            case 'E':
                feedback = 14;
                break;
            case 'F':
                feedback = 15;
                break;
            case 'G':
                feedback = 16;
                break;
            case 'H':
                feedback = 17;
                break;
            case 'I':
                feedback = 34;
                break;
            case 'J':
                feedback = 18;
                break;
            case 'K':
                feedback = 19;
                break;
            case 'L':
                feedback = 20;
                break;
            case 'M':
                feedback = 21;
                break;
            case 'N':
                feedback = 22;
                break;
            case 'O':
                feedback = 35;
                break;
            case 'P':
                feedback = 23;
                break;
            case 'Q':
                feedback = 24;
                break;
            case 'R':
                feedback = 25;
                break;
            case 'S':
                feedback = 26;
                break;
            case 'T':
                feedback = 27;
                break;
            case 'U':
                feedback = 28;
                break;
            case 'V':
                feedback = 29;
                break;
            case 'W':
                feedback = 32;
                break;
            case 'X':
                feedback = 30;
                break;
            case 'Y':
                feedback = 31;
                break;
            case 'Z':
                feedback = 33;
                break;
            default:
                feedback = -1;
                break;
        }
        return feedback;
    }
    #endregion

    #region 把檔案轉換成二進位
    /// <summary>
    /// 把檔案轉換成二進位
    /// </summary>
    /// <param name="filename">檔案實體位置與檔名(例：d:\uploadfile\aa.txt)</param>
    /// <returns>二進位 byte</returns>
    public byte[] getFileByte(string filename)
    {
        ArrayList OUTFILE = new ArrayList();
        byte[] outb;
        string FilePath = filename;

        try
        {
            FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            BinaryReader BFD = new BinaryReader(fs);
            try
            {
                while (true)
                {
                    OUTFILE.Add(BFD.ReadByte());
                }
            }
            catch
            {
                outb = new byte[OUTFILE.Count];
                for (int i = 0; i < OUTFILE.Count; i++)
                {
                    outb[i] = (byte)OUTFILE[i];
                }
                fs.Close();
                return outb;
            }
        }
        catch
        {
            throw new Exception("找不到" + filename + "檔案");
        }
    }
    #endregion
}
