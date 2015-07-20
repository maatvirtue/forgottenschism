package net.forgottenschism.main;

import java.io.File;

import net.forgottenschism.constants.Constants;
import org.newdawn.slick.CanvasGameContainer;
import org.newdawn.slick.SlickException;

import javax.swing.*;

public class Main
{
	public static void main(String[] args) throws SlickException
    {
		loadNativeLibs();

        CanvasGameContainer app = new CanvasGameContainer(new ForgottenSchismGame());
        app.getContainer().setAlwaysRender(true);

        JFrame frame = new JFrame();
        frame.setSize(Constants.DEFAULT_GAME_SIZE.getWidth(), Constants.DEFAULT_GAME_SIZE.getHeight());
        frame.getContentPane().add(app);

        frame.setVisible(true);
        app.start();
	}
	
	private static void loadNativeLibs()
	{
		System.setProperty("org.lwjgl.librarypath", new File("native-libs").getAbsolutePath());
	}
}
