package net.forgottenschism.main;

import java.io.File;

import org.newdawn.slick.AppGameContainer;
import org.newdawn.slick.SlickException;

public class Main
{
	public static void main(String[] args)
	{
		loadNativeLibs();
		
		try
        {
            AppGameContainer app = new AppGameContainer(new ForgottenSchismGame());
            app.setDisplayMode(500, 400, false);
            app.start();
        }
        catch(SlickException e)
        {
            e.printStackTrace();
        }
	}
	
	private static void loadNativeLibs()
	{
		System.setProperty("org.lwjgl.librarypath", new File("native-libs").getAbsolutePath());
	}
}
