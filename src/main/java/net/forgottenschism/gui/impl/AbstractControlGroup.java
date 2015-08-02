package net.forgottenschism.gui.impl;

import net.forgottenschism.gui.Control;
import net.forgottenschism.gui.ControlGroup;
import net.forgottenschism.gui.focus.FocusCycleRoot;
import net.forgottenschism.gui.focus.FocusTraversalPolicy;
import net.forgottenschism.gui.focus.impl.FocusTraversalPolicyImpl;

import java.util.LinkedList;
import java.util.List;

public abstract class AbstractControlGroup extends AbstractControl implements ControlGroup
{
	protected List<Control> controls;

	public AbstractControlGroup()
	{
		controls = new LinkedList<>();
	}

	@Override
	public void addControl(Control control)
	{
		control.setParentControl(this);
		control.setKeyboardFocusManager(getKeyboardFocusManager());

		controls.add(control);

		FocusCycleRoot parentFocusCycleRoot = getParentFocusCycleRoot();

		if(parentFocusCycleRoot!=null)
			parentFocusCycleRoot.addControlToCycle(control);
	}

	@Override
	public void removeControl(Control control)
	{
		control.setParentControl(null);
		control.setKeyboardFocusManager(null);

		controls.remove(control);

		FocusCycleRoot parentFocusCycleRoot = getParentFocusCycleRoot();

		if(parentFocusCycleRoot!=null)
			parentFocusCycleRoot.removeControlFromCycle(control);
	}

	@Override
	public void setParentFocusCycleRoot(FocusCycleRoot parentFocusCycleRoot)
	{
		super.setParentFocusCycleRoot(parentFocusCycleRoot);

		for(Control control: controls)
			control.setParentFocusCycleRoot(parentFocusCycleRoot);
	}

	@Override
	public void removeAllChildren()
	{
		for(Control control: controls)
			removeControl(control);
	}

	@Override
	public List<Control> getChildren()
	{
		return controls;
	}

	@Override
	public FocusTraversalPolicy getFocusTraversalPolicy()
	{
		return new FocusTraversalPolicyImpl();
	}

	@Override
	public boolean isFocusable()
	{
		for(Control control: controls)
			if(control.isFocusable())
				return true;

		return false;
	}

	@Override
	protected void updateControl(int delta)
	{
		if(controls.isEmpty())
			return;

		for(Control control : controls)
			control.update(delta);
	}
}
