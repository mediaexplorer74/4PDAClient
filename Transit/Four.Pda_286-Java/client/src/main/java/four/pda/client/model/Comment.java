package four.pda.client.model;

import java.util.Date;

/**
 * Created by asavinova on 02/11/15.
 */
public class Comment extends AbstractComment {

	private User user;
	private Date date;
	private Karma karma;

	public User getUser() {
		return user;
	}

	public void setUser(User user) {
		this.user = user;
	}

	public Date getDate() {
		return date;
	}

	public void setDate(Date date) {
		this.date = date;
	}

	public Karma getKarma() {
		return karma;
	}

	public void setKarma(Karma karma) {
		this.karma = karma;
	}

	public static class Karma {

		private CanLike canLike;
		private boolean unknown2;
		private int unknown3;
		private int likesCount;

		public CanLike getCanLike() {
			return canLike;
		}

		public void setCanLike(CanLike canLike) {
			this.canLike = canLike;
		}

		public boolean getUnknown2() {
			return unknown2;
		}

		public void setUnknown2(boolean unknown2) {
			this.unknown2 = unknown2;
		}

		public int getUnknown3() {
			return unknown3;
		}

		public void setUnknown3(int unknown3) {
			this.unknown3 = unknown3;
		}

		public int getLikesCount() {
			return likesCount;
		}

		public void setLikesCount(int likesCount) {
			this.likesCount = likesCount;
		}

	}

	public enum CanLike {

		CAN(0),
		ALREADY_LIKED(1),
		CANT(2);

		private int serverValue;

		CanLike(int serverValue) {
			this.serverValue = serverValue;
		}

		public static CanLike fromServerValue(int value) {
			for (CanLike availability : values()) {
				if (availability.serverValue == value) {
					return availability;
				}
			}
			throw new IllegalArgumentException("Can't find like availability for serverValue " + value);
		}

		public int serverValue() {
			return serverValue;
		}

	}

}
