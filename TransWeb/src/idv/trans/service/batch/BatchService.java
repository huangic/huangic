package idv.trans.service.batch;

import idv.trans.model.Fileinfo;
import idv.trans.model.FileinfoDAO;
import idv.trans.model.Userinfo;
import idv.trans.model.UserinfoDAO;
import idv.trans.service.system.SystemVar;
import idv.trans.util.SpringUtil;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.RandomAccessFile;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import org.apache.commons.httpclient.HttpClient;
import org.apache.commons.httpclient.HttpException;
import org.apache.commons.httpclient.MultiThreadedHttpConnectionManager;
import org.apache.commons.httpclient.NameValuePair;
import org.apache.commons.httpclient.methods.PostMethod;
import org.aspectj.util.FileUtil;
import org.codehaus.plexus.util.FileUtils;
import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Restrictions;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class BatchService {
	//
	private static Logger logger = LoggerFactory.getLogger(BatchService.class);
	private static HttpClient httpclient = createHttpClient();

	private static HttpClient createHttpClient() {
		// MultiThreadedHttpConnectionManager.DEFAULT_MAX_HOST_CONNECTIONS=200;
		// MultiThreadedHttpConnectionManager.DEFAULT_MAX_TOTAL_CONNECTIONS=1024;
		MultiThreadedHttpConnectionManager manager = new MultiThreadedHttpConnectionManager();

		manager.getParams().setDefaultMaxConnectionsPerHost(1024);
		manager.getParams().setMaxTotalConnections(1024);
		manager.getParams().setSoTimeout(100000);
		manager.getParams().setConnectionTimeout(100000);

		// manager.getParams().set

		return new HttpClient(manager);
	}

	private String excuteDate = new SimpleDateFormat("yyyyMMddHHmm")
			.format(new Date());

	public void doBatch() {

		SystemVar systemVar = (SystemVar) SpringUtil.getBean("SystemVar");

		logger.info("執行批次轉檔");

		BatchService service = (BatchService) SpringUtil
				.getBean("BatchService");

		FileinfoDAO dao = (FileinfoDAO) SpringUtil.getBean("FileinfoDAO");

		// 把DAO的資料都抓出來準備丟轉檔

		List<Fileinfo> listTrans = dao.findByStatus((short) 1);

		DetachedCriteria criteria = DetachedCriteria.forClass(Fileinfo.class);

		criteria.add(Restrictions.eq("status", (short) 4));
		criteria.add(Restrictions.isNull("transdate"));

		List<Fileinfo> listCancel = (List<Fileinfo>) dao.getHibernateTemplate()
				.findByCriteria(criteria);

		logger.info("取消轉檔文件數:" + listCancel.size());
		doBackFile(listCancel);
		logger.info("準備轉檔文件數:" + listTrans.size());
		doTrans(listTrans);

	}

	public void doBackFile(List<Fileinfo> list) {
		SystemVar systemVar = (SystemVar) SpringUtil.getBean("SystemVar");
		FileinfoDAO dao = (FileinfoDAO) SpringUtil.getBean("FileinfoDAO");
		UserinfoDAO userdao = (UserinfoDAO) SpringUtil.getBean("UserinfoDAO");
		BatchRule rule = (BatchRule) SpringUtil.getBean("BatchRule");

		for (int i = 0; i < list.size(); i++) {
			// 把檔名抓一下
			Fileinfo fileinfo = list.get(i);

			Userinfo user = userdao.findById(fileinfo.getUserid());

			String account = user.getAccount();
			/*
			 * 轉檔檔名 /account/filename...
			 */

			String transfilepath = fileinfo.getFilepath();
			String transfilename = "/" + account + "/" + fileinfo.getFilename();
			String filename = fileinfo.getFilename();

			logger.info("序號:" + i + " 檔名:" + transfilename + " 被取消");

			boolean canTrans = true;

			{
				// 檢查一下要轉的檔案是否存在

				String absfilepath = transfilepath + "/" + filename;
				if (!FileUtils.fileExists(absfilepath)) {
					canTrans = false;
					logger.debug("檔案不存在!!" + absfilepath);
					logger.info("\t檔案不存在!!" + filename);
				}
			}

			try {

				// 使用者上傳檔案搬到/backup/upload/轉檔執行時間/user/txt檔

				StringBuffer oldFilePath = new StringBuffer();
				oldFilePath.append(systemVar.getUploadPath()).append(
						transfilename);

				StringBuffer newFilePath = new StringBuffer();
				newFilePath.append(systemVar.getBackupPath()).append("/")
						.append(excuteDate).append("/").append(transfilename);

				StringBuffer newFilePathDir = new StringBuffer();
				newFilePathDir.append(systemVar.getBackupPath()).append("/")
						.append(excuteDate).append("/").append(account).append(
								"/");

				logger.debug("OLD FILE:" + oldFilePath.toString());
				logger.debug("NEW FILE:" + newFilePath.toString());

				File oldTranFile = new File(oldFilePath.toString());
				File newTranFile = new File(newFilePath.toString());

				if (canTrans) {
					FileUtil.copyFile(oldTranFile, newTranFile);
					this.delete(oldTranFile);
				}
				// fileinfo更新 改變新檔名 轉檔時間 狀態 數值 //新檔名要改變因為上傳會檢查檔名

				fileinfo.setTransdate(new Date());
				fileinfo.setNewfilename(transfilename);
				fileinfo.setFilepath(newFilePathDir.toString());
				fileinfo.setAllnum(0);
				fileinfo.setErrornum(0);
				fileinfo.setSuccessnum(0);

				dao.attachDirty(fileinfo);

			} catch (Exception e) {
				e.printStackTrace();
			}

		}

	}

	public void doTrans(List<Fileinfo> list) {
		SystemVar systemVar = (SystemVar) SpringUtil.getBean("SystemVar");
		FileinfoDAO dao = (FileinfoDAO) SpringUtil.getBean("FileinfoDAO");
		UserinfoDAO userdao = (UserinfoDAO) SpringUtil.getBean("UserinfoDAO");
		BatchRule rule = (BatchRule) SpringUtil.getBean("BatchRule");

		for (int i = 0; i < list.size(); i++) {
			// 把檔名抓一下
			Fileinfo fileinfo = list.get(i);

			Userinfo user = userdao.findById(fileinfo.getUserid());

			String account = user.getAccount();
			/*
			 * 轉檔檔名 /account/filename...
			 */

			String transfilepath = fileinfo.getFilepath();
			String transfilename = "/" + account + "/" + fileinfo.getFilename();
			String filename = fileinfo.getFilename();

			String loggfile = "/" + account + "/"
					+ filename.substring(0, filename.lastIndexOf('.'))
					+ "_LOG.txt";

			String dbPattren = filename.substring(0, filename.indexOf('_'));

			String filePattren = filename.substring(0, filename
					.lastIndexOf('_'));

			// 把文件檔名比對一下PATTERN
			String ParserClass = (String) rule.getBatchPattern().get(
					filePattren);

			logger.info("序號:" + i + " 檔名規則:" + filePattren + " ParserClass:"
					+ ParserClass);

			logger.info("\tCSV:" + transfilename);
			// 參數塞一塞
			// tester.do?
			// DB=CBIDB&
			// CSV=F01_OPEN_CASE_SAMPLE.txt&
			// PSR=tw.gov.cbi.trans.parser.Parser01&
			// LOG=F01_OPEN_CASE_SAMPLE_LOG.txt
			logger.debug("URL:" + rule.getBatchURL());
			logger.debug("DB:" + rule.getBatchDB().get(dbPattren).toString());
			logger.debug("CSV:" + transfilename);
			logger.debug("PSR:" + ParserClass);
			logger.debug("LOG:" + loggfile);

			ArrayList<NameValuePair> args = new ArrayList<NameValuePair>();
			args.add(new NameValuePair("DB", rule.getBatchDB().get(dbPattren)
					.toString()));
			args.add(new NameValuePair("CSV", transfilename));
			args.add(new NameValuePair("PSR", ParserClass));
			args.add(new NameValuePair("LOG", loggfile));

			logger.info("\t傳送資料:" + rule.getBatchURL());

			boolean canTrans = true;

			{
				// 檢查一下要轉的檔案是否存在

				String absfilepath = transfilepath + "/" + filename;
				if (!FileUtils.fileExists(absfilepath)) {
					canTrans = false;
					logger.debug("檔案不存在!!" + absfilepath);
					logger.info("檔案不存在!!" + filename);
				}
			}

			if (canTrans) {

				try {
					String data = basicPostAction(rule.getBatchURL(), args,
							httpclient, rule.getRemoteCharset());

					logger.debug(data);
					// String logdata="";
					// Document xmldoc;
					// try {

					// SAXReader reader=new SAXReader(false);
					// try {
					// reader.setFeature(Constants.XERCES_FEATURE_PREFIX +
					// Constants.LOAD_EXTERNAL_DTD_FEATURE, false);
					// } catch (SAXException e) {
					// TODO Auto-generated catch block
					// e.printStackTrace();
					// }

					// } catch (DocumentException e) {
					// TODO Auto-generated catch block
					// e.printStackTrace();
					// }

					// data是執行完之後的LOG

					List<String> logdata = new ArrayList<String>();
					logdata.add("");
					// logger.debug(data);

					// 檢查一下是不是有檔案產生 系統路徑+LOG檔案
					String logpath = systemVar.getUploadPath() + "/" + loggfile;
					if (!FileUtils.fileExists(logpath)) {
						logger.info("\t路徑:" + logpath + " LOG文件沒有產生!");
					} else {
						// 把LOG檔案搬到LOG中的log/user/轉檔執行時間/log檔

						logdata = read(logpath, rule.getRemoteCharset());

						StringBuffer newlogPathDir = new StringBuffer(systemVar
								.getLogPath());
						newlogPathDir.append("/").append(excuteDate)
								.append("/");
						// newlogPathDir.append("/").append(account)
						// .append("/");
						StringBuffer newlogPath = new StringBuffer(
								newlogPathDir).append(loggfile);

						logger.debug("new LOG=" + newlogPath);
						File newLogFile = new File(newlogPath.toString());
						File logFile = new File(logpath);

						FileUtils.copyFile(logFile, newLogFile);
						
						delete(logFile);
						logger.debug("路徑:" + logpath + " LOG文件產生!");
						logger.debug("新路徑:" + newlogPath.toString()
								+ " LOG文件產生!");
						fileinfo.setLogpath(newlogPathDir.toString());
						fileinfo.setLogfilename(loggfile);
					}

					// Parser一下拿到的資料
					// 讀取DATA的最後一行
					// String[] splitdata=StringUtils.split(logdata,"\n");

					String parserData = logdata.get(0);
					logger.info("\t" + parserData);

					// 解讀一下資料，如果不是那就丟例外
					if (parserData.startsWith("總筆數 : ")) {
						int all_num = 0, success_num = 0, error_num = 0;

						Pattern p = Pattern.compile("\\d+");
						Matcher m = p.matcher(parserData);
						if (m.find()) {
							all_num = Integer.parseInt((m.group()));
						}
						if (m.find()) {
							success_num = Integer.parseInt((m.group()));
						}
						if (m.find()) {
							error_num = Integer.parseInt((m.group()));
						}

						Short status;
						if (all_num == success_num) {
							status = Short.parseShort("2");
						} else {
							status = Short.parseShort("3");
						}

						// 拿完所有資料了~可以寫入BACKUP與資料庫

						// 使用者上傳檔案搬到/backup/upload/轉檔執行時間/user/txt檔

						String oldFilePath = transfilepath + "/" + filename;

						StringBuffer newFilePath = new StringBuffer();
						newFilePath.append(systemVar.getBackupPath()).append(
								"/").append(excuteDate).append("/").append(
								transfilename);

						StringBuffer newFilePathDir = new StringBuffer();
						newFilePathDir.append(systemVar.getBackupPath())
								.append("/").append(excuteDate).append("/")
								.append(account).append("/");

						logger.debug("OLD FILE:" + oldFilePath.toString());
						logger.debug("NEW FILE:" + newFilePath.toString());

						File oldTranFile = new File(oldFilePath.toString());
						File newTranFile = new File(newFilePath.toString());

						FileUtil.copyFile(oldTranFile, newTranFile);

						delete(oldTranFile);

						// fileinfo更新 改變新檔名 轉檔時間 狀態 數值 //新檔名要改變因為上傳會檢查檔名
						fileinfo.setStatus(status);
						fileinfo.setTransdate(new Date());
						fileinfo.setAllnum(all_num);
						fileinfo.setErrornum(error_num);
						fileinfo.setSuccessnum(success_num);
						fileinfo.setNewfilename(transfilename);
						fileinfo.setFilepath(newFilePathDir.toString());
						dao.attachDirty(fileinfo);

					} else {
						logger.info("\t回傳資料錯誤");
					}

				} catch (HttpException e) {
					logger.info("\t無法連線遠端");
					e.printStackTrace();
				} catch (IOException e) {
					logger.info("\t系統IO異常,無法連線");
					// TODO Auto-generated catch block
					e.printStackTrace();
				}

				// 使用HTTPCLIENT去做轉檔
			}
		}
	}

	// 需要一個取dom的 然後拿最後一個textarea的值

	public static String basicPostAction(String url,
			ArrayList<NameValuePair> paramsList, HttpClient httpclient,
			String Charset) throws HttpException, IOException {

		httpclient.getParams().setParameter("http.protocol.content-charset",
				Charset);
		PostMethod post = new PostMethod(url);
		post.setRequestHeader("Connection", "close");
		NameValuePair[] params = new NameValuePair[paramsList.size()];
		paramsList.toArray(params);
		post.addParameters(params);
		try {
			int result = httpclient.executeMethod(post);
			if (result == 200) {
				return post.getResponseBodyAsString();
				// try {
				// httpClient.executeMethod(get);

				// System.out.println(get.getResponseBodyAsString());
				// List<String> data = new ArrayList<String>();
				// BufferedReader reader = new BufferedReader(
				// new InputStreamReader(post.getResponseBodyAsStream(),
				// "ISO-8859-1"));
				// String tmp = null;
				// String htmlRet = "";
				// while ((tmp = reader.readLine()) != null) {
				// data.add(new String(tmp.getBytes("ISO-8859-1"), Charset));
				// logger.debug(new String(tmp.getBytes("ISO-8859-1"),
				// Charset));

				// }

				// return data;
			} else {
				throw new RuntimeException("遠端URL錯誤:" + url + ":" + result
						+ ":" + post.getStatusText());
			}
		} finally {
			post.releaseConnection();
			
			// ((SimpleHttpConnectionManager)httpclient.getHttpConnectionManager()).shutdown();
		}
	}

	public static void delete(File file) {
        
		
		if (!file.canWrite()) {
			logger.debug("Cant W");

		}
		try {
			FileUtils.forceDelete(file);
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

	}

	public static List<String> read(String filename, String charset) {

		ArrayList<String> list = new ArrayList<String>();

		RandomAccessFile rf = null;
		try {
			rf = new RandomAccessFile(filename, "r");
			long len = rf.length();
			long start = rf.getFilePointer();
			long nextend = start + len - 1;
			String line;
			rf.seek(nextend);
			int c = -1;
			while (nextend > start) {
				c = rf.read();
				if (c == '\n' || c == '\r') {
					line = rf.readLine();
					if (line != null) {
						list.add(new String(line.getBytes("ISO-8859-1"),
								charset));
					} else {
						// System.out.println(line);
					}
					nextend--;
				}
				nextend--;
				rf.seek(nextend);
				if (nextend == 0) {// 当文件指针退至文件开始处，输出第一行
					// System.out.println(rf.readLine());
					list.add(new String(rf.readLine().getBytes("ISO-8859-1"),
							charset));

				}
			}
		} catch (FileNotFoundException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		} finally {
			try {
				if (rf != null)
					rf.close();
			} catch (IOException e) {
				e.printStackTrace();
			}
		}

		return list;
	}

}