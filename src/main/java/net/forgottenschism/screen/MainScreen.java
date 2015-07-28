package net.forgottenschism.screen;

import net.forgottenschism.gui.Position2d;
import net.forgottenschism.gui.Window;
import net.forgottenschism.gui.control.Label;
import net.forgottenschism.gui.control.Textbox;
import net.forgottenschism.gui.impl.AbstractScreen;
import net.forgottenschism.gui.impl.WindowImpl;
import org.newdawn.slick.Color;

public class MainScreen extends AbstractScreen
{
    public MainScreen()
    {
        Label label1 = new Label("Main window");
        label1.setPosition(new Position2d(50, 50));

        addToMainWindow(label1);

		Textbox textbox1 = new Textbox(10);
		textbox1.setPosition(new Position2d(50, 100));
		addToMainWindow(textbox1);

		Textbox textbox2 = new Textbox(10);
		textbox2.setPosition(new Position2d(50, 150));
		addToMainWindow(textbox2);
	}
}
