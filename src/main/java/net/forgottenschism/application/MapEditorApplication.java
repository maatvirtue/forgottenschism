package net.forgottenschism.application;

import net.forgottenschism.engine.ScreenManager;
import net.forgottenschism.engine.impl.ScreenManagerImpl;
import net.forgottenschism.gui.bean.Size2d;
import net.forgottenschism.mapeditor.MapEditorScreen;

import org.newdawn.slick.BasicGame;
import org.newdawn.slick.GameContainer;
import org.newdawn.slick.Graphics;
import org.newdawn.slick.SlickException;

public class MapEditorApplication extends BasicGame implements Application
{
	private static String WINDOW_TITLE = "Forgotten Schism Map Editor";

	private ScreenManager screenManager;
	private ApplicationBootstrap applicationBootstrap;

	public MapEditorApplication() throws SlickException
	{
		super(WINDOW_TITLE);
	}

	@Override
	public void setApplicationBootstrap(ApplicationBootstrap applicationBootstrap)
	{
		this.applicationBootstrap = applicationBootstrap;

		applicationBootstrap.setWindowTitle(WINDOW_TITLE);
		applicationBootstrap.setWindowSize(new Size2d(1024, 768));
	}

	@Override
	public void init(GameContainer container) throws SlickException
	{
		container.setShowFPS(false);

		screenManager = new ScreenManagerImpl(applicationBootstrap, container);
		screenManager.enterNewScreen(MapEditorScreen.class);
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
