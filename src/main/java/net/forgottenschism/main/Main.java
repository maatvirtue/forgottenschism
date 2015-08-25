package net.forgottenschism.main;

import net.forgottenschism.application.ApplicationBootstrap;
import net.forgottenschism.application.ForgottenSchismGame;

import org.newdawn.slick.SlickException;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class Main
{
	private static Logger logger = LoggerFactory.getLogger(Main.class);

	public static void main(String[] args) throws SlickException
	{
		ApplicationBootstrap applicationBootstrap = new ApplicationBootstrap();

		applicationBootstrap.start(new ForgottenSchismGame());
	}
}
