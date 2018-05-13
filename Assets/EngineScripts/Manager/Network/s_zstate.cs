using UnityEngine;
using System;
using System.Collections;

public class s_zstate
{
	public const int BITS = 12;
	public const int HSIZE = 5021;
	
	public const int FIRST = 258;
	public const int EOB = 257;
	public const int CLEAR = 256;
	
	private static byte[] lmask =
	{0xff, 0xfe, 0xfc, 0xf8, 0xf0, 0xe0, 0xc0, 0x80, 0x00};
	private static byte[] rmask =
	{0x00, 0x01, 0x03, 0x07, 0x0f, 0x1f, 0x3f, 0x7f, 0xff};
	
	private struct zread
	{
		//public char[] zs_stackp;
		public int[] zs_stackp_value;
		public int zs_stackp;
		public int zs_finchar;
		public int zs_code;
		public int zs_oldcode;
		public int zs_incode;
		public int zs_roffset;
		public int zs_size;
		public byte[] zs_gbuf;
		public zreader reader;
		public int needsz;
	}

	private struct zwrite
	{
		public long zs_fcode;
		public int zs_ent;
		public int zs_hsize_reg;
		public int zs_hshift;
		public zwriter writer;
	}
	
	private enum state_t
	{
		S_START, 
		S_MIDDLE,
		S_STREAMING,
		S_EOB,
	}
	
	private string zs_mode;
	private state_t zs_state;
	private UInt16 zs_n_bits;
	private UInt16 zs_maxbits;
	private int zs_maxcode;
	private int zs_maxmaxcode;
	private int[] zs_htab;
	private uint[] zs_codetab;
	private int zs_hsize;
	private int zs_free_ent;
	
	private int zs_block_compress;
	private int zs_clear_flg;
	private long zs_ratio;
	private int zs_checkpoint;
	private uint zs_offset;
	private long zs_in_count;
	private long zs_bytes_out;
	private long zs_out_count;
	private byte[] zs_buf;
	private zread s_read;
	private zwrite s_write;
	
	public static s_zstate zopen_read(int bits, zreader read)
	{
		if (bits < 0 || bits > BITS)
		{
			return null;
		}

		s_zstate zs = s_zstate.create (bits);

		zs.zs_mode = "r";
		zs.s_read.reader = read;
		zs.s_read.needsz = 0;

		return zs;
	}

	public static s_zstate zopen_write(int bits, zwriter write)
	{
		if (bits < 0 || bits > BITS)
			return null;

		s_zstate zs = s_zstate.create (bits);

		zs.zs_mode = "w";
		zs.s_write.writer = write;

		return zs;
	}

	public static s_zstate create(int bits)
	{
		s_zstate zs = new s_zstate ();
		zs.zs_maxbits = (UInt16)((bits!=0) ? bits : BITS);
		zs.zs_maxmaxcode = (UInt16)(1L << zs.zs_maxbits);
		zs.zs_hsize = HSIZE;
		zs.zs_free_ent = 0;
		zs.zs_block_compress = 0x80;
		zs.zs_clear_flg = 0;
		zs.zs_ratio = 0;
		zs.zs_checkpoint = 10000;
		zs.zs_in_count = 1;
		zs.zs_out_count = 0;
		zs.zs_state = state_t.S_START;
		zs.s_read.zs_roffset = 0;
		zs.s_read.zs_size = 0;
		zs.zs_htab = new int[HSIZE];
		zs.zs_codetab = new uint[HSIZE];
		zs.zs_buf = new byte[BITS];

		return zs;
	}
	
	public int zclose()
	{
		return 0;
	}
	
	public void resetroffset()
	{
		this.s_read.zs_roffset = this.s_read.zs_roffset & 7;
	}

