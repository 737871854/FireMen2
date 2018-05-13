using UnityEngine;
using System;
using System.Collections;

public class zwriter
{
	public byte[] zbuf;
	UInt16 zoff;
	UInt16 ztotal;
	public s_zstate zip = null;

	public void zwriter_init(int bits)
	{
		UInt16 ztotal =1024;
		s_zstate zip = s_zstate.zopen_write(bits, this);
		if (zip == null)
			return;

		this.zip = zip;
		this.ztotal = ztotal;
		this.zoff = 0;
		this.zbuf = new byte[this.ztotal];
	}

	public void zwriter_deinit()
	{
		this.zbuf = null;
		this.zip.zclose ();
		this.zip = null;
	}

	public void zwriter_clear()
	{
		this.zoff = 0;
	}

	public int zwriter_write(byte[] buf, UInt16 len)
	{
		int w = this.zip.zwrite_buff (buf, len);
		return w;
	}

	public UInt16 zwriter_fwrite(byte[] ptr, UInt16 sz, UInt16 nmemb)
	{
		int len = sz * nmemb;
		UInt16 left = (UInt16)(this.ztotal - this.zoff);
		if (left < len) {
			this.ztotal += (UInt16)(len > this.ztotal ? len : this.ztotal);
			byte[] temp = new byte[this.ztotal];
			Array.ConstrainedCopy(this.zbuf, 0, temp, 0, this.zbuf.Length);
			this.zbuf = temp;
		}

		Array.ConstrainedCopy (ptr, 0, this.zbuf, this.zoff, len);
		this.zoff += (UInt16)len;
		return (UInt16)len;
	}

}



