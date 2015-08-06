package net.forgottenschism.gamescreen;

import net.forgottenschism.gui.*;
import net.forgottenschism.gui.bean.*;
import net.forgottenschism.gui.control.Label;
import net.forgottenschism.gui.control.Link;
import net.forgottenschism.gui.control.Textbox;
import net.forgottenschism.gui.impl.AbstractScreen;
import net.forgottenschism.gui.impl.WindowImpl;
import net.forgottenschism.gui.layout.LinearLayout;
import net.forgottenschism.gui.layout.RelativeLayout;
import net.forgottenschism.gui.layout.RelativeLayoutParameters;
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
		linearLayoutTest();
	}

	private void linearLayoutTest()
	{
		getMainWindow().setLayout(new LinearLayout(Orientation2d.VERTICAL, 50));

		Label title = new Label("Main window");
		addControl(title);

		Textbox textbox = new Textbox(10);
		addControl(textbox);

		Link link = new Link("Show dialog", new SelectionListener()
		{
			@Override
			public void handleSelect(Control control)
			{
				dialog.show();
			}
		});
		addControl(link);
	}

	private void relativeLayoutTest()
	{
		getMainWindow().setLayout(new RelativeLayout());

		Label title = new Label("Main window");
		RelativeLayoutParameters titlePosition = new RelativeLayoutParameters();
		titlePosition.setTopPosition(10, GraphicalUnit.PERCENT);
		titlePosition.horizontallyCentered();
		title.setLayoutParameters(titlePosition);
		addControl(title);

		Textbox textbox = new Textbox(10);
		RelativeLayoutParameters textboxPosition = new RelativeLayoutParameters();
		textboxPosition.setTopPosition(20, GraphicalUnit.PERCENT);
		textboxPosition.horizontallyCentered();
		textbox.setLayoutParameters(textboxPosition);
		addControl(textbox);

		Link link = new Link("Show dialog", new SelectionListener()
		{
			@Override
			public void handleSelect(Control control)
			{
				dialog.show();
			}
		});
		RelativeLayoutParameters linkPosition = new RelativeLayoutParameters();
		linkPosition.setBottomPosition(10, GraphicalUnit.PERCENT);
		linkPosition.horizontallyCentered();
		link.setLayoutParameters(linkPosition);
		addControl(link);
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
