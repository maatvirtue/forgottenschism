package net.forgottenschism.gui.focus.impl;

import net.forgottenschism.gui.Control;
import net.forgottenschism.gui.focus.FocusCycleRoot;
import net.forgottenschism.gui.focus.FocusTraversalPolicy;

import java.util.List;

public class FocusTraversalPolicyImpl implements FocusTraversalPolicy
{
	@Override
	public Control getNextFocusControl(FocusCycleRoot focusCycleRoot, Control currentFocusedControl)
	{
		List<Control> controls = focusCycleRoot.getCycleChildren();
		int currentIndex = controls.indexOf(currentFocusedControl);

		currentIndex++;

		if(currentIndex>controls.size()-1)
			return getFirstFocusControl(focusCycleRoot);
		else
			return forwardFindFocusableControl(controls, currentIndex);
	}

	@Override
	public Control getPreviousFocusControl(FocusCycleRoot focusCycleRoot, Control currentFocusedControl)
	{
		List<Control> controls = focusCycleRoot.getCycleChildren();
		int currentIndex = controls.indexOf(currentFocusedControl);

		currentIndex--;

		if(currentIndex<0)
			return getLastFocusControl(focusCycleRoot);
		else
			return backwardFindFocusableControl(controls, currentIndex);
	}

	@Override
	public Control getFirstFocusControl(FocusCycleRoot focusCycleRoot)
	{
		return forwardFindFocusableControl(focusCycleRoot.getCycleChildren(), 0);
	}

	@Override
	public Control getLastFocusControl(FocusCycleRoot focusCycleRoot)
	{
		List<Control> controls = focusCycleRoot.getCycleChildren();

		return backwardFindFocusableControl(controls, controls.size()-1);
	}

	@Override
	public Control getDefaultFocusControl(FocusCycleRoot focusCycleRoot)
	{
		return getFirstFocusControl(focusCycleRoot);
	}

	private Control forwardFindFocusableControl(List<Control> controls, int startIndex)
	{
		Control currentControl;

		int i = startIndex;

		do
		{
			currentControl = controls.get(i);

			if(currentControl.isVisible() && currentControl.isVisible() && currentControl.isFocusable())
				return currentControl;

			i++;

			if(i>controls.size()-1)
				i = 0;

		} while(i!=startIndex);

		return null;
	}

	private Control backwardFindFocusableControl(List<Control> controls, int startIndex)
	{
		Control currentControl;

		int i = startIndex;

		do
		{
			currentControl = controls.get(i);

			if(currentControl.isVisible() && currentControl.isVisible() && currentControl.isFocusable())
				return currentControl;

			i--;

			if(i<0)
				i = controls.size()-1;

		} while(i!=startIndex);

		return null;
	}
}
