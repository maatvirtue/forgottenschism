package net.forgottenschism.gui.focus.impl;

import net.forgottenschism.gui.Control;
import net.forgottenschism.gui.focus.FocusCycleRoot;
import net.forgottenschism.gui.focus.FocusTraversalPolicy;
import net.forgottenschism.gui.focus.KeyboardFocusConstants;
import net.forgottenschism.gui.impl.AbstractControlGroup;

import java.util.LinkedList;
import java.util.List;

public abstract class AbstractFocusCycleRoot extends AbstractControlGroup implements FocusCycleRoot
{
	private List<Control> cycleControls;

	public AbstractFocusCycleRoot()
	{
		cycleControls = new LinkedList<>();
	}

	@Override
	public boolean isFocusable()
	{
		return true;
	}

	@Override
	public void addControl(Control control)
	{
		control.setParentFocusCycleRoot(this);
		control.setParentControl(this);
		control.setKeyboardFocusManager(getKeyboardFocusManager());

		controls.add(control);

		addControlToCycle(control);
	}

	@Override
	public void removeControl(Control control)
	{
		control.setParentFocusCycleRoot(null);
		control.setParentControl(null);
		control.setKeyboardFocusManager(null);

		controls.remove(control);

		removeControlFromCycle(control);
	}

	@Override
	public void addControlToCycle(Control control)
	{
		cycleControls.add(control);

		getKeyboardFocusManager().notifyNewControlAdded();
	}

	@Override
	public void removeControlFromCycle(Control control)
	{
		cycleControls.remove(control);
	}

	@Override
	public List<Control> getCycleChildren()
	{
		return cycleControls;
	}

	@Override
	public int getDownwardFocusTraversalKey()
	{
		return KeyboardFocusConstants.DEFAULT_DOWNWARD_TRAVERSAL_KEY;
	}

	@Override
	public FocusTraversalPolicy getFocusTraversalPolicy()
	{
		return new FocusTraversalPolicyImpl();
	}
}
