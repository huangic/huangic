using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Topic 的摘要描述
/// </summary>
public class Topic
{
	public Topic()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}

    public int ForumId { get; set; }

    public int Id { get; set; }

    /// <summary>
    /// 屬於哪一篇的回覆
    /// </summary>
    public int ParentId { get; set; }




    /// <summary>
    /// 主題名稱
    /// </summary>
    public String Name { get; set; }
    

    /// <summary>
    /// 作者編號
    /// </summary>
    public int AuthorId { get; set; }
    /// <summary>
    /// 作者
    /// </summary>
    public String Author { get; set; }

    /// <summary>
    /// 內容
    /// </summary>
    public String Content { get; set; }

    /// <summary>
    /// 發表日期
    /// </summary>
    public DateTime PublishDate { get; set; }
    /// <summary>
    /// 回復數
    /// </summary>
    public int RelayCount { get; set; }
    /// <summary>
    /// 觀看數
    /// </summary>
    public int ViewCount { get; set; }
    /// <summary>
    /// 最後回覆
    /// </summary>
    public String LastRelay { get; set; }

    /// <summary>
    /// 最後回覆作者
    /// </summary>
    public String LastRelayAuthor { get; set; }

    /// <summary>
    /// 最後回覆日
    /// </summary>
    public DateTime LastRelayDate { get; set; }

    /// <summary>
    /// 權限(總管理權限,版主權限,作者權限)
    /// </summary>
    public String Permission { get; set; }

    /// <summary>
    /// 檔案名稱
    /// </summary>
    public String FileName { get; set; }

    /// <summary>
    /// 檔案格式
    /// </summary>
    public String FileFormat { get; set; }

    /// <summary>
    /// 檔案路徑
    /// </summary>
    public String FilePath { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// 有作者或是管理或是總管理權限
    /// </summary>
    public bool HasPermission {
        get {
            int permission = System.Convert.ToInt32(this.Permission, 2);

            return (permission >= 1);
        }
    
    }

    public bool IsManager {
        get
        {
            int permission = System.Convert.ToInt32(this.Permission, 2);

            return (permission & 2)==2;
        }
    }

    public String Uid { get; set; }

    public String Sex { get {
        return this.Uid.Substring(1, 1);
        }
    }

    public int FolderId { get; set; }

    public int TrackId { get; set; }


}