	public int zwrite_buff(byte[] wbp, int num)
	{
		int bp_index = 0;
		int i;
		int c, disp;
		byte[] bp;
		int count;

		if (num == 0)
			return (0);

		count = num;
		bp = (byte[])wbp;

		if (this.zs_state == state_t.S_MIDDLE)
		{
			goto middle;
		}

		this.zs_state = state_t.S_MIDDLE;
		this.zs_maxmaxcode = (int)(1L << this.zs_maxbits);
		this.zs_offset = 0;
		this.zs_bytes_out = 3;
		this.zs_out_count = 0;
		this.zs_clear_flg = 0;
		this.zs_ratio = 0;
		this.zs_in_count = 1;
		this.zs_checkpoint = 10000;
		this.zs_free_ent = ((this.zs_block_compress != 0)? FIRST : 256);
		this.zs_n_bits = BITS;
		this.zs_maxcode = ((1 << (this.zs_n_bits)) - 1);
		this.s_write.zs_hshift = 0;
		for(this.s_write.zs_fcode = (long)this.zs_hsize; this.s_write.zs_fcode < 65536L; this.s_write.zs_fcode *= 2L)
			this.s_write.zs_hshift++;

		this.s_write.zs_hshift = 8 - this.s_write.zs_hshift;
		this.s_write.zs_hsize_reg = this.zs_hsize;
		this.cl_hash(this.s_write.zs_hsize_reg);

middle: this.s_write.zs_ent = bp[bp_index++];
		--count;
		for (i = 0; count > 0; count--) 
		{
			c = bp[bp_index++];
			this.zs_in_count++;
			this.s_write.zs_fcode = (long)(((long)c << this.zs_maxbits) + this.s_write.zs_ent);
			i = ((c << this.s_write.zs_hshift) ^ this.s_write.zs_ent);
			if (this.zs_htab[i] == this.s_write.zs_fcode)
			{
				this.s_write.zs_ent = (int)this.zs_codetab[i];
				continue;
			}
			else if ((long)this.zs_htab[i] < 0)
			{
				goto nomatch;
			}
			
			disp = this.s_write.zs_hsize_reg - i;
			if (i == 0)
				disp = 1;
			
probe:		if ((i -= disp) < 0)
			{
				i += this.s_write.zs_hsize_reg;
			}
			
			if (this.zs_htab[i] == this.s_write.zs_fcode)
			{
				this.s_write.zs_ent = (int)this.zs_codetab[i];
				continue;
			}
			
			if ((long)this.zs_htab[i] >= 0)
				goto probe;

nomatch:	if (this.output((int) this.s_write.zs_ent) == -1)
			{
				return (-1);
			}
			this.zs_out_count++;
			this.s_write.zs_ent = c;
			if (this.zs_free_ent < this.zs_maxmaxcode)
			{
				this.zs_codetab[i] = (UInt16)(this.zs_free_ent++);
				this.zs_htab[i] = (int)this.s_write.zs_fcode;
			}
			else if ((int)this.zs_in_count >= this.zs_checkpoint && this.zs_block_compress != 0)
			{
				if (this.cl_block() == -1)
				{
					return (-1);
				}
			}
		}

		if (output((int) this.s_write.zs_ent) == -1)
		{
			return (-1);
		}
		this.zs_out_count++;

		if (output((int) EOB) == -1)
		{
			return (-1);
		}

		if (output((int) -1) == -1)
		{
			return (-1);
		}

		return (num);
	}

	public int output(int ocode)
	{
		int bp_index = 0;
		int r_off;
		UInt16 bits;
		byte[] bp;

		r_off = (int)this.zs_offset;
		bits = this.zs_n_bits;
		bp = this.zs_buf;

		if (ocode >= 0)
		{
			bp_index += (r_off >> 3);
			r_off &= 7;
			bp[bp_index] = (byte)((bp[bp_index] & rmask[r_off]) | ((ocode << r_off) & lmask[r_off]));
			bp_index++;
			bits = (UInt16)(bits - (8 - r_off));
			ocode >>= (8 - r_off);

			if (bits >= 8) 
			{
				bp[bp_index++] = (byte)ocode;
				ocode >>= 8;
				bits -= 8;
			}

			if (bits != 0)
			{
				bp[bp_index] = (byte)ocode;
			}
			this.zs_offset += this.zs_n_bits;
			if (this.zs_offset == (this.zs_n_bits << 3))
			{
				bp = this.zs_buf;
				bp_index = 0;
				bits = this.zs_n_bits;
				this.zs_bytes_out += bits;
				if (this.s_write.writer.zwriter_fwrite(bp, (UInt16)sizeof(byte), (UInt16)bits) != (UInt16)bits)
					return (-1);

				bp_index += bits;
				bits = 0;
				this.zs_offset = 0;
			}

			if ((this.zs_clear_flg > 0))
			{
				if (this.zs_clear_flg != 0)
				{
					this.zs_clear_flg = 0;
				}
				else
				{
				}
			}
		}
		else
		{
			if (this.zs_offset > 0)
			{
				this.zs_offset = (this.zs_offset + 7) / 8;
				if (this.s_write.writer.zwriter_fwrite(this.zs_buf, (UInt16)1, (UInt16)this.zs_offset) != (UInt16)this.zs_offset)
					return (-1);

				this.zs_bytes_out += this.zs_offset;
			}
			this.zs_offset = 0;
		}

		return (0);
	}

