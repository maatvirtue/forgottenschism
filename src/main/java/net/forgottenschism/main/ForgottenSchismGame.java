package net.forgottenschism.main;

import net.forgottenschism.engine.GraphicsGenerator;
import org.newdawn.slick.*;
import org.newdawn.slick.util.InputAdapter;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class ForgottenSchismGame extends BasicGame
{
	private static Logger logger = LoggerFactory.getLogger(ForgottenSchismGame.class);
	private Image image;
	private int width;
	private int height;
	private int screenWidth;
	private int screenHeight;

	public ForgottenSchismGame() throws SlickException
	{
		super("ForgottenSchism");
	}

	@Override
	public void init(GameContainer container) throws SlickException
	{
		container.setShowFPS(false);

		image = GraphicsGenerator.getSolidColorImage(10, 10, Color.blue);

		container.getInput().addKeyListener(new InputAdapter()
		{
			@Override
			public void keyPressed(int key, char c)
			{
				logger.debug("key pressed: "+key+" "+c);
			}

			@Override
			public void keyReleased(int key, char c)
			{
				logger.debug("key released: "+key+" "+c);
			}
		});
	}

	@Override
	public void update(GameContainer container, int delta) throws SlickException
	{
		if(width!=container.getWidth() || height!=container.getHeight())
		{
			width = container.getWidth();
			height = container.getHeight();

			logger.debug("Window size: "+width+" "+height);
		}


		if(screenWidth!=container.getScreenWidth() || screenHeight!=container.getScreenHeight())
		{
			screenWidth = container.getScreenWidth();
			screenHeight = container.getScreenHeight();

			logger.debug("Screen size: "+screenWidth+" "+screenHeight);
		}
	}


	@Override
	public void render(GameContainer container, Graphics graphics) throws SlickException
	{
		graphics.drawString("TEST1 TEST2 TEST3", 25, 25);

		graphics.drawImage(image, 50, 50);
	}
}
