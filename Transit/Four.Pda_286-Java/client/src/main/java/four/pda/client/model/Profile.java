package four.pda.client.model;

/**
 * Created by asavinova on 19/02/16.
 */
public class Profile {

	private String photo;
	private String login;
	/**
	 * Html-код со страницы профиля, содержащий всю информацию о пользователе.
	 */
	private String info;

	public String getPhoto() {
		return photo;
	}

	public void setPhoto(String photo) {
		this.photo = photo;
	}

	public String getLogin() {
		return login;
	}

	public void setLogin(String login) {
		this.login = login;
	}

	public String getInfo() {
		return info;
	}

	public void setInfo(String info) {
		this.info = info;
	}

}
