package net.forgottenschism.mapeditor;

import net.forgottenschism.gui.bean.GraphicMeasure;
import net.forgottenschism.gui.bean.GraphicalUnit;
import net.forgottenschism.gui.bean.Orientation2d;
import net.forgottenschism.gui.control.Label;
import net.forgottenschism.gui.control.MapControl;
import net.forgottenschism.gui.control.Picture;
import net.forgottenschism.gui.control.Spinner;
import net.forgottenschism.gui.event.KeyEvent;
import net.forgottenschism.gui.impl.AbstractScreen;
import net.forgottenschism.gui.layout.LinearLayout;
import net.forgottenschism.gui.layout.LinearLayoutParameters;
import net.forgottenschism.gui.layout.RelativeLayout;
import net.forgottenschism.gui.layout.RelativeLayoutParameters;
import net.forgottenschism.gui.theme.ColorTheme;
import net.forgottenschism.gui.theme.ColorThemeElement;
import net.forgottenschism.gui.theme.Theme;
import net.forgottenschism.world.Map;
import net.forgottenschism.world.Terrain;
import net.forgottenschism.world.Tile;

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

	private Map map;
	private File mapFile;

	private Picture selectedTerrainPicture;
	private Spinner<Terrain> terrainSpinner;
	private Label mapFileLabel;

	public MapEditorScreen()
	{
		loadDefaultMap();

		setupGui();
	}

	private void loadDefaultMap()
	{
		map = new Map(50, 50);
	}

	private void setupGui()
	{
		setupMainWindow();
	}

	private void setupMainWindow()
	{
		getMainWindow().setLayout(new LinearLayout(Orientation2d.HORIZONTAL, 0));

		MapControl mapControl = new MapControl(map);
		LinearLayoutParameters mapPosition = new LinearLayoutParameters();
		mapPosition.setWeight(1f);
		mapControl.setLayoutParameters(mapPosition);
		mapControl.setSelectionListener(coordinate ->
		{
			map.getTile(coordinate).setTerrain(terrainSpinner.getSelection());
		});
		addControl(mapControl);

		RelativeLayout sideMenu = new RelativeLayout();
		LinearLayoutParameters panelPosition = new LinearLayoutParameters();
		panelPosition.setLength(300);
		sideMenu.setLayoutParameters(panelPosition);
		addControl(sideMenu);

		setupSideMenu(sideMenu);
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
	}

	private void setupMapFileMenu(LinearLayout centerSideMenuPanel)
	{
		Label mapFileTitle = new Label("Map File");
		mapFileTitle.setTextColor(DEFAULT_COLOR_THEME.getColor(ColorThemeElement.LABEL_TITLE));
		centerSideMenuPanel.addControl(mapFileTitle);

		mapFileLabel = new Label("None");
		centerSideMenuPanel.addControl(mapFileLabel);
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
		if(keyEvent.getKeyCode()==Input.KEY_TAB)
			return false;

		return true;
	}
}
