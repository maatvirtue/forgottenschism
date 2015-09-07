package net.forgottenschism.exception;

public class ForgottenschismException extends Exception
{
	public ForgottenschismException()
	{
		//Do nothing
	}

	public ForgottenschismException(String message)
	{
		super(message);
	}

	public ForgottenschismException(Throwable cause)
	{
		super(cause);
	}

	public ForgottenschismException(String message, Throwable cause)
	{
		super(message, cause);
	}
}
