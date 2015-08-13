package net.forgottenschism.main;

import net.forgottenschism.engine.ScreenManager;
import net.forgottenschism.engine.impl.ScreenManagerImpl;
import net.forgottenschism.gamescreen.TestScreen;
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
	private GameApplicationBootstrap gameApplicationBootstrap;

	public ForgottenSchismGame(GameApplicationBootstrap gameApplicationBootstrap, String title) throws SlickException
	{
		super(title);

		this.gameApplicationBootstrap = gameApplicationBootstrap;
	}

	@Override
	public void init(GameContainer container) throws SlickException
	{
		container.setShowFPS(false);

		screenManager = new ScreenManagerImpl(gameApplicationBootstrap, container);
		screenManager.enterNewScreen(TestScreen.class);
	}

	@Override
	public void update(GameContainer container, int delta) throws SlickException
	{
		screenManager.update(delta);
	}

	@Override
	public void render(GameContainer container, Graphics graphics) throws SlickException
	{
		screenManager.render(graphics);
	}
}
