package net.forgottenschism.gamescreen;

import org.newdawn.slick.Color;

import net.forgottenschism.gui.Control;
import net.forgottenschism.gui.SelectionListener;
import net.forgottenschism.gui.bean.GraphicalUnit;
import net.forgottenschism.gui.control.Label;
import net.forgottenschism.gui.control.Spinner;
import net.forgottenschism.gui.impl.AbstractScreen;
import net.forgottenschism.gui.layout.RelativeLayout;
import net.forgottenschism.gui.layout.RelativeLayoutParameters;
import net.forgottenschism.gui.layout.RowLayout;
import net.forgottenschism.gui.layout.TableLayout;

public class SettingsScreen extends AbstractScreen
{
	public SettingsScreen()
	{
		setupGui();
	}

	private void setupGui() 
	{
		getMainWindow().setLayout(new RelativeLayout());
		
		TableLayout tableLayout = new TableLayout();
		RelativeLayoutParameters tableLayoutPosition = new RelativeLayoutParameters();
		tableLayoutPosition.horizontallyCentered();
		tableLayoutPosition.setTopPosition(10, GraphicalUnit.PERCENT);
		tableLayout.setLayoutParameters(tableLayoutPosition);
		addControl(tableLayout);
		
		RelativeLayoutParameters centered = new RelativeLayoutParameters();
		centered.horizontallyCentered();
		centered.verticalyCentered();
		
		RelativeLayoutParameters rightAligned = new RelativeLayoutParameters();
		rightAligned.verticalyCentered();
		rightAligned.setRightPosition(0, GraphicalUnit.PIXEL);
				
		Label screenTitle = new Label("Settings");
		screenTitle.setTextColor(Color.yellow);
		screenTitle.setLayoutParameters(centered);
		
		Label lblScreenSize = new Label("Screen Size: ");
		lblScreenSize.setLayoutParameters(rightAligned);
		
		Spinner spinnerScreenSize = new Spinner(new String[] {"800x600", "FULLSCREEN"}, new SelectionListener()
		{
			@Override
			public void handleSelect(Control control) 
			{
				// change screen size
			}
		});
		spinnerScreenSize.setLayoutParameters(centered);
		
		Control[][] tableContent = new Control[][]
		{
			new Control[] {screenTitle},
			new Control[] {lblScreenSize, spinnerScreenSize}
		};
		
		RowLayout row;
		
		for(Control[] rowContent : tableContent)
		{
			row = new RowLayout();

			for(Control control : rowContent)
				row.addControl(control);

			tableLayout.addControl(row);
		}
	}
}
