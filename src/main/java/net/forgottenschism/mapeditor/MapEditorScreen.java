package net.forgottenschism.mapeditor;

import net.forgottenschism.gui.Window;
import net.forgottenschism.gui.bean.GraphicMeasure;
import net.forgottenschism.gui.bean.GraphicalUnit;
import net.forgottenschism.gui.bean.Orientation2d;
import net.forgottenschism.gui.bean.Size2d;
import net.forgottenschism.gui.control.*;
import net.forgottenschism.gui.event.KeyEvent;
import net.forgottenschism.gui.impl.AbstractScreen;
import net.forgottenschism.gui.impl.WindowImpl;
import net.forgottenschism.gui.layout.*;
import net.forgottenschism.gui.theme.ColorTheme;
import net.forgottenschism.gui.theme.ColorThemeElement;
import net.forgottenschism.gui.theme.Theme;
import net.forgottenschism.world.RegionMap;
import net.forgottenschism.world.Terrain;
import net.forgottenschism.world.Tile;

import org.newdawn.slick.Color;
import org.newdawn.slick.Input;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.io.File;
import java.util.Arrays;

public class MapEditorScreen extends AbstractScreen
{
	private static Logger logger = LoggerFactory.getLogger(MapEditorScreen.class);
	private static ColorTheme DEFAULT_COLOR_THEME = Theme.getDefaultTheme().getColorTheme();
	private static Terrain DEFAULT_SELECTED_TERRAIN = Terrain.RED;
	private static Terrain DEFAULT_NEWMAP_TERRAIN = Terrain.BLUE;

	private RegionMap regionMap;
	private File mapFile;

	private MapControl mapControl;
	private Picture selectedTerrainPicture;
	private Spinner<Terrain> terrainSpinner;
	private Label mapFileLabel;
	private Window newMapWindow;
	private Textbox newMapWidth;
	private Textbox newMapHeight;

	public MapEditorScreen()
	{
		loadDefaultMap();

		setupGui();
	}

	private void loadDefaultMap()
	{
		regionMap = new RegionMap(50, 50);
	}

	private void setupGui()
	{
		setupMainWindow();
		setupNewMapWindow();
	}

	private void setupMainWindow()
	{
		getMainWindow().setLayout(new LinearLayout(Orientation2d.HORIZONTAL, 0));

		mapControl = new MapControl(regionMap);
		LinearLayoutParameters mapPosition = new LinearLayoutParameters();
		mapPosition.setWeight(1f);
		mapControl.setLayoutParameters(mapPosition);
		mapControl.setSelectionListener(coordinate ->
		{
			regionMap.getTile(coordinate).setTerrain(terrainSpinner.getSelection());
		});
		addControl(mapControl);

		RelativeLayout sideMenu = new RelativeLayout();
		LinearLayoutParameters panelPosition = new LinearLayoutParameters();
		panelPosition.setLength(400);
		sideMenu.setLayoutParameters(panelPosition);
		addControl(sideMenu);

		setupSideMenu(sideMenu);
	}

	private void setupNewMapWindow()
	{
		newMapWindow = new WindowImpl(this);
		newMapWindow.setBackgroundColor(Color.gray);
		newMapWindow.setLayout(new RelativeLayout());

		Label newMapTitle = new Label("New Map");
		RelativeLayoutParameters topCenter = new RelativeLayoutParameters();
		topCenter.horizontallyCentered();
		topCenter.setTopPosition(10, GraphicalUnit.PIXEL);
		newMapTitle.setLayoutParameters(topCenter);
		newMapTitle.setTextColor(DEFAULT_COLOR_THEME.getColor(ColorThemeElement.LABEL_TITLE));
		newMapWindow.addControl(newMapTitle);

		TableLayout centerPanel = new TableLayout();
		RelativeLayoutParameters center = new RelativeLayoutParameters();
		center.horizontallyCentered();
		center.verticalyCentered();
		centerPanel.setLayoutParameters(center);
		newMapWindow.addControl(centerPanel);

		setupNewMapWindowCenterPanel(centerPanel);

		Link applyNewMap = new Link("Apply new map");
		RelativeLayoutParameters bottomCenter = new RelativeLayoutParameters();
		bottomCenter.horizontallyCentered();
		bottomCenter.setBottomPosition(10, GraphicalUnit.PIXEL);
		applyNewMap.setLayoutParameters(bottomCenter);
		applyNewMap.setSelectionListener(control ->
		{
			Size2d newMapSize = getNewMapSizeFromGui();

			applyNewMap(newMapSize, DEFAULT_NEWMAP_TERRAIN);
		});
		newMapWindow.addControl(applyNewMap);
	}

	private void setupNewMapWindowCenterPanel(TableLayout centerPanel)
	{
		RelativeLayoutParameters centered = new RelativeLayoutParameters();
		centered.horizontallyCentered();
		centered.verticalyCentered();

		RowLayout widthRow = new RowLayout();
		centerPanel.addControl(widthRow);

		Label width = new Label("Width");
		width.setLayoutParameters(centered);
		widthRow.addControl(width);

		newMapWidth = new Textbox(2);
		newMapWidth.setLayoutParameters(centered);
		widthRow.addControl(newMapWidth);

		RowLayout heightRow = new RowLayout();
		centerPanel.addControl(heightRow);

		Label height = new Label("Height");
		height.setLayoutParameters(centered);
		heightRow.addControl(height);

		newMapHeight = new Textbox(2);
		newMapHeight.setLayoutParameters(centered);
		heightRow.addControl(newMapHeight);
	}

