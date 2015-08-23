package net.forgottenschism.gamescreen;

import org.newdawn.slick.Color;

import net.forgottenschism.gui.bean.GraphicalUnit;
import net.forgottenschism.gui.control.Label;
import net.forgottenschism.gui.impl.AbstractScreen;
import net.forgottenschism.gui.layout.LinearLayout;
import net.forgottenschism.gui.layout.RelativeLayout;
import net.forgottenschism.gui.layout.RelativeLayoutParameters;

public class LoadGameScreen extends AbstractScreen
{
	public LoadGameScreen()
	{
		setUpGui();
	}

	private void setUpGui() 
	{
		getMainWindow().setLayout(new RelativeLayout());
		
		LinearLayout linearLayout = new LinearLayout(10);
		RelativeLayoutParameters linearLayoutPosition = new RelativeLayoutParameters();
		linearLayoutPosition.horizontallyCentered();
		linearLayoutPosition.setTopPosition(10, GraphicalUnit.PERCENT);
		linearLayout.setLayoutParameters(linearLayoutPosition);
		addControl(linearLayout);

		Label screenTitle = new Label("Load Game");
		screenTitle.setTextColor(Color.yellow);
		linearLayout.addControl(screenTitle);
		
		
	}
}
