package net.forgottenschism.main;

import org.newdawn.slick.SlickException;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class Main
{
	@SuppressWarnings("unused")
	private static Logger logger = LoggerFactory.getLogger(Main.class);

	public static void main(String[] args) throws SlickException
	{
		GameApplicationBootstrap gameApplicationBootstrap = new GameApplicationBootstrap();

		gameApplicationBootstrap.start();
	}
}
