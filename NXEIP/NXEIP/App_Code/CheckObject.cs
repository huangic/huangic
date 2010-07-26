using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// CheckObject 的摘要描述
/// </summary>
public class CheckObject
{
	public CheckObject()
	{
		
	}

    #region 身分證字號檢查(true：正確  false：錯誤)
    /// <summary>
    /// 身分證字號檢查(true：正確  false：錯誤)
    /// </summary>
    /// <param name="IDCARD">身份證字號</param>
    /// <returns></returns>
    public bool CheckIDCard(string IDCARD)
    {
        bool feedback = true;
        ChangeObject co = new ChangeObject();
        if (IDCARD.Length < 10)
        {
            feedback = false;
        }
        else
        {
            char[] str = IDCARD.ToCharArray();
            if (co.ChangeIDcardLetter(str[0]) < 0)
            {
                feedback = false;
            }
            else if (Convert.ToInt32(str[1]) < 1 || Convert.ToInt32(str[1]) > 2)
            {
                feedback = false;
            }
            string str1 = co.ChangeIDcardLetter(str[0]).ToString();

            int equation = Convert.ToInt32(str1.Substring(0, 1))
                + Convert.ToInt32(str1.Substring(1, 1)) * 9
                + Convert.ToInt32(str[1].ToString()) * 8
                + Convert.ToInt32(str[2].ToString()) * 7
                + Convert.ToInt32(str[3].ToString()) * 6
                + Convert.ToInt32(str[4].ToString()) * 5
                + Convert.ToInt32(str[5].ToString()) * 4
                + Convert.ToInt32(str[6].ToString()) * 3
                + Convert.ToInt32(str[7].ToString()) * 2
                + Convert.ToInt32(str[8].ToString()) * 1;

            if ((10 - (equation % 10) - Convert.ToInt32(str[9].ToString())) == 0)
            {
                feedback = true;
            }
            else
            {
                feedback = false;
            }
        }
        return feedback;
    }
    #endregion

    #region Email是否正確(true：正確  false：錯誤)
    /// <summary>
    /// Email是否正確(true：正確  false：錯誤)
    /// </summary>
    /// <param name="strIn">輸入字串</param>
    /// <returns>true/false：正確/錯誤</returns>
    public bool IsValidEmail(string strIn)
    {
        if (System.Text.RegularExpressions.Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    #region 是否為空白(true：是  false：否)
    /// <summary>
    /// 是否為空白(true：是  false：否)
    /// </summary>
    /// <param name="strIn">輸入字串</param>
    /// <returns>true/false：是/否</returns>
    public bool IsBlank(string strIn)
    {
        if (strIn.Equals(""))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    #region 是否為Int(true：是  false：否)
    /// <summary>
    /// 是否為Int(true：是  false：否)
    /// </summary>
    /// <param name="strIn">輸入字串</param>
    /// <returns>true/false：是/否</returns>
    public bool IsInt(string strIn)
    {
        try
        {
            int isIntTmp = Convert.ToInt32(strIn);
            return true;
        }
        catch
        {
            return false;
        }
    }
    #endregion

    #region 是否在長度限制內(true：是  false：否)
    /// <summary>
    /// 是否在長度限制內(true：是  false：否)
    /// </summary>
    /// <param name="strIn">輸入字串</param>
    /// <param name="limitLen">限制長度</param>
    /// <returns>true/false：是/否</returns>
    public bool IsValidLen(string strIn, int limitLen)
    {
        if (strIn.Length > limitLen)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    #endregion

    #region 電話是否正確「只允許0~9、-、(、)」(true：正確  false：錯誤)
    /// <summary>
    /// 電話是否正確「只允許0~9、-、(、)」(true：正確  false：錯誤)
    /// </summary>
    /// <param name="strIn">輸入字串</param>
    /// <returns>true/false：正確/錯誤</returns>
    public bool IsValidTel(string strIn)
    {
        strIn = strIn.Replace("\r\n", "");
        if (System.Text.RegularExpressions.Regex.IsMatch(strIn, @"^[0-9\-\(\)]+$"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    #region 判斷 ROC 年月日 的 合法性  checkROC_YMD
    /// <summary>
    /// 判斷 ROC 年月日 的 合法性
    /// </summary>
    /// <param name="aROC"> 194-05-01 or 94-05-01</param>
    /// <returns></returns>
    public bool checkROC_YMD(string aROC)
    {
        ChangeObject co = new ChangeObject();
        bool rtnval = false;
        string aAD = "";
        System.DateTime aADTIME;
        try
        {
            aAD = co.ROCDTtoADDT(aROC);
            if (aAD.Equals("0") || (aAD.Split('-')[0].Equals("0")))
            {
                rtnval = false;
            }
            else
            {
                aADTIME = Convert.ToDateTime(aAD);
                rtnval = true;
            }
        }
        catch
        {
            rtnval = false;
        }
        return rtnval;
    }
    #endregion

    #region 檔案類型是否正確(true:是 false:否)
    /// <summary>
    /// 檔案類型是否正確(true:是 false:否)
    /// </summary>
    /// <param name="filetype">副檔名</param>
    /// <param name="validfiletype">檔案類型字串(以,區隔)</param>
    /// <returns></returns>
    public bool IsValidFileType(string filetype, string validfiletype)
    {
        string[] vft = validfiletype.Split(',');
        int vft_count = 0;
        for (int i = 0; i < vft.Length; i++)
        {
            if (filetype.Equals(vft[i].ToString()))
            {
                vft_count++;
            }
        }
        if (vft_count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion
}
