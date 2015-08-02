package net.forgottenschism.gui;

import net.forgottenschism.engine.GameComponent;

public interface Screen extends GameComponent, InputCapable
{
	void setScreenSize(Size2d size);

	Size2d getScreenSize();

	void showWindow(Window window);

	void closeWindow(Window window);

	void enterScreen();

	void pauseScreen();

	void resumeScreen();

	void leaveScreen();

	void addControl(Control control);

	void removeControl(Control control);

	void removeAllControl();
}
