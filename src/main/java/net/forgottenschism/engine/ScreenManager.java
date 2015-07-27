package net.forgottenschism.engine;

import net.forgottenschism.gui.Screen;

public interface ScreenManager extends GameComponent
{
    void enterNewScreen(Class<? extends Screen> screenClass);
    void goBackToLastScreen();
    void resetToScreen(Class<? extends Screen> screenClass);

    Screen getActiveScreen();
}
