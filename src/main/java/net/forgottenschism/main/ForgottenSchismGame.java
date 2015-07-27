package net.forgottenschism.main;

import net.forgottenschism.engine.ScreenManager;
import net.forgottenschism.engine.impl.ScreenManagerImpl;
import net.forgottenschism.screen.MainScreen;
import org.newdawn.slick.BasicGame;
import org.newdawn.slick.GameContainer;
import org.newdawn.slick.Graphics;
import org.newdawn.slick.SlickException;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class ForgottenSchismGame extends BasicGame
{
	private static Logger logger = LoggerFactory.getLogger(ForgottenSchismGame.class);

	private ScreenManager screenManager;

	public ForgottenSchismGame() throws SlickException
	{
		super("ForgottenSchism");
	}

	@Override
	public void init(GameContainer container) throws SlickException
	{
		container.setShowFPS(false);

		screenManager = new ScreenManagerImpl(container);
		screenManager.enterNewScreen(MainScreen.class);
	}

	@Override
	public void update(GameContainer container, int delta) throws SlickException
	{
		screenManager.update(container, delta);
	}

	@Override
	public void render(GameContainer container, Graphics graphics) throws SlickException
	{
		screenManager.render(container, graphics);
	}
}
