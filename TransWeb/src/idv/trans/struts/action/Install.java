package idv.trans.struts.action;

import idv.trans.model.Message;
import idv.trans.service.admin.UserService;
import idv.trans.util.SpringUtil;

import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.net.URL;
import java.sql.Connection;
import java.util.HashMap;

import org.apache.commons.dbcp.BasicDataSource;
import org.apache.struts2.ServletActionContext;
import org.codehaus.plexus.util.FileUtils;
import org.dom4j.Document;
import org.dom4j.DocumentException;
import org.dom4j.Element;
import org.dom4j.XPath;
import org.dom4j.io.SAXReader;
import org.dom4j.io.XMLWriter;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.orm.hibernate3.LocalSessionFactoryBean;

public class Install {
	private static Logger logger = LoggerFactory.getLogger(Install.class);

	Document xmlSystem;
	Document xmlDB;
	Document xmlBatchRule;

	Message message;

	boolean createdb;
	String executemessage;
	String dbstring;
	String dbaccount;
	String dbpassword;
	String dbschema;

	String uploadpath;
	String backuppath;
	String logpath;

	String batchurl;
	String batchcharset;

	private void setSchema() throws Exception {
		LocalSessionFactoryBean sessionfactory = (LocalSessionFactoryBean) SpringUtil
				.getBean("&baseSessionFactory");

		sessionfactory.getHibernateProperties().setProperty(
				"hibernate.default_schema", this.dbschema);
		try {
			sessionfactory.afterPropertiesSet();
			// sessionfactory.createDatabaseSchema();
		} catch (Exception e) {
			// TODO Auto-generated catch block
			this.executemessage += e.toString();
			throw new Exception("無法設定SCHEMA");

		}

	}

	private void testDbConnect() throws Exception {
		BasicDataSource ds = (BasicDataSource) SpringUtil
				.getBean("baseDataSource");

		String oldDbString = ds.getUrl();
		String oldDbAccount = ds.getUsername();
		String oldDbPassword = ds.getPassword();

		ds.setUrl(this.dbstring);
		ds.setUsername(this.dbaccount);
		ds.setPassword(this.dbpassword);

		ds.close();

		try {
			Connection connection = ds.getConnection();

			connection.close();

		} catch (Exception ex) {
			// 還原設定
			ds.setUrl(oldDbString);
			ds.setUsername(oldDbAccount);
			ds.setPassword(oldDbPassword);
			ds.close();

			this.executemessage = ex.toString();
			throw new Exception("無法連線資料庫!!");
		}

	}

	private void importDB() throws Exception {
		LocalSessionFactoryBean sessionfactory = (LocalSessionFactoryBean) SpringUtil
				.getBean("&baseSessionFactory");

		try {

			sessionfactory.createDatabaseSchema();
		} catch (Exception e) {
			// TODO Auto-generated catch block
			this.executemessage += e.toString();
			throw new Exception("無法初始資料庫");

		}

	}

	public String save() {
		init();

		// 測試參數

		// 測試DB
		try {
			testDbConnect();

			// 設定SCHEMA;
			setSchema();

			// 建立初始化DB;
			if (this.isCreatedb()) {
				importDB();
				UserService service = (UserService) SpringUtil
						.getBean("UserService");
				service.initUser();

			}

			// 寫入XML設定

			saveAllVar();

			this.message = new Message("修改成功，請確認登入正常後，重新啟動");
		} catch (Exception e) {
			// TODO Auto-generated catch block
			this.message = new Message(e.getMessage());
			e.printStackTrace();
		}

		return "SUCCESS";

	}

	public void init() {
		// read xml to object

		String rootdir = null;
		try {
			rootdir = ServletActionContext.getServletContext().getRealPath("/");
		} catch (Exception ex) {

		}

		if (rootdir == null) {
			rootdir = "/Users/huangic/Documents/jetty-6.1.22/webapps/TransWeb";
		}

		logger.debug(rootdir);
		String classesdir = rootdir + "/WEB-INF/classes/";
		this.xmlBatchRule = ReadXML(classesdir
				+ "applicationContext-BatchRule.xml");
		this.xmlDB = ReadXML(classesdir + "applicationContext-DB.xml");
		this.xmlSystem = ReadXML(classesdir + "applicationContext-system.xml");

	}

	public void setAllVar() {

		Element element = selectElement(xmlDB,
				"//s:bean[@id='baseDataSource']/s:property[@name='url']");
		this.dbstring = element.valueOf("@value");
		this.dbaccount = selectElement(xmlDB,
				"//s:bean[@id='baseDataSource']/s:property[@name='username']")
				.valueOf("@value");
		this.dbpassword = selectElement(xmlDB,
				"//s:bean[@id='baseDataSource']/s:property[@name='password']")
				.valueOf("@value");
		this.dbschema = selectElement(
				xmlDB,
				"//s:bean[@id='baseSessionFactory']/s:property[@name='hibernateProperties']/s:props/s:prop[@key='hibernate.default_schema']")
				.getText();

		this.backuppath = selectElement(xmlSystem,
				"//s:bean[@id='SystemVar']/s:property[@name='backupPath']/s:value")
				.getText();
		this.logpath = selectElement(xmlSystem,
				"//s:bean[@id='SystemVar']/s:property[@name='logPath']/s:value")
				.getText();
		this.uploadpath = selectElement(xmlSystem,
				"//s:bean[@id='SystemVar']/s:property[@name='uploadPath']/s:value")
				.getText();

		this.batchurl = selectElement(xmlBatchRule,
				"//s:bean[@id='BatchRule']/s:property[@name='batchURL']/s:value")
				.getText();
		this.batchcharset = selectElement(xmlBatchRule,
				"//s:bean[@id='BatchRule']/s:property[@name='remoteCharset']/s:value")
				.getText();

	}