	public void cl_hash(int cl_hsize)
	{
		int htab_p = cl_hsize;
		long i;
		int m1;
		m1 = -1;
		i = cl_hsize - 16;
		do
		{
			if (this.zs_htab == null)
			{
				Debug.Log("this.zs_htab == null");
			}
			this.zs_htab[htab_p - 16] = m1;
			this.zs_htab[htab_p - 15] = m1;
			this.zs_htab[htab_p - 14] = m1;
			this.zs_htab[htab_p - 13] = m1;
			this.zs_htab[htab_p - 12] = m1;
			this.zs_htab[htab_p - 11] = m1;
			this.zs_htab[htab_p - 10] = m1;
			this.zs_htab[htab_p - 9] = m1;
			this.zs_htab[htab_p - 8] = m1;
			this.zs_htab[htab_p - 7] = m1;
			this.zs_htab[htab_p - 6] = m1;
			this.zs_htab[htab_p - 5] = m1;
			this.zs_htab[htab_p - 4] = m1;
			this.zs_htab[htab_p - 3] = m1;
			this.zs_htab[htab_p - 2] = m1;
			this.zs_htab[htab_p - 1] = m1;
			htab_p -= 16;
		}while((i -= 16) >= 0);

		for (i += 16; i > 0; i--)
		{
			this.zs_htab[--htab_p] = m1;
		}
	}

	public int cl_block()
	{
		long rat;
		this.zs_checkpoint = (int)(this.zs_in_count + 10000);

		if (this.zs_in_count > 0x007fffff)
		{
			rat = this.zs_bytes_out >> 8;
			if (rat == 0)
				rat = 0x7fffffff;
			else
				rat = this.zs_in_count / rat;
		}
		else
			rat = (this.zs_in_count << 8) / this.zs_bytes_out;

		if (rat > this.zs_ratio)
			this.zs_ratio = rat;
		else
		{
			this.zs_ratio = 0;
			this.cl_hash((int)this.zs_hsize);
			this.zs_free_ent = FIRST;
			this.zs_clear_flg = 1;

			if (output((int) CLEAR) == -1)
				return (-1);
		}

		return (0);
	}

