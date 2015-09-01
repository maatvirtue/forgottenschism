package net.forgottenschism.mapeditor;

import net.forgottenschism.gui.bean.Orientation2d;
import net.forgottenschism.gui.control.MapControl;
import net.forgottenschism.gui.impl.AbstractScreen;
import net.forgottenschism.gui.layout.LinearLayout;
import net.forgottenschism.gui.layout.LinearLayoutParameters;

public class MapEditorScreen extends AbstractScreen
{
	public MapEditorScreen()
	{
		setupGui();
	}

	private void setupGui()
	{
		getMainWindow().setLayout(new LinearLayout(Orientation2d.HORIZONTAL, 0));

		LinearLayoutParameters mapPosition = new LinearLayoutParameters();
		mapPosition.setWeight(1f);

		LinearLayoutParameters panelPosition = new LinearLayoutParameters();
		panelPosition.setLength(300);

		MapControl map = new MapControl();
		map.setLayoutParameters(mapPosition);
		addControl(map);

		LinearLayout panel = new LinearLayout();
		panel.setLayoutParameters(panelPosition);
		addControl(panel);
	}
}