	public void saveAllVar() {

		selectElement(xmlDB,
				"//s:bean[@id='baseDataSource']/s:property[@name='url']")
				.attribute("value").setValue(this.dbstring);

		selectElement(xmlDB,
		"//s:bean[@id='baseDataSource']/s:property[@name='username']")
		.attribute("value").setValue(this.dbaccount);

		selectElement(xmlDB,
		"//s:bean[@id='baseDataSource']/s:property[@name='password']")
		.attribute("value").setValue(this.dbpassword);

		selectElement(xmlDB,
				"//s:bean[@id='baseSessionFactory']/s:property[@name='hibernateProperties']/s:props/s:prop[@key='hibernate.default_schema']")
		.setText(this.dbschema);
		
		
		
		
		
		
		
		
		

		selectElement(xmlSystem,
				"//s:bean[@id='SystemVar']/s:property[@name='backupPath']/s:value").setText(this.backuppath);
				
		selectElement(xmlSystem,
				"//s:bean[@id='SystemVar']/s:property[@name='logPath']/s:value")
				.setText(this.logpath);
		selectElement(xmlSystem,
				"//s:bean[@id='SystemVar']/s:property[@name='uploadPath']/s:value")
				.setText(this.uploadpath);

		selectElement(xmlBatchRule,
				"//s:bean[@id='BatchRule']/s:property[@name='batchURL']/s:value")
				.setText(this.batchurl);
		selectElement(xmlBatchRule,
				"//s:bean[@id='BatchRule']/s:property[@name='remoteCharset']/s:value")
				.setText(this.batchcharset);

		//SAVE XML;
		saveXml(xmlDB);
		saveXml(xmlBatchRule);
		saveXml(xmlSystem);
		
		
		
		
	}

	public static void saveXml(Document xmldoc) {

	
		
		
		
		try {
			File xmlfile=FileUtils.toFile(new URL(xmldoc.getName()));
			 
			//編碼問題 所以要多這轉換UTF8
			java.io.Writer wr=new java.io.OutputStreamWriter(new java.io.FileOutputStream(xmlfile),"UTF-8");  
			
			// lets write to a file
			XMLWriter writer = new XMLWriter(wr);

			writer.write(xmldoc);
			writer.close();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

	}

	public String index() {
		// 讀xml檔案
		init();
		setAllVar();

		return "SUCCESS";
	}

	public static Document ReadXML(String filename) {
		File file = new File(filename);
		SAXReader reader = new SAXReader();

		// try {
		// reader.setFeature(Constants.XERCES_FEATURE_PREFIX +
		// Constants.LOAD_EXTERNAL_DTD_FEATURE, false);
		// } catch (SAXException e) {
		// TODO Auto-generated catch block
		// e.printStackTrace();
		// }

		Document document;
		try {
			document = reader.read(file);

			// logger.debug(document.asXML());

			return document;
		} catch (DocumentException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return null;

	}

	public static Element selectElement(Document doc, String xpath) {
		HashMap xmlMap = new HashMap();
		xmlMap.put("s", "http://www.springframework.org/schema/beans");
		// 在每个节点前面加上自定义命名空间前缀
		XPath x = doc.createXPath(xpath);
		x.setNamespaceURIs(xmlMap);
		return (Element) x.selectSingleNode(doc);

	}

	public String getDbstring() {
		return dbstring;
	}

	public void setDbstring(String dbstring) {
		this.dbstring = dbstring;
	}

	public String getDbaccount() {
		return dbaccount;
	}

	public void setDbaccount(String dbaccount) {
		this.dbaccount = dbaccount;
	}

	public String getDbpassword() {
		return dbpassword;
	}

	public void setDbpassword(String dbpassword) {
		this.dbpassword = dbpassword;
	}

	public String getDbschema() {
		return dbschema;
	}

	public void setDbschema(String dbschema) {
		this.dbschema = dbschema;
	}

	public String getUploadpath() {
		return uploadpath;
	}

	public void setUploadpath(String uploadpath) {
		this.uploadpath = uploadpath;
	}

	public String getBackuppath() {
		return backuppath;
	}

	public void setBackuppath(String backuppath) {
		this.backuppath = backuppath;
	}

	public String getLogpath() {
		return logpath;
	}

	public void setLogpath(String logpath) {
		this.logpath = logpath;
	}

	public String getBatchurl() {
		return batchurl;
	}

	public void setBatchurl(String batchurl) {
		this.batchurl = batchurl;
	}

	public String getBatchcharset() {
		return batchcharset;
	}

	public void setBatchcharset(String batchcharset) {
		this.batchcharset = batchcharset;
	}

	public Message getMessage() {
		return message;
	}

	public void setMessage(Message message) {
		this.message = message;
	}

	public String getExecutemessage() {
		return executemessage;
	}

	public void setExecutemessage(String executemessage) {
		this.executemessage = executemessage;
	}

	public boolean isCreatedb() {
		return createdb;
	}

	public void setCreatedb(boolean createdb) {
		this.createdb = createdb;
	}
}
