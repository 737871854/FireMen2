package
{
	import flash.system.System;

	public class GateFromFlashWorld
	{
	    public static function GetPrivateMemory(): int
	    {
			return System.privateMemory;
	    }
	}
}