	private Size2d getNewMapSizeFromGui()
	{
		int width;
		int height;

		try
		{
			width = Integer.parseInt(newMapWidth.getText());
		}
		catch(NumberFormatException e)
		{
			width = 10;
		}

		try
		{
			height = Integer.parseInt(newMapHeight.getText());
		}
		catch(NumberFormatException e)
		{
			height = 10;
		}

		return new Size2d(width, height);
	}

	private void applyNewMap(Size2d newMapSize, Terrain terrain)
	{
		regionMap = new RegionMap(newMapSize.getWidth(), newMapSize.getHeight(), terrain);

		mapControl.setRegionMap(regionMap);

		newMapWindow.close();
	}

	private void setupSideMenu(RelativeLayout sideMenu)
	{
		LinearLayout centerSideMenuPanel = new LinearLayout(15);
		RelativeLayoutParameters terrainListPosition = new RelativeLayoutParameters();
		terrainListPosition.horizontallyCentered();
		terrainListPosition.setHeight(new GraphicMeasure(100, GraphicalUnit.PERCENT));
		centerSideMenuPanel.setLayoutParameters(terrainListPosition);
		sideMenu.addControl(centerSideMenuPanel);

		setupTerrainMenu(centerSideMenuPanel);
		setupMapFileMenu(centerSideMenuPanel);
		setupControlsMenu(centerSideMenuPanel);
	}

	private void setupMapFileMenu(LinearLayout centerSideMenuPanel)
	{
		Label mapFileTitle = new Label("Map File");
		mapFileTitle.setTextColor(DEFAULT_COLOR_THEME.getColor(ColorThemeElement.LABEL_TITLE));
		centerSideMenuPanel.addControl(mapFileTitle);

		mapFileLabel = new Label("None");
		centerSideMenuPanel.addControl(mapFileLabel);
	}

	private void setupControlsMenu(LinearLayout centerSideMenuPanel)
	{
		Label controlsLabel = new Label("Controls");
		controlsLabel.setTextColor(DEFAULT_COLOR_THEME.getColor(ColorThemeElement.LABEL_TITLE));
		centerSideMenuPanel.addControl(controlsLabel);

		Label moveSelectionLabel = new Label("ARROWS Move selection");
		centerSideMenuPanel.addControl(moveSelectionLabel);

		Label changeTerrainLabel = new Label("LSHIFT/RSHIFT Change terrain");
		centerSideMenuPanel.addControl(changeTerrainLabel);

		Label applyTerrainLabel = new Label("SPACE Apply terrain");
		centerSideMenuPanel.addControl(applyTerrainLabel);

		Label openLoadSaveMenuLabel = new Label("ENTER Open load/save menu");
		centerSideMenuPanel.addControl(openLoadSaveMenuLabel);

		Label newTerrainMenuLabel = new Label("N New map");
		centerSideMenuPanel.addControl(newTerrainMenuLabel);
	}

	private void setupTerrainMenu(LinearLayout centerSideMenuPanel)
	{
		Label terrainTitle = new Label("Terrain");
		terrainTitle.setTextColor(DEFAULT_COLOR_THEME.getColor(ColorThemeElement.LABEL_TITLE));
		centerSideMenuPanel.addControl(terrainTitle);

		terrainSpinner = new Spinner();
		terrainSpinner.addAll(Arrays.asList(Terrain.values()));
		terrainSpinner.select(DEFAULT_SELECTED_TERRAIN);
		terrainSpinner.setValueChangeListener((control, selectedTerrain) ->
		{
			selectedTerrainPicture.setImage(selectedTerrain.getImage());
		});
		centerSideMenuPanel.addControl(terrainSpinner);

		selectedTerrainPicture = new Picture();
		LinearLayoutParameters picturePosition = new LinearLayoutParameters(null, Tile.SIZE.getHeight());
		selectedTerrainPicture.setLayoutParameters(picturePosition);
		selectedTerrainPicture.setImage(DEFAULT_SELECTED_TERRAIN.getImage());
		centerSideMenuPanel.addControl(selectedTerrainPicture);
	}

	@Override
	public boolean screenKeyReleased(KeyEvent keyEvent)
	{
		if(getMainWindow().hasFocus())
		{

			if(keyEvent.getKeyCode()==Input.KEY_LSHIFT)
			{
				terrainSpinner.selectPreviousOption();
				return false;
			}
			else if(keyEvent.getKeyCode()==Input.KEY_RSHIFT)
			{
				terrainSpinner.selectNextOption();
				return false;
			}
			else if(keyEvent.getKeyCode()==Input.KEY_N)
			{
				newMapWindow.show();
				return false;
			}
			else if(keyEvent.getKeyCode()==Input.KEY_TAB)
				return false;
		}

		return true;
	}
}
