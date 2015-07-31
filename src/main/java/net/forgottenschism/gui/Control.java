package net.forgottenschism.gui;

public interface Control extends GuiComponent
{
	boolean isEnabled();

	void setEnabled(boolean enabled);

	boolean isVisible();

	void setVisible(boolean visible);

	boolean canHaveFocus();

	Size2d getPreferredSize();
}
