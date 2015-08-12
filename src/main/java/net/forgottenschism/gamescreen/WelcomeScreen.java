package net.forgottenschism.gamescreen;

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
		linearLayout.addControl(title);

		Link linkNewGame = new Link("New Game", new SelectionListener()
		{
			@Override
			public void handleSelect(Control control)
			{
				startNewGame();
			}
		});
		linearLayout.addControl(linkNewGame);

		Link linkLoadGame = new Link("Load Game", new SelectionListener()
		{
			@Override
			public void handleSelect(Control control)
			{
				showLoadGameDialog();
			}
		});
		linearLayout.addControl(linkLoadGame);

		Link linkOptions = new Link("Options", new SelectionListener()
		{
			@Override
			public void handleSelect(Control control)
			{
				showOptionsDialog();
			}
		});
		linearLayout.addControl(linkOptions);

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

	protected void startNewGame()
	{
		//TODO
	}

	protected void showLoadGameDialog()
	{
		//TODO
	}

	protected void showOptionsDialog()
	{
		//TODO
	}
}
