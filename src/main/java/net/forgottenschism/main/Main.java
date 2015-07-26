package net.forgottenschism.main;

import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import java.awt.event.WindowListener;
import java.io.File;

import net.forgottenschism.constants.Constants;
import org.newdawn.slick.CanvasGameContainer;
import org.newdawn.slick.SlickException;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import javax.swing.*;

public class Main
{
    private static Logger logger = LoggerFactory.getLogger(Main.class);

	public static void main(String[] args) throws SlickException
    {
		loadNativeLibs();

        final CanvasGameContainer app = new CanvasGameContainer(new ForgottenSchismGame());
        app.getContainer().setAlwaysRender(true);

        JFrame frame = new JFrame();
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
	
	private static void loadNativeLibs()
	{
        String nativeLibraryPath = System.getProperty("java.library.path");

		System.setProperty("org.lwjgl.librarypath", new File(nativeLibraryPath).getAbsolutePath());
	}
}
