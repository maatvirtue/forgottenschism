package net.forgottenschism.gui;

import net.forgottenschism.gui.focus.FocusCycleRoot;
import net.forgottenschism.gui.layout.Layout;
import org.newdawn.slick.Color;

public interface Window extends GuiComponent
{
    boolean isMainWindow();

    void show();

    void close();

    void setLayout(Layout layout);

	Layout getLayout();

	void addControl(Control control);

	void removeControl(Control control);

	void removeAllControl();

    FocusCycleRoot getFocusCycleRoot();

    Color getBackgroundColor();

    void setBackgroundColor(Color backgroundColor);
}
