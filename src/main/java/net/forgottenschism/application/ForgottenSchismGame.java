package net.forgottenschism.application;

import net.forgottenschism.engine.ScreenManager;
import net.forgottenschism.engine.impl.ScreenManagerImpl;
import net.forgottenschism.gamescreen.WelcomeScreen;
import net.forgottenschism.gui.bean.Size2d;

import org.newdawn.slick.BasicGame;
import org.newdawn.slick.GameContainer;
import org.newdawn.slick.Graphics;
import org.newdawn.slick.SlickException;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class ForgottenSchismGame extends BasicGame implements Application
{
	private static Logger logger = LoggerFactory.getLogger(ForgottenSchismGame.class);
	public static final String WINDOW_TITLE = "Forgotten Schism";

	private ScreenManager screenManager;
	private ApplicationBootstrap gameApplicationBootstrap;

	public ForgottenSchismGame() throws SlickException
	{
		super(WINDOW_TITLE);
	}

	@Override
	public void setApplicationBootstrap(ApplicationBootstrap applicationBootstrap)
	{
		this.gameApplicationBootstrap = applicationBootstrap;

		gameApplicationBootstrap.setWindowTitle(WINDOW_TITLE);
		gameApplicationBootstrap.setWindowSize(new Size2d(1024, 768));
	}

	@Override
	public void init(GameContainer container) throws SlickException
	{
		container.setShowFPS(false);

		screenManager = new ScreenManagerImpl(gameApplicationBootstrap, container);
		screenManager.enterNewScreen(WelcomeScreen.class);
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
