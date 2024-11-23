package four.pda.ui;

import android.view.MotionEvent;

import uk.co.senab.photoview.DefaultOnDoubleTapListener;
import uk.co.senab.photoview.PhotoViewAttacher;

/**
 * Created by pavel on 11/09/16.
 */
public class DoubleTapListener extends DefaultOnDoubleTapListener {

	private PhotoViewAttacher attacher;

    public DoubleTapListener(PhotoViewAttacher attacher) {
        super(attacher);
        this.attacher = attacher;
    }

    @Override
    public boolean onDoubleTap(MotionEvent ev) {

		if (attacher == null)
            return false;

        try {
            float scale = attacher.getScale();
            float x = ev.getX();
            float y = ev.getY();

			if (scale < attacher.getMediumScale()) {
                attacher.setScale(attacher.getMediumScale(), x, y, true);
            } else {
                attacher.setScale(attacher.getMinimumScale(), x, y, true);
            }
        } catch (ArrayIndexOutOfBoundsException e) {
            // Can sometimes happen when getX() and getY() is called
        }

        return true;
    }

}
