package idv.trans.model;

/**
 * Download entity.
 * 
 * @author MyEclipse Persistence Tools
 */

public class Download implements java.io.Serializable {

	// Fields

	private Long downloadid;
	private String name;
	private String filename;
	private String filepath;
	private Short priority;

	// Constructors

	/** default constructor */
	public Download() {
	}

	/** full constructor */
	public Download(String name, String filename, String filepath,
			Short priority) {
		this.name = name;
		this.filename = filename;
		this.filepath = filepath;
		this.priority = priority;
	}

	// Property accessors

	public Long getDownloadid() {
		return this.downloadid;
	}

	public void setDownloadid(Long downloadid) {
		this.downloadid = downloadid;
	}

	public String getName() {
		return this.name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getFilename() {
		return this.filename;
	}

	public void setFilename(String filename) {
		this.filename = filename;
	}

	public String getFilepath() {
		return this.filepath;
	}

	public void setFilepath(String filepath) {
		this.filepath = filepath;
	}

	public Short getPriority() {
		return this.priority;
	}

	public void setPriority(Short priority) {
		this.priority = priority;
	}

}