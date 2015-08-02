package net.forgottenschism.gui.focus.impl;

import net.forgottenschism.gui.Control;
import net.forgottenschism.gui.Window;
import net.forgottenschism.gui.event.KeyEvent;
import net.forgottenschism.gui.focus.FocusCycleRoot;
import net.forgottenschism.gui.focus.FocusTraversalPolicy;
import net.forgottenschism.gui.focus.KeyboardFocusManager;

public class KeyboardFocusManagerImpl implements KeyboardFocusManager
{
	private Window window;
	private Control focusedControl;
	private FocusCycleRoot activeCycleRoot;

	public KeyboardFocusManagerImpl(Window window)
	{
		this.window = window;

		activeCycleRoot = window.getFocusCycleRoot();
	}

	@Override
	public void focusDefaultControl()
	{
		if(focusedControl!=null)
			focusedControl.setFocus(false);

		FocusTraversalPolicy policy = activeCycleRoot.getFocusTraversalPolicy();

		focusedControl = policy.getDefaultFocusControl(activeCycleRoot);

		if(focusedControl!=null)
			focusedControl.setFocus(true);
	}

	@Override
	public void focusNextControl()
	{
		if(focusedControl!=null)
			focusedControl.setFocus(false);

		FocusTraversalPolicy policy = activeCycleRoot.getFocusTraversalPolicy();

		focusedControl = policy.getNextFocusControl(activeCycleRoot, focusedControl);

		if(focusedControl!=null)
			focusedControl.setFocus(true);
	}

	@Override
	public void focusPreviousControl()
	{
		if(focusedControl!=null)
			focusedControl.setFocus(false);

		FocusTraversalPolicy policy = activeCycleRoot.getFocusTraversalPolicy();

		focusedControl = policy.getPreviousFocusControl(activeCycleRoot, focusedControl);

		if(focusedControl!=null)
			focusedControl.setFocus(true);
	}

	@Override
	public void upFocusCycle()
	{
		FocusCycleRoot parentCycleRoot = activeCycleRoot.getParentFocusCycleRoot();

		if(parentCycleRoot==null)
		{
			if(focusedControl!=null)
				focusedControl.setFocus(false);

			FocusTraversalPolicy policy = activeCycleRoot.getFocusTraversalPolicy();

			focusedControl = policy.getDefaultFocusControl(activeCycleRoot);

			if(focusedControl!=null)
				focusedControl.setFocus(true);
		}
		else
		{
			if(focusedControl!=null)
				focusedControl.setFocus(false);

			focusedControl = activeCycleRoot;

			if(focusedControl!=null)
				focusedControl.setFocus(true);

			activeCycleRoot = parentCycleRoot;
		}
	}

	@Override
	public void downFocusCycle()
	{
		if(focusedControl==null || !(focusedControl instanceof FocusCycleRoot))
			return;

		FocusCycleRoot newFocusCycleRoot = (FocusCycleRoot) focusedControl;

		focusedControl.setFocus(false);

		FocusTraversalPolicy policy = activeCycleRoot.getFocusTraversalPolicy();

		focusedControl = policy.getDefaultFocusControl(newFocusCycleRoot);

		if(focusedControl!=null)
			focusedControl.setFocus(true);

		activeCycleRoot = newFocusCycleRoot;
	}

	@Override
	public Control getFocusedControl()
	{
		return focusedControl;
	}

	@Override
	public void notifyNewControlAdded()
	{
		if(focusedControl==null)
			focusDefaultControl();
	}

	@Override
	public boolean dispatchKeyEvent(KeyEvent keyEvent)
	{
		if(focusedControl!=null && keyEvent.isKeyPressed()==focusedControl.isFocusTraversalOnKeyPressed())
		{
			if(keyEvent.getKeyCode()==focusedControl.getBackwardFocusTraversalKey())
				focusPreviousControl();
			else if(keyEvent.getKeyCode()==focusedControl.getForwardFocusTraversalKey())
				focusNextControl();
			else if(keyEvent.getKeyCode()==focusedControl.getUpwardFocusTraversalKey())
				upFocusCycle();
			else if(focusedControl instanceof FocusCycleRoot &&
					keyEvent.getKeyCode()==((FocusCycleRoot) focusedControl).getDownwardFocusTraversalKey())
				downFocusCycle();
			else
				focusedControl.receiveEvent(keyEvent);
		}

		return true;
	}

	@Override
	public boolean postProcessKeyEvent(KeyEvent keyEvent)
	{
		return true;
	}
}
