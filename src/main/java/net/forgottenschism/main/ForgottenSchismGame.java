package net.forgottenschism.main;

import net.forgottenschism.engine.ScreenManager;
import net.forgottenschism.engine.impl.ScreenManagerImpl;
import net.forgottenschism.gamescreen.WelcomeScreen;
import net.forgottenschism.gamescreen.TestScreen;

import org.lwjgl.opengl.Display;
import org.lwjgl.opengl.GL11;
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
		super("Forgotten Schism");
	}

	@Override
	public void init(GameContainer container) throws SlickException
	{
		container.setShowFPS(true);
		container.setTargetFrameRate(60);
		container.setMaximumLogicUpdateInterval(60);
		container.setAlwaysRender(true);
		container.setVSync(true);
		
		screenManager = new ScreenManagerImpl(container);
		//screenManager.enterNewScreen(TestScreen.class);
		screenManager.enterNewScreen(WelcomeScreen.class);
	}

	@Override
	public void update(GameContainer container, int delta) throws SlickException
	{
		if(Display.wasResized())
		{
		    GL11.glViewport(0, 0, Display.getWidth(), Display.getHeight());
		}
		
		screenManager.update(delta);
	}

	@Override
	public void render(GameContainer container, Graphics graphics) throws SlickException
	{
		screenManager.render(graphics);
	}
}
