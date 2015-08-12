package net.forgottenschism.main;

import net.forgottenschism.constants.Constants;

import org.lwjgl.opengl.Display;
import org.lwjgl.opengl.GL11;
import org.newdawn.slick.AppGameContainer;
import org.newdawn.slick.BasicGame;
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
		loadNativeLibs();
		
		final BasicGame game = new ForgottenSchismGame();
		final AppGameContainer app = new AppGameContainer(game);
		
		Display.setResizable(true);
		app.setDisplayMode(Constants.DEFAULT_GAME_SIZE.getWidth(), Constants.DEFAULT_GAME_SIZE.getHeight(), false);
		
//		final CanvasGameContainer app = new CanvasGameContainer(game);
//		final JFrame frame = new JFrame(game.getTitle());
//		
//		frame.setSize(Constants.DEFAULT_GAME_SIZE.getWidth(), Constants.DEFAULT_GAME_SIZE.getHeight());
//		frame.setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
//		frame.setLocationRelativeTo(null);
//		frame.getContentPane().add(app);
//
//		frame.addWindowListener(new WindowAdapter()
//		{
//			@Override
//			public void windowClosing(WindowEvent e)
//			{
//				frame.setVisible(false);
//				app.setEnabled(false);
//				app.getContainer().exit();
//				app.dispose();
//			}
//
//			@Override
//			public void windowClosed(WindowEvent e)
//			{
//				System.exit(0);
//			}
//		});
//		
//		frame.setVisible(true);
		
		app.start();
	}

	private static void loadNativeLibs()
	{
		String nativeLibraryPath = System.getProperty("java.library.path");
		
		System.setProperty("org.lwjgl.librarypath", new File(nativeLibraryPath).getAbsolutePath());
	}
}
