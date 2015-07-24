package net.forgottenschism.main;

import net.forgottenschism.gui.MapControl;

import org.newdawn.slick.*;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class ForgottenSchismGame extends BasicGame
{
	private static Logger logger = LoggerFactory.getLogger(ForgottenSchismGame.class);

	private MapControl mapControl;

	public ForgottenSchismGame() throws SlickException
	{
		super("ForgottenSchism");
	}

	@Override
	public void init(GameContainer container) throws SlickException
	{
		container.setShowFPS(false);

		mapControl = new MapControl();
	}

	@Override
	public void update(GameContainer container, int delta) throws SlickException
	{
		mapControl.update(container, delta);
	}


	@Override
	public void render(GameContainer container, Graphics graphics) throws SlickException
	{
		mapControl.render(container, graphics);
	}
}
