package net.forgottenschism.gui.event;

import net.forgottenschism.gui.Control;

public interface ValueChangeListener<T>
{
	void valueChanged(Control control, T newValue);
}
