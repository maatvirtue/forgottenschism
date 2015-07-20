package net.forgottenschism.engine;

import net.forgottenschism.gui.Screen;

public interface ScreenManager extends GameComponent
{
    void enterNewScreen(Class<Screen> screenClass);
    void goBackToLastScreen();
    void resetToScreen(Class<Screen> screenClass);

    Screen getActiveScreen();
}
