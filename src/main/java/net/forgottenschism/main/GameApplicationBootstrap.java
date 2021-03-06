package net.forgottenschism.main;

import net.forgottenschism.constants.Constants;
import org.newdawn.slick.CanvasGameContainer;
import org.newdawn.slick.SlickException;

import javax.swing.*;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import java.io.File;

public class GameApplicationBootstrap
{
	private CanvasGameContainer app;
	private JFrame frame;

	public GameApplicationBootstrap()
	{
		//Do nothing
	}

	public void start() throws SlickException
	{
		loadNativeLibs();

		app = new CanvasGameContainer(new ForgottenSchismGame(this, Constants.GAME_TITLE));
		app.getContainer().setAlwaysRender(true);

		frame = new JFrame(Constants.GAME_TITLE);
		frame.setSize(Constants.DEFAULT_GAME_SIZE.getWidth(), Constants.DEFAULT_GAME_SIZE.getHeight());
		frame.setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
		frame.getContentPane().add(app);

		frame.addWindowListener(new WindowAdapter()
		{
			@Override
			public void windowClosing(WindowEvent e)
			{
				app.setEnabled(false);
				app.getContainer().exit();
				app.dispose();
			}

			@Override
			public void windowClosed(WindowEvent e)
			{
				System.exit(0);
			}
		});

		frame.setVisible(true);
		app.start();
	}

	public void stop()
	{
		frame.dispatchEvent(new WindowEvent(frame, WindowEvent.WINDOW_CLOSING));
	}

	private static void loadNativeLibs()
	{
		String nativeLibraryPath = System.getProperty("java.library.path");

		System.setProperty("org.lwjgl.librarypath", new File(nativeLibraryPath).getAbsolutePath());
	}
}
