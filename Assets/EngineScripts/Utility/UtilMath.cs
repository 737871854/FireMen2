/**
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 2016,广州擎天柱网络科技有限公司
 * All rights reserved.
 *
 * 文件名称：UtilMath.cs
 * 简    述: 导出给lua使用的数学方便函数，在这里实现排列组合函数(非递归实现)。以后包括随机，空间
 * 位置判断函数，放在此文件中。
 *      但是时间相隔的Util函数请放在UtilTime.cs文件中。请勿随便存放。
 */

using UnityEngine;
using System;
using System.Collections.Generic;


public class UtilMath
{
    /// <summary>   
    /// 排列循环方法   
    /// </summary>   
    /// <param name="N"></param>   
    /// <param name="R"></param>   
    public static long P1(int N, int R)
    {
        if (R > N || R <= 0 || N <= 0)
        {
            //throw new ArgumentException("params invalid!");
            return 1;//为了避免组合除零错误;
        }
        long t = 1;
        int i = N;

        while (i != N - R)
        {
            try
            {
                checked
                {
                    t *= i;
                }
            }
            catch
            {
                throw new OverflowException("overflow happens!");
            }
            --i;
        }
        return t;
    }

    /// <summary>   
    /// 排列堆栈方法   
    /// </summary>   
    /// <param name="N"></param>   
    /// <param name="R"></param>   
    /// <returns></returns>   
    public static long P2(int N, int R)
    {
        if (R > N || R <= 0 || N <= 0) throw new ArgumentException("arguments invalid!");
        Stack<int> s = new Stack<int>();
        long iRlt = 1;
        int t;
        s.Push(N);
        while ((t = s.Peek()) != N - R)
        {
            try
            {
                checked
                {
                    iRlt *= t;
                }
            }
            catch
            {
                throw new OverflowException("overflow happens!");
            }
            s.Pop();
            s.Push(t - 1);
        }
        return iRlt;
    }

    /// <summary>   
    /// 组合   
    /// </summary>   
    /// <param name="N"></param>   
    /// <param name="R"></param>   
    /// <returns></returns>   
    public static long C(int N, int R)
    {
        return P1(N, R) / P1(R, R);
    }

}
