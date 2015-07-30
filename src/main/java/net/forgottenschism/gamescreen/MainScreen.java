package net.forgottenschism.gamescreen;

import net.forgottenschism.gui.*;
import net.forgottenschism.gui.control.Label;
import net.forgottenschism.gui.control.Link;
import net.forgottenschism.gui.control.Textbox;
import net.forgottenschism.gui.impl.AbstractScreen;
import net.forgottenschism.gui.impl.WindowImpl;
import org.newdawn.slick.Color;

public class MainScreen extends AbstractScreen
{
	private Window dialog;

    public MainScreen()
    {
		setupGui();
	}

	private void setupGui()
	{
		dialog = buildDialog();

		setupMainWindow();
	}

	private void setupMainWindow()
	{
		Label label1 = new Label("Main window");
		label1.setPosition(new Position2d(50, 50));
		addToMainWindow(label1);

		Textbox textbox1 = new Textbox(10);
		textbox1.setPosition(new Position2d(50, 100));
		addToMainWindow(textbox1);

		Link link = new Link("Show dialog", new SelectionListener()
		{
			@Override
			public void handleSelect(Control control)
			{
				dialog.show();
			}
		});
		link.setPosition(new Position2d(50, 150));
		addToMainWindow(link);
	}

	private Window buildDialog()
	{
		final Window dialog = new WindowImpl(this);
		dialog.setBackgroundColor(Color.lightGray);
		dialog.setPosition(new Position2d(150, 150));
		dialog.setSize(new Size2d(400, 400));

		Textbox textbox2 = new Textbox(10);
		textbox2.setPosition(new Position2d(50, 100));
		dialog.addControl(textbox2);

		Textbox textbox3 = new Textbox(10);
		textbox3.setPosition(new Position2d(50, 150));
		dialog.addControl(textbox3);

		Link link = new Link("Close dialog", new SelectionListener()
		{
			@Override
			public void handleSelect(Control control)
			{
				dialog.close();
			}
		});
		link.setPosition(new Position2d(50, 200));
		dialog.addControl(link);

		return dialog;
	}
}
