package net.forgottenschism.gamescreen;

import net.forgottenschism.gui.*;
import net.forgottenschism.gui.bean.*;
import net.forgottenschism.gui.control.Label;
import net.forgottenschism.gui.control.Link;
import net.forgottenschism.gui.control.Textbox;
import net.forgottenschism.gui.impl.AbstractScreen;
import net.forgottenschism.gui.impl.WindowImpl;
import net.forgottenschism.gui.layout.*;

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
		testRelativeAndLinearLayout();
	}

	private void testTableLayout()
	{
		String[][] tableContent = new String[][]
				{
						new String[]{"Components", "Description", "Latest Version", "Released"},
						new String[]{"BCEL", "Byte Code Engineering Library - analyze, create, and manipulate Java class files", "5.2", "2007-06-14"},
						new String[]{"Chain", "Chain of Responsibility pattern implemention.", "1.2", "2008-06-02"}
				};

		String[][] tableContent2 = new String[][]
				{
						new String[]{"Components", "Released"},
						new String[]{"BCEL", "2007-06-14"}
				};

		getMainWindow().setLayout(new TableLayout());

		RelativeLayoutParameters centered = new RelativeLayoutParameters();
		centered.horizontallyCentered();
		centered.verticalyCentered();

		RowLayout row;
		Label cell;

		for(String[] rowContent : tableContent)
		{
			row = new RowLayout();

			for(String cellContent : rowContent)
			{
				cell = new Label(cellContent);
				cell.setLayoutParameters(centered);
				row.addControl(cell);
			}

			addControl(row);
		}
	}

	private void testRelativeAndLinearLayout()
	{
		getMainWindow().setLayout(new RelativeLayout());

		LinearLayout linearLayout = new LinearLayout(10);
		RelativeLayoutParameters linearLayoutPosition = new RelativeLayoutParameters();
		linearLayoutPosition.horizontallyCentered();
		linearLayoutPosition.setTopPosition(10, GraphicalUnit.PERCENT);
		linearLayout.setLayoutParameters(linearLayoutPosition);
		addControl(linearLayout);

		Label title = new Label("Main window");
		linearLayout.addControl(title);

		Textbox textbox = new Textbox(10,1);
		linearLayout.addControl(textbox);

		Link link = new Link("Show dialog", new SelectionListener()
		{
			@Override
			public void handleSelect(Control control)
			{
				dialog.show();
			}
		});
		linearLayout.addControl(link);
	}

	private void testLinearLayout()
	{
		getMainWindow().setLayout(new LinearLayout(Orientation2d.VERTICAL, 50));

		Label title = new Label("Main window");
		addControl(title);

		Textbox textbox = new Textbox(10,1);
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

	private void testRelativeLayout()
	{
		getMainWindow().setLayout(new RelativeLayout());

		Label title = new Label("Main window");
		RelativeLayoutParameters titlePosition = new RelativeLayoutParameters();
		titlePosition.setTopPosition(10, GraphicalUnit.PERCENT);
		titlePosition.horizontallyCentered();
		title.setLayoutParameters(titlePosition);
		addControl(title);

		Textbox textbox = new Textbox(10,1);
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

		Textbox textbox2 = new Textbox(10,1);
		textbox2.setPosition(new Position2d(50, 100));
		dialog.addControl(textbox2);

		Textbox textbox3 = new Textbox(10,1);
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
