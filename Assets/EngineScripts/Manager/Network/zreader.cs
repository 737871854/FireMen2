using UnityEngine;
using System;
using System.Collections;

public class zreader
{
	public byte[] buf;
	public int off;
	public int total;

	public byte[] zbuf;
	public int zlen;
	public int zoff;
	public int ztotal;
	public s_zstate unzip;

	public int[] eob;
	public int eobcnt;
	public int eobtotal;

	public void zreader_init(int bits)
	{
		UInt16 total = 1024;
		s_zstate _unzip = s_zstate.zopen_read(bits, this);
		if (_unzip == null)
		{
			return;
		}

		this.unzip = _unzip;
		this.off = 0;
		this.total = total;
		this.buf = new byte[this.total];

		this.zoff = 0;
		this.zlen = 0;
		this.ztotal = total;
		this.zbuf = new byte[this.ztotal];

		this.eobcnt = 0;
		this.eobtotal = 1;
		this.eob = new int[this.eobtotal];
		for (int i = 0; i < this.eob.Length; ++i)
		{
			this.eob[i] = 0;
		}
	}

	public void zreader_deinit()
	{
		this.buf = null;
		this.eob = null;
		this.zbuf = null;
		this.unzip.zclose ();
		this.unzip = null;
	}

	public void zreader_clear()
	{
		if (this.eobcnt > 0)
		{
			int eob = this.eob[this.eobcnt - 1];
			int sz = (UInt16)(this.off - eob);
			int index = 0;
			Array.ConstrainedCopy(this.buf, eob, this.buf, 0, sz);
			this.off = sz;
			this.eobcnt = 0;
		}
	}

	public int zreader_read(byte[] zbufs, UInt16 zlen)
	{
		UInt16 zsz = (UInt16)(this.zlen - this.zoff);
		Array.ConstrainedCopy (this.zbuf, this.zoff, this.zbuf, 0, zsz);
		if ((this.ztotal - zsz) < zlen)
		{
			this.ztotal += (zlen > this.ztotal ? zlen : this.ztotal);
			byte[] temp = new byte[this.ztotal];
			Array.ConstrainedCopy(this.zbuf, 0, temp, 0, this.zbuf.Length);
			this.zbuf = temp;
		}

		Array.ConstrainedCopy (zbufs, 0, this.zbuf, zsz, zlen);
		this.zoff = 0;
		this.zlen = (UInt16)(zsz + zlen);
		this.unzip.resetroffset ();

		while(this.unzip.zread_buff())
		{
			zreader_eob();
		}

		return this.eobcnt;
	}

	public void zreader_eob()
	{
		if (this.eobtotal == this.eobcnt)
		{
			this.eobtotal *= 2;
			int[] temp = new int[this.eobtotal];
			Array.ConstrainedCopy(this.eob, 0, temp, 0, this.eob.Length);
			this.eob = temp;
		}
		this.eob [this.eobcnt++] = this.off;
	}

	public void zreader_output(byte c)
	{
		if (this.off >= this.total)
		{
			this.total *= 2;
			byte[] temp = new byte[this.total];
			Array.ConstrainedCopy(this.buf, 0, temp, 0, this.buf.Length);
			this.buf = temp;
		}
		this.buf [this.off++] = c;
	}
}



