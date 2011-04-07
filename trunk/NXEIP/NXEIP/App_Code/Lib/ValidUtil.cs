using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;


namespace NXEIP.Lib
{
    /// <summary>
    /// 驗證字串格式
    /// </summary>
    public static class ValidUtil
    {
        

        /// <summary>
        /// 驗證EMAIL格式
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static bool IsValidEmail(this string strIn)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }


        /// <summary>
        /// method to validate an IP address
        /// using regular expressions. The pattern
        /// being used will validate an ip address
        /// with the range of 1.0.0.0 to 255.255.255.255
        /// </summary>
        /// <param name="addr">Address to validate</param>
        /// <returns></returns>
        public static bool IsValidIP(this string addr)
        {
            
            //create our match pattern
            string pattern = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";
            //create our Regular Expression object
            Regex check = new Regex(pattern);
            //boolean variable to hold the status
            bool valid = false;
            //check to make sure an ip address was provided
            if (addr == "")
            {
                //no address provided so return false
                valid = false;
            }
            else
            {
                //address provided so use the IsMatch Method
                //of the Regular Expression object
                valid = check.IsMatch(addr, 0);
            }
            //return the results
            return valid;
        }


        public static bool IsNumber(this string number) {
            decimal result = 0;
            return decimal.TryParse(number, out result);
        
        }

        /// <summary>
        /// 驗證字串是否各包含一個數字,英文字母,字串長度在4~12碼
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsPowerAccount(this string val)
        {
            
            Regex regex = new Regex(@"^(?=.*[0-9])(?=.*[a-zA-Z]).{4,12}$");
            if (string.IsNullOrEmpty(val))
            {
                return false;
            }
            else
            {
                return regex.IsMatch(val);
            }
        }

        /// <summary>
        /// 驗證字串是否各包含一個數字及英文字母,特殊符號,字串長度在4~12碼
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsPowerPassWD(this string val)
        {
            //1. 至少有一個數字
            //2. 至少有一個大寫或小寫英文字母
            //3. 至少有一個特殊符號 \W
            //4. 字串長度在 4 ~ 12 個字母之間
            Regex regex = new Regex(@"^(?=.*\d)(?=.*[a-zA-Z])(?=.*\W).{4,12}$");
            if (string.IsNullOrEmpty(val))
            {
                return false;
            }
            else
            {
                return regex.IsMatch(val);
            }
        }

        /// <summary>
        /// ID號碼是否正確 true:正確
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        public static bool IsIDNumber(this string strID)
        {
            if (string.IsNullOrEmpty(strID))
            {
                return false;
            }
            else
            {
                return CheckIDNO(strID, "0");
            }
            
        }

        #region 驗證 國民身分證 居留證 護照號
        private static bool CheckIDNO(string strIDNO, string Kind)
        {
            string CharEng = "ABCDEFGHJKLMNPQRSTUVXYWZIO";
            string CharNumber = "0123456789";
            
            //True: 驗証成功、False:驗証有誤
            bool rst = false;
             
            //Kind: 0:自動 1:國民身分證 2:居留證 3:護照號(暫無規則)長度應小於20(護照號碼正確)

            strIDNO = strIDNO.ToUpper();

            switch (Kind)
            {
                case "0":
                    if (strIDNO.Length >= 10)
                    {
                        switch (Convert.ToString(strIDNO).Substring(1, 1))
                        {
                            case "1":
                            case "2":
                                //身分證號碼有誤(第2碼為 1、2 依國民身分證規則判斷)!!
                                rst = CheckIDCard(strIDNO);
                                break;
                            case "A":
                            case "C":
                            case "B":
                            case "D":
                                //居留號碼有誤(第2碼為 A、C、B、D 依國民居留號碼規則判斷)!!
                                rst = CheckIDNO(strIDNO,"2");
                                break;
                            default:
                                break;
                            //身分證號碼或居留證號碼有誤，第2碼，應為(1.2或A.C.B.D)
                            //rst = False
                        }
                    }
                    break;
                case "1":
                    rst = CheckIDCard(strIDNO);
                    break;
                case "2":
                    if (strIDNO.Length < 10)
                        break; // TODO: might not be correct. Was : Exit Select

                    if (CharEng.IndexOf(strIDNO[0]) == -1 || CharEng.IndexOf(strIDNO[1]) == -1)
                    {
                        break; // TODO: might not be correct. Was : Exit Select
                    }
                    for (int i = 0 + 2; i <= 10 - 1; i++)
                    {

                        if (CharNumber.IndexOf(strIDNO[i]) == -1)
                        {
                            break; // TODO: might not be correct. Was : Exit Select
                        }
                    }

                    int intS = 0;
                    string strID = "";
                    strID = "";
                    strID += Convert.ToString(CharEng.IndexOf(strIDNO[0]) + 10);
                    strID += Convert.ToString((CharEng.IndexOf(strIDNO[1]) + 10) % 10);
                    for (int i = 0 + 2; i <= 10 - 1; i++)
                    {
                        strID += Convert.ToString(strIDNO[i]);
                    }

                    intS += Convert.ToInt32(strID[0].ToString());
                    intS += Convert.ToInt32(strID[1].ToString()) * 9;
                    intS += Convert.ToInt32(strID[2].ToString()) * 8;
                    intS += Convert.ToInt32(strID[3].ToString()) * 7;
                    intS += Convert.ToInt32(strID[4].ToString()) * 6;
                    intS += Convert.ToInt32(strID[5].ToString()) * 5;
                    intS += Convert.ToInt32(strID[6].ToString()) * 4;
                    intS += Convert.ToInt32(strID[7].ToString()) * 3;
                    intS += Convert.ToInt32(strID[8].ToString()) * 2;
                    intS += Convert.ToInt32(strID[9].ToString()) * 1;
                    intS += Convert.ToInt32(strID[10].ToString());

                    if (((intS % 10) == 0))
                    {
                        //居留證號碼正確
                        rst = true;
                    }
                    break;
                case "3":
                    //長度應小於20(護照號碼正確)
                    if (strIDNO.Length <= 20)
                    {
                        rst = true;
                    }
                    break;
            }

            return rst;
        }

        #endregion

        #region 國民身分證字號檢查(true：正確  false：錯誤)
        /// <summary>
        /// 國民身分證字號檢查(true：正確  false：錯誤)
        /// </summary>
        /// <param name="IDCARD">身份證字號</param>
        /// <returns></returns>
        private static bool CheckIDCard(string IDCARD)
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


    }
}