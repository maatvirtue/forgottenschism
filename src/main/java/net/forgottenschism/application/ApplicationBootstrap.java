package net.forgottenschism.application;

import net.forgottenschism.gui.bean.Size2d;
import org.newdawn.slick.CanvasGameContainer;
import org.newdawn.slick.SlickException;

import javax.swing.*;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import java.io.File;

public class ApplicationBootstrap
{
	private CanvasGameContainer app;
	private JFrame frame;
	private String windowTitle;
	private Size2d windowSize;

	public ApplicationBootstrap()
	{
		windowTitle = "";
		windowSize = new Size2d(800, 600);
	}

	public void start(Application application) throws SlickException
	{
		loadNativeLibs();

		application.setApplicationBootstrap(this);

		app = new CanvasGameContainer(application);
		app.getContainer().setAlwaysRender(true);

		frame = new JFrame(windowTitle);
		frame.setSize(windowSize.getWidth(), windowSize.getHeight());
		frame.setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
		frame.getContentPane().add(app);
		frame.setLocationRelativeTo(null);

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

	public void setWindowSize(Size2d size)
	{
		windowSize = size;

		if(frame!=null)
			frame.setSize(windowSize.getWidth(), windowSize.getHeight());
	}

	public void setWindowTitle(String title)
	{
		windowTitle = title;

		if(frame!=null)
			frame.setTitle(windowTitle);
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
