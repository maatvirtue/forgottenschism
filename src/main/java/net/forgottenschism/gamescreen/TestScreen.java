package net.forgottenschism.gamescreen;

import net.forgottenschism.gui.*;
import net.forgottenschism.gui.bean.*;
import net.forgottenschism.gui.control.Label;
import net.forgottenschism.gui.control.Link;
import net.forgottenschism.gui.control.MapControl;
import net.forgottenschism.gui.control.Textbox;
import net.forgottenschism.gui.impl.AbstractScreen;
import net.forgottenschism.gui.impl.WindowImpl;
import net.forgottenschism.gui.layout.*;

import net.forgottenschism.world.Coordinate;
import net.forgottenschism.world.RegionMap;
import net.forgottenschism.world.Terrain;
import net.forgottenschism.world.Tile;
import org.newdawn.slick.Color;

import java.util.Random;

public class TestScreen extends AbstractScreen
{
	private Window dialog;

	public TestScreen()
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
		testMapControl();
	}

	private void testMapControl()
	{
		getMainWindow().setLayout(new RelativeLayout());

		RelativeLayoutParameters allscreen = new RelativeLayoutParameters();
		allscreen.setWidth(new GraphicMeasure(100, GraphicalUnit.PERCENT));
		allscreen.setHeight(new GraphicMeasure(100, GraphicalUnit.PERCENT));

		MapControl map = new MapControl(generateRandomMap(50, 50));
		map.setLayoutParameters(allscreen);
		addControl(map);
	}

	private RegionMap generateRandomMap(int width, int height)
	{
		RegionMap regionMap = new RegionMap(width, height);
		Random random = new Random();

		for(int e = 0; e<width; e++)
			for(int i = 0; i<height; i++)
			{
				if(random.nextBoolean())
					regionMap.putTile(new Coordinate(i, e), new Tile(Terrain.BLUE));
				else
					regionMap.putTile(new Coordinate(i, e), new Tile(Terrain.RED));
			}

		return regionMap;
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

		Textbox textbox = new Textbox(10, 5);
		linearLayout.addControl(textbox);

		Link link = new Link("Show dialog", (control) -> dialog.show());
		linearLayout.addControl(link);
	}

	private void testLinearLayout()
	{
		getMainWindow().setLayout(new LinearLayout(Orientation2d.VERTICAL, 50));

		Label title = new Label("Main window");
		addControl(title);

		Textbox textbox = new Textbox(10);
		addControl(textbox);

		Link link = new Link("Show dialog", (control) -> dialog.show());
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

		Textbox textbox = new Textbox(10);
		RelativeLayoutParameters textboxPosition = new RelativeLayoutParameters();
		textboxPosition.setTopPosition(20, GraphicalUnit.PERCENT);
		textboxPosition.horizontallyCentered();
		textbox.setLayoutParameters(textboxPosition);
		addControl(textbox);

		Link link = new Link("Show dialog", (control) -> dialog.show());
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

		Link link = new Link("Close dialog", (control) -> dialog.close());
		link.setPosition(new Position2d(50, 200));
		dialog.addControl(link);

		return dialog;
	}
}
