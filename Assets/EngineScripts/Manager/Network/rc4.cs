using System;
using System.Collections;

public class rc4
{
	static public Int32 rc4_key = 0;
	static public byte[] sBox = new byte[256];
	static private uint ci = 0;
	static private uint cj = 0;

	static public void CreateContext()
	{
		ci = 0;
		cj = 0;

		for (int i = 0; i < 256; ++i)
		{
			sBox[i] = (byte)i;
		}

		byte[] temp = new byte[256];
		for (int i = 0; i < 64; ++i)
		{
			temp[i * 4] = (byte)(0xFF & rc4_key);
			temp[i * 4 + 1] = (byte)(0xFF & (rc4_key >> 8));
			temp[i * 4 + 2] = (byte)(0xFF & (rc4_key >> 16));
			temp[i * 4 + 3] = (byte)(0xFF & (rc4_key >> 24));
		}

		int j = 0;
		for (int i = 0; i < 256; ++i)
		{
			j = j + sBox[i] + temp[i];
			j = j % 256;
			byte temp_byte = sBox[i];
			sBox[i] = sBox[j];
			sBox[j] = temp_byte;
		}
	}

	static public byte[] RC4_Transform(byte[] context)
	{
		byte[] res = new byte[context.Length];
		for(int i = 0; i < context.Length; ++i)
		{
			byte code = GetNextSeed();
			res[i] = (byte)(code ^ context[i]);
		}
		return res;
	}

	static private byte GetNextSeed()
	{
		ci = 0xFF & (ci + 1);
		cj = 0xFF & (cj + sBox [ci]);

		byte si = sBox [ci];
		byte sj = sBox [cj];
		sBox [ci] = sj;
		sBox [cj] = si;

		return sBox[0xFF & (si+sj)];
	}
}



