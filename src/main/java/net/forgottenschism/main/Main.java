package net.forgottenschism.main;

import net.forgottenschism.constants.Constants;
import org.newdawn.slick.CanvasGameContainer;
import org.newdawn.slick.SlickException;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import javax.swing.*;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import java.io.File;

public class Main
{
	private static Logger logger = LoggerFactory.getLogger(Main.class);

	public static void main(String[] args) throws SlickException
	{
		GameApplicationBootstrap gameApplicationBootstrap = new GameApplicationBootstrap();

		gameApplicationBootstrap.start();
	}
}
