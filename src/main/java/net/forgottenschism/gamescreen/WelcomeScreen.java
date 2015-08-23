package net.forgottenschism.gamescreen;

import org.newdawn.slick.Color;

import net.forgottenschism.gui.*;
import net.forgottenschism.gui.bean.*;
import net.forgottenschism.gui.control.Label;
import net.forgottenschism.gui.control.Link;
import net.forgottenschism.gui.impl.AbstractScreen;
import net.forgottenschism.gui.layout.*;

public class WelcomeScreen extends AbstractScreen
{

	public WelcomeScreen()
	{
		setupGui();
	}

	private void setupGui()
	{
		getMainWindow().setLayout(new RelativeLayout());

		LinearLayout linearLayout = new LinearLayout(10);
		RelativeLayoutParameters linearLayoutPosition = new RelativeLayoutParameters();
		linearLayoutPosition.horizontallyCentered();
		linearLayoutPosition.setTopPosition(10, GraphicalUnit.PERCENT);
		linearLayout.setLayoutParameters(linearLayoutPosition);
		addControl(linearLayout);

		Label title = new Label("Forgotten Schism");
		title.setTextColor(Color.yellow);
		linearLayout.addControl(title);

		Link linkNewGame = new Link("New Game", new SelectionListener()
		{
			@Override
			public void handleSelect(Control control)
			{
				// go to first screen of a new game
			}
		});
		linearLayout.addControl(linkNewGame);

		Link linkLoadGame = new Link("Load Game", new SelectionListener()
		{
			@Override
			public void handleSelect(Control control)
			{
				// enter load game screen
			}
		});
		linearLayout.addControl(linkLoadGame);
		
		Link linkSettings = new Link("Settings", new SelectionListener()
		{
			@Override
			public void handleSelect(Control control)
			{
				// enter settings screen
			}
		});
		linearLayout.addControl(linkSettings);

		Link linkExit = new Link("Exit", new SelectionListener()
		{
			@Override
			public void handleSelect(Control control)
			{
				exitGame();
			}
		});
		linearLayout.addControl(linkExit);
	}
}
