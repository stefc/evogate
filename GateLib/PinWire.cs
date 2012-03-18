using System;

namespace stefc.gatelib
{
	[Flags]
	public	enum PinWire {
		None	= 0x00,
		A		= 0x01,
		B		= 0x02,
		Both	= 0x03
	}
	
	[Flags]
	public enum GateOp {
		None = 0x000,
		Or	 = 0x001,
		And	 = 0x010,
		Nor	 = 0x011,
		Nand = 0x100,
		Xor	 = 0x101,
		Nxor = 0x110
	}
}

