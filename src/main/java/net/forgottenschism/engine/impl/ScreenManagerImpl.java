package net.forgottenschism.engine.impl;

import net.forgottenschism.engine.ScreenManager;
import net.forgottenschism.gui.Screen;
import net.forgottenschism.gui.Size2d;
import org.newdawn.slick.GameContainer;
import org.newdawn.slick.Graphics;
import org.newdawn.slick.util.InputAdapter;

import java.util.LinkedList;
import java.util.Queue;

public class ScreenManagerImpl implements ScreenManager
{
    private class InputHandler extends InputAdapter
    {
        @Override
        public void keyPressed(int key, char character)
        {
            Screen activeScreen = getActiveScreen();

            if(activeScreen!=null)
                activeScreen.keyPressed(key, character);
        }

        @Override
        public void keyReleased(int key, char character)
        {
            Screen activeScreen = getActiveScreen();

            if(activeScreen!=null)
                activeScreen.keyReleased(key, character);
        }
    }

    private Queue<Screen> screenHistory;
    private GameContainer gameContainer;
    private InputHandler inputHandler;

    public ScreenManagerImpl(GameContainer gameContainer)
    {
        this.gameContainer = gameContainer;
        screenHistory = new LinkedList<>();
        inputHandler = new InputHandler();

        gameContainer.getInput().addKeyListener(inputHandler);
    }

    @Override
    public void enterNewScreen(Class<Screen> screenClass)
    {
        Screen oldScreen = getActiveScreen();

        if(oldScreen!=null)
            oldScreen.pauseScreen();

        Screen newScreen;

        try
        {
            newScreen = screenClass.newInstance();
        }
        catch(Exception exception)
        {
            throw new RuntimeException("Error instanciating screen "+screenClass.getCanonicalName(), exception);
        }

        newScreen.setScreenSize(new Size2d(gameContainer.getWidth(), gameContainer.getHeight()));

        screenHistory.add(newScreen);

        newScreen.enterScreen();
    }

    @Override
    public void goBackToLastScreen()
    {
        Screen currentScreen = getActiveScreen();
        Screen lastScreen;

        if(currentScreen!=null)
        {
            currentScreen.leaveScreen();
            screenHistory.remove();
        }

        lastScreen = screenHistory.peek();
        lastScreen.resumeScreen();
    }

    @Override
    public void resetToScreen(Class<Screen> screenClass)
    {
        exitAllScreens();
        enterNewScreen(screenClass);
    }

    private void exitAllScreens()
    {
        while(!screenHistory.isEmpty())
            screenHistory.poll().leaveScreen();
    }

    @Override
    public Screen getActiveScreen()
    {
        return screenHistory.peek();
    }

    @Override
    public void update(GameContainer container, int delta)
    {
        if(!screenHistory.isEmpty())
            getActiveScreen().update(container, delta);
    }

    @Override
    public void render(GameContainer container, Graphics graphics)
    {
        if(!screenHistory.isEmpty())
            getActiveScreen().render(container, graphics);
    }
}
