package idv.trans.service.batch;

import idv.trans.model.Fileinfo;
import idv.trans.model.FileinfoDAO;
import idv.trans.model.Userinfo;
import idv.trans.model.UserinfoDAO;
import idv.trans.service.system.SystemVar;
import idv.trans.util.SpringUtil;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.List;

import org.apache.commons.httpclient.HttpClient;
import org.apache.commons.httpclient.HttpException;
import org.apache.commons.httpclient.MultiThreadedHttpConnectionManager;
import org.apache.commons.httpclient.NameValuePair;
import org.apache.commons.httpclient.methods.PostMethod;
import org.codehaus.plexus.util.FileUtils;
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

	public void doBatch() {

		SystemVar systemVar=(SystemVar)SpringUtil.getBean("SystemVar");
 
		logger.info("執行批次轉檔");

		BatchService service = (BatchService) SpringUtil
				.getBean("BatchService");

		FileinfoDAO dao = (FileinfoDAO) SpringUtil.getBean("FileinfoDAO");
		UserinfoDAO userdao = (UserinfoDAO) SpringUtil.getBean("UserinfoDAO");
		BatchRule rule = (BatchRule) SpringUtil.getBean("BatchRule");

		// 把DAO的資料都抓出來準備丟轉檔

		List<Fileinfo> list = dao.findByStatus((short) 1);
		logger.info("準備轉檔文件數:" + list.size());

		for (int i = 0; i < list.size(); i++) {
			// 把檔名抓一下
			Fileinfo fileinfo = list.get(i);

			Userinfo user = userdao.findById(fileinfo.getUserid());

			String transfilename = "/" + user.getAccount() + "/"
					+ fileinfo.getFilename();
			String filename = fileinfo.getFilename();
			String loggfile = "/" + user.getAccount() + "/"
					+ filename.substring(0, filename.lastIndexOf('.'))
					+ "_LOG.txt";

			String filePattren = filename.substring(0, filename
					.lastIndexOf('_'));

			// 把文件檔名比對一下PATTERN
			String ParserClass = (String) rule.getBatchPattern().get(
					filePattren);

			logger.info("序號:" + i + " 檔名規則:" + filePattren + " ParserClass:"
					+ ParserClass);
			// 參數塞一塞
			// tester.do?
			// DB=CBIDB&
			// CSV=F01_OPEN_CASE_SAMPLE.txt&
			// PSR=tw.gov.cbi.trans.parser.Parser01&
			// LOG=F01_OPEN_CASE_SAMPLE_LOG.tx
			logger.debug("URL:" + rule.getBatchURL());
			logger.debug("DB:" + rule.getBatchDB());
			logger.debug("CSV:" + transfilename);
			logger.debug("PSR:" + ParserClass);
			logger.debug("LOG:" + loggfile);

			ArrayList<NameValuePair> args = new ArrayList<NameValuePair>();
			args.add(new NameValuePair("DB", rule.getBatchDB()));
			args.add(new NameValuePair("CSV", transfilename));
			args.add(new NameValuePair("PSR", ParserClass));
			args.add(new NameValuePair("LOG", loggfile));

			logger.info("傳送資料:" + rule.getBatchURL());
			try {
				List<String> data = basicPostAction(rule.getBatchURL(), args,
						httpclient,rule.getRemoteCharset());

				// data是執行完之後的LOG

				//logger.debug(data);
				
				//檢查一下是不是有檔案產生 系統路徑+LOG檔案
				String logpath=systemVar.getUploadPath()+loggfile;
				if (!FileUtils.fileExists(logpath)){
					logger.info("路徑:"+logpath+" LOG文件沒有產生!");
				}else{
					//logger.info("路徑:"+logpath+" LOG文件產生!");
				}
				
				//Parser一下拿到的資料
				//讀取DATA的最後一行
				logger.info(data.get(data.size()-1));
				
				

			} catch (HttpException e) {
				logger.info("無法連線遠端");
				e.printStackTrace();
			} catch (IOException e) {
				logger.info("系統IO異常,無法連線");
				// TODO Auto-generated catch block
				e.printStackTrace();
			}

			// 使用HTTPCLIENT去做轉檔

		}

	}

	public static List<String> basicPostAction(String url,
			ArrayList<NameValuePair> paramsList, HttpClient httpclient,String Charset)
			throws HttpException, IOException {

		PostMethod post = new PostMethod(url);
		post.setRequestHeader("Connection", "close");
		NameValuePair[] params = new NameValuePair[paramsList.size()];
		paramsList.toArray(params);
		post.addParameters(params);
		try {
			int result = httpclient.executeMethod(post);
			if (result == 200) {
				// return post.getResponseBodyAsString();
				// try {
				// httpClient.executeMethod(get);

				// System.out.println(get.getResponseBodyAsString());
                 List<String> data=new ArrayList<String>();
				BufferedReader reader = new BufferedReader(
						new InputStreamReader(post.getResponseBodyAsStream(),
								"ISO-8859-1"));
				String tmp = null;
				String htmlRet = "";
				while ((tmp = reader.readLine()) != null) {
					data.add( new String(tmp.getBytes("ISO-8859-1"),Charset));
				}

				return data;
			} else {
				throw new RuntimeException("遠端URL錯誤:" + url + ":" + result
						+ ":" + post.getStatusText());
			}
		} finally {
			post.releaseConnection();
			// ((SimpleHttpConnectionManager)httpclient.getHttpConnectionManager()).shutdown();
		}
	}

}
