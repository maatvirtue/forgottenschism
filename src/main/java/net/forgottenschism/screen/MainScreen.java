package net.forgottenschism.screen;

import net.forgottenschism.gui.Position2d;
import net.forgottenschism.gui.Window;
import net.forgottenschism.gui.control.Label;
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

        Window window = new WindowImpl(this);
        window.setPosition(new Position2d(150, 100));
        window.setBackgroundColor(Color.red);

        Label label2 = new Label("Window 1");
        label2.setPosition(new Position2d(50, 50));

        window.addControl(label2);
        window.show();
    }
}