	public bool zread_buff()
	{
		switch(this.zs_state)
		{
		case state_t.S_START:
			this.zs_state = state_t.S_EOB;
			break;
		case state_t.S_MIDDLE:
			goto middleing;
			break;
		case state_t.S_STREAMING:
			goto streaming;
			break;
		case state_t.S_EOB:
			goto streamstart;
			break;
		}

		this.zs_n_bits = BITS;
		this.zs_maxcode = ((1 << (this.zs_n_bits)) - 1);
		for(this.s_read.zs_code = 255; this.s_read.zs_code >= 0; this.s_read.zs_code--)
		{
			this.zs_codetab[this.s_read.zs_code] = 0;
			this.zs_htab[this.s_read.zs_code] = (char)this.s_read.zs_code;
		}

		this.zs_free_ent = (this.zs_block_compress != 0) ? 258 : 256;
		this.s_read.zs_stackp = 1 << BITS;

streamstart:
		this.s_read.zs_oldcode = this.getcode ();
		if (this.s_read.zs_oldcode == -1) {	/* EOF already? */
			return false;	/* Get out of here */
		}
		
		if (this.s_read.zs_oldcode == EOB || this.s_read.zs_oldcode == CLEAR)
		{
			// 出错了
			return false;
		}

		this.zs_state = state_t.S_STREAMING;
		if (this.s_read.zs_oldcode < 256)
		{
			this.s_read.zs_finchar = this.s_read.zs_oldcode;
			this.s_read.reader.zreader_output((byte)this.s_read.zs_finchar);
		}
		else
		{
			int code_ = this.s_read.zs_oldcode;
			while (code_ >= 256)
			{
				this.zs_htab[this.s_read.zs_stackp++] = this.zs_htab[code_];
				code_ = (int)this.zs_codetab[code_];
			}
			this.s_read.zs_finchar = this.zs_htab[code_];
			this.zs_htab[this.s_read.zs_stackp++] = this.s_read.zs_finchar;
			
			do
			{
				this.s_read.reader.zreader_output((byte)(this.zs_htab[--this.s_read.zs_stackp]));
			}while(this.s_read.zs_stackp > (1 << BITS));
		}

streaming:
		while ((this.s_read.zs_code = this.getcode()) > -1)
		{
			if (this.s_read.zs_code == EOB) {
				this.zs_state = state_t.S_EOB;
				return true;
			}
			
			if ((this.s_read.zs_code == CLEAR) && this.zs_block_compress > 0)
			{
				for (this.s_read.zs_code = 255; this.s_read.zs_code >= 0; this.s_read.zs_code--)
				{
					this.zs_codetab[this.s_read.zs_code] = 0;
				}
				this.zs_clear_flg = 1;
				this.zs_free_ent = FIRST - 1;
				if ((this.s_read.zs_code = this.getcode()) == -1)
					break;
			}
			this.s_read.zs_incode = this.s_read.zs_code;
			if (this.s_read.zs_code >= this.zs_free_ent)
			{
				this.zs_htab[this.s_read.zs_stackp++] = this.s_read.zs_finchar;
				this.s_read.zs_code = this.s_read.zs_oldcode;
			}
			
			while (this.s_read.zs_code >= 256)
			{
				this.zs_htab[this.s_read.zs_stackp++] = this.zs_htab[this.s_read.zs_code];
				this.s_read.zs_code = (int)this.zs_codetab[this.s_read.zs_code];
			}
			this.s_read.zs_finchar = this.zs_htab[this.s_read.zs_code];
			this.zs_htab[this.s_read.zs_stackp++] = this.s_read.zs_finchar;
			goto middleing;
		}

		goto end;

middleing:
		{
			do{
				this.s_read.reader.zreader_output((byte)(this.zs_htab[--this.s_read.zs_stackp]));
			}while(this.s_read.zs_stackp > (1 << BITS));

			//bool is_goto_end = (this.s_read.zs_code > -1)?false:true;
			bool is_goto_end = false;
			if ((this.s_read.zs_code = this.zs_free_ent) < this.zs_maxmaxcode)
			{
				this.zs_codetab[this.s_read.zs_code] = (uint)this.s_read.zs_oldcode;
				this.zs_htab[this.s_read.zs_code] = this.s_read.zs_finchar;
				this.zs_free_ent = this.s_read.zs_code + 1;
			}
			this.s_read.zs_oldcode = this.s_read.zs_incode;
			if (!is_goto_end)
			{
				goto streaming;
			}
			else
			{
				goto end;
			}
		}

end:
		return false;
	}

	private int getcode()
	{
		int gcode;
		int r_off, bits;
		byte[] bp = this.s_read.zs_gbuf;
		if (this.zs_clear_flg > 0 || this.s_read.zs_roffset >= this.s_read.zs_size || this.zs_free_ent > this.zs_maxcode)
		{
			if (this.zs_clear_flg > 0){
				this.zs_clear_flg = 0;
			}
		}
		
		UInt16 leftb = (UInt16)(this.s_read.reader.zlen - this.s_read.reader.zoff);
		UInt16 leftbits = (UInt16)(leftb * 8);
		UInt16 tbits = (UInt16)(leftbits - (this.s_read.zs_roffset & 7));

		if (tbits < this.zs_n_bits)
		{
			return -1;
		}
		
		bp = this.s_read.reader.zbuf;
		r_off = this.s_read.zs_roffset;
		bits = this.zs_n_bits;
		
		int bp_index = 0;
		bp_index += (r_off >> 3);
		r_off &= 7;

		gcode = (this.s_read.reader.zbuf [bp_index++] >> r_off);
		bits -= (8 - r_off);
		r_off = 8 - r_off;
		
		if (bits >= 8)
		{
			gcode |= (this.s_read.reader.zbuf[bp_index++] << r_off);
			r_off += 8;
			bits -= 8;
		}

		gcode |= (this.s_read.reader.zbuf [bp_index] & rmask [bits]) << r_off;
		this.s_read.zs_roffset += this.zs_n_bits;
		this.s_read.reader.zoff = (UInt16)(this.s_read.zs_roffset / 8);
		
		if (gcode == EOB)
		{
			if ((this.s_read.zs_roffset & 7) != 0)
			{
				this.s_read.reader.zoff++;
			}
			this.s_read.zs_roffset = 8*((this.s_read.zs_roffset + 7) / 8);
		}

		return gcode;
	}
}



