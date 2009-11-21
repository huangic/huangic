package idv.trans.struts.action;

import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.util.HashMap;

import org.apache.struts2.ServletActionContext;
import org.dom4j.Document;
import org.dom4j.DocumentException;
import org.dom4j.Element;
import org.dom4j.XPath;
import org.dom4j.io.SAXReader;
import org.dom4j.io.XMLWriter;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class Install {
	private static Logger logger = LoggerFactory.getLogger(Install.class);

	Document xmlSystem;
	Document xmlDB;
	Document xmlBatchRule;

	String dbstring;
	String dbaccount;
	String dbpassword;
	String dbschema;
	
	String uploadpath;
	String backuppath;
	String logpath;
	
	
	String batchurl;
	String batchcharset;

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
		this.dbaccount=selectElement(xmlDB,"//s:bean[@id='baseDataSource']/s:property[@name='username']").valueOf("@value");
        this.dbpassword=selectElement(xmlDB,"//s:bean[@id='baseDataSource']/s:property[@name='password']").valueOf("@value");
        this.dbschema=selectElement(xmlDB,"//s:bean[@id='baseSessionFactory']/s:property[@name='hibernateProperties']/s:props/s:prop[@key='hibernate.default_schema']").getText();
		
         this.backuppath=selectElement(xmlSystem,"//s:bean[@id='SystemVar']/s:property[@name='backupPath']/s:value").getText();
        this.logpath=selectElement(xmlSystem,"//s:bean[@id='SystemVar']/s:property[@name='logPath']/s:value").getText();
        this.uploadpath=selectElement(xmlSystem,"//s:bean[@id='SystemVar']/s:property[@name='uploadPath']/s:value").getText();
        
        this.batchurl=selectElement(xmlBatchRule,"//s:bean[@id='BatchRule']/s:property[@name='batchURL']/s:value").getText();
        this.batchcharset=selectElement(xmlBatchRule,"//s:bean[@id='BatchRule']/s:property[@name='remoteCharset']/s:value").getText();
        
        
	}

	public static void saveXml(Document xmldoc, String filename) {

		try {
			// lets write to a file
			XMLWriter writer = new XMLWriter(new FileWriter(filename));

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

			logger.debug(document.asXML());

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
}
