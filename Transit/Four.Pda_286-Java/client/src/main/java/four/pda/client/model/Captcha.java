package four.pda.client.model;

/**
 * Created by asavinova on 09/02/16.
 */
public class Captcha {

	private String time;
	private String sig;
	private String url;

	public String getTime() {
		return time;
	}

	public void setTime(String time) {
		this.time = time;
	}

	public String getSig() {
		return sig;
	}

	public void setSig(String sig) {
		this.sig = sig;
	}

	public String getUrl() {
		return url;
	}

	public void setUrl(String url) {
		this.url = url;
	}

}
