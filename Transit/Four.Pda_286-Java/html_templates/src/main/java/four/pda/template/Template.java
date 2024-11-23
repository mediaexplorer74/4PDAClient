package four.pda.template;

import com.github.mustachejava.DefaultMustacheFactory;
import com.github.mustachejava.Mustache;
import com.github.mustachejava.MustacheFactory;

import org.apache.commons.io.output.StringBuilderWriter;

import java.io.IOException;
import java.io.InputStreamReader;
import java.lang.ref.WeakReference;
import java.util.HashMap;

/**
 * Created by asavinova on 26/07/16.
 */
public class Template {

	protected WeakReference<MustacheFactory> factoryRef;
	protected WeakReference<Mustache> templateRef;

	public String make(String source) {

		HashMap<String, String> scopes = new HashMap<>();
		scopes.put("content", source);

		StringBuilderWriter writer = new StringBuilderWriter();
		getTemplate().execute(writer, scopes);

		return writer.toString();
	}

	protected MustacheFactory getFactory() {
		if (factoryRef == null || factoryRef.get() == null) {
			factoryRef = new WeakReference<MustacheFactory>(new DefaultMustacheFactory());
		}
		return factoryRef.get();
	}

	protected Mustache getTemplate() {

		String className = getClass().getSimpleName();

		if (templateRef == null || templateRef.get() == null) {
			InputStreamReader reader = new InputStreamReader(getClass().getResourceAsStream(className + ".htm"));
			try {
				Mustache mustache = getFactory().compile(reader, className);
				templateRef = new WeakReference<>(mustache);
			} finally {
				try {
					reader.close();
				} catch (IOException e) {
					throw new RuntimeException(e);
				}
			}
		}

		return templateRef.get();
	}

}
