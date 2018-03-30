using System;

namespace stefc.gatelib
{
	[Flags]
	public	enum PinWire {
		None	= 0b00,
		A		= 0b01,
		B		= 0b10,
		Both	= 0b11
	}
	
	[Flags]
	public enum GateOp {
		None = 0b000,
		Or	 = 0b001,
		And	 = 0b010,
		Nor	 = 0b011,
		Nand = 0b100,
		Xor	 = 0b101,
		Nxor = 0x110
	}
}

