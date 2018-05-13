using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class VendorCode
{
    /// <summary>
    /// HASP vendor code for demo keys
    /// </summary>
    private const string vendorCodeString =
        "AzIceaqfA1hX5wS+M8cGnYh5ceevUnOZIzJBbXFD6dgf3tBkb9cvUF/Tkd/iKu2fsg9wAysYKw7RMA" +
        "sVvIp4KcXle/v1RaXrLVnNBJ2H2DmrbUMOZbQUFXe698qmJsqNpLXRA367xpZ54i8kC5DTXwDhfxWT" +
        "OZrBrh5sRKHcoVLumztIQjgWh37AzmSd1bLOfUGI0xjAL9zJWO3fRaeB0NS2KlmoKaVT5Y04zZEc06" +
        "waU2r6AU2Dc4uipJqJmObqKM+tfNKAS0rZr5IudRiC7pUwnmtaHRe5fgSI8M7yvypvm+13Wm4Gwd4V" +
        "nYiZvSxf8ImN3ZOG9wEzfyMIlH2+rKPUVHI+igsqla0Wd9m7ZUR9vFotj1uYV0OzG7hX0+huN2E/Id" +
        "gLDjbiapj1e2fKHrMmGFaIvI6xzzJIQJF9GiRZ7+0jNFLKSyzX/K3JAyFrIPObfwM+y+zAgE1sWcZ1" +
        "YnuBhICyRHBhaJDKIZL8MywrEfB2yF+R3k9wFG1oN48gSLyfrfEKuB/qgNp+BeTruWUk0AwRE9XVMU" +
        "uRbjpxa4YA67SKunFEgFGgUfHBeHJTivvUl0u4Dki1UKAT973P+nXy2O0u239If/kRpNUVhMg8kpk7" +
        "s8i6Arp7l/705/bLCx4kN5hHHSXIqkiG9tHdeNV8VYo5+72hgaCx3/uVoVLmtvxbOIvo120uTJbuLV" +
        "TvT8KtsOlb3DxwUrwLzaEMoAQAFk6Q9bNipHxfkRQER4kR7IYTMzSoW5mxh3H9O8Ge5BqVeYMEW36q" +
        "9wnOYfxOLNw6yQMf8f9sJN4KhZty02xm707S7VEfJJ1KNq7b5pP/3RjE0IKtB2gE6vAPRvRLzEohu0" +
        "m7q1aUp8wAvSiqjZy7FLaTtLEApXYvLvz6PEJdj4TegCZugj7c8bIOEqLXmloZ6EgVnjQ7/ttys7VF" +
        "ITB3mazzFiyQuKf4J6+b/a/Y";

    private const string vendorCodeString1 = "X7FkglJvajrI7/0ZimVvKocg9F/6/dt7XsrlhLn3dm2fcbBbxyazhYT5/i71hh3PlDgr+hG006SK/bOFxCAqtOsR7SyPzeA4e61g872gaL3VR5/lL4rX1ACsySwvLo3Z7EFBXDlSARwS3VM78OrhdnZDIMdFEhd+mpE6dH6CBoACRBIx8GY7H8MDHaeKW3IrbWeXOLSwlR7C1yMcgAC4EsVaJkdITZGEHMNzNjACuAtTIhJbnpFGP7QUH/TGSDO/qe7NjqLp1SON1iLbA9YKvgSVt29vsgVsxq0SLjmV2yl9BIQ9CeU18InZMeKK3i5XUb7cGquEKT1hw70+NfBgymwIz4At31QjIKKPUI1R5ZRlQGaNg/qTXqdTb8WiqBkoVzxgDr7dPMVVjLpetP2zQiqSEdR4ztBNCHJzRPrRV5fJ3Gqq/y2tqoFrKNyeW6KoRgslTv2t1YUH+gwCXp7CeHQnxTcj6mme/BV+HOZk/tc+/1h74FL3x4aanaZmjcD79XL7fHZpUCYbMoLFVUKTJGB7OKHevbNhIpVZglbJC4+gLl1AnfW0Tj2M/VqBFWXhcONpOmHSIhP1Oal/R5dbdfx+QX59+czeREVjHx75h+OTPcUwZNXrtfHIj5AeIHiHBT/DV65JrlVK/HxrqlIKFnBrbwsZIGzmTHKEnNpdOTAlALQ+37Ff8DiQOYa8YZwYDPnn6P2znEgkNfm6tj+ZmHze8TyxnxVqhHDE8HjTxU7Lyb3BkJ3oGEXP4tPA/t7w1AwiaczZtf+UO6iA8O/ab1bWb5I47KTcM5SmGbMkBKONY0NNsS1+OvyedIWj3RDjNL20oTlfRy1XVX1JCS+691w+NYlbfM53WHQwmxOkwLdBExpOugv6xaEP8WIE1VKtZjXHhBTM6L80z7hIKZyf5x9sa+O48vHCRPLdCzI68XT4uqhP7arQujopedpbibOh9SfH6En9QBGu6mRgB8NTTQ==";



    public const string localScope = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?> " +
                                                                           "<haspscope> " +
                                                                           "   <license_manager hostname =\"localhost\" /> " +
                                                                           "</haspscope>";

    public const string defaultScope = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?> " +
                                                                        "<haspscope/> ";

   

    //加密前的数据//
    private static string[] encryptStrs = new string[] 
    {   "VfZ97CMGOLSt5DiRSpWs","mcXfUmKBPmRyELFYEREw","O9UlG6IvRWPCdT36qimA","g6RstQHqSxOHM1qedK4E","H3OygAFlU8MMl9NmZcMI","90LE3kDfVJKRUhatLDvM",
        "BXILP4BaXjJWtpyBy5dQ","2UGRCOz4YUH01xVJkwVU","uRDXpyxZ0vG5AFiQ6YDZ","WOA3biwT16Ea9NGYTql3","nLxaY2uO3GCfIU36FR37","PIugLMsI4hBkh2qdrjMb",
        "hFrmywqD6SzpQaNleKuf","ICoskgox7tytpibt0ccj","azmz70ms93wyYqyBMEUn","CwjFUKlmaEvDxyVIz5Cr","3tgLGujhcftI6GiQlxkv","vqdStehbdQrNFOGY7Y2A",
        "XnaYgYf6frqSeW35UqLE","ok742Id1g1oWN4qdGStI","Qh5aPsbViCn1mcOlsjbM","ie2hCcaQjdl6VkbseLTQ","JbZnpW8KlOjbusyA1cBU","b8WtbG6Fmoig3AVINEjY",
        "D5TzYq4zoZglCHjQz612","42QGLa2upAfpbPGXmxK7","wZOMxV0orbduKX358Zsb","YWLSkFZjsLczi5qdUqaf","qTIZ7pXdumaERdOkHSSj","RQF5U9V8vX8JqlbstkAn",
        "jNCbGTT2wy7OZtyAfLir","LKzhtDRXy85SyBWH2d1v","cHwognPRzJ4X7JjPOEJz","EEuu27NMBk22GRGXA6rD","6BrAPRMHCV07fZ35nx9I","xyoGCBKBEvZcO7rc9ZRM",
        "ZvlNolIwF6XhnfOkVrzQ","rsiTb5GqHHWlWnbsIShU","SpfZYPElIiUqvuzzuk0Y","kmd6LzCfKSTv4CWHgLI2","MjacxjBaLtRADKjP3dq6","dg7ik3z4N4PFcSGWPF8a",
        "Fd4o7NxZOFOKL044B6Qf","7a1vTxvTQfMOk8rcoyyj","y7YBGhtORQLTTgOkaZgn","03VHt1rITrJYsobrWrZr","s0TNgLqDU2H31wzzJTHv","TXQU2voyWCG8zEWHvkpz",
        "lUN0PfmsXdEd8MjOhM7D","NRK6CZknZODhHUHW3dPH","eOHdoJih0pBmg244QFxM","GLEjbtgc20ArParbC7fQ","8ICpYdf63AywohOjoyYU","zFzvKXd15bwBXpcrb0GY",
        "1CwCxHbV6MvGwxzzXro2","tztIkr9Q8ntK5FWGJT66","UwqO7b7K9XsPENjOwlOa","mtnVTV5FbyqUdVHWiMwe","Oql1GF4zc9oZM3434efi","fni7tp2udKn4lbrbRFXn",
        "Hkfdf90ofkl8UjPjD7Fr","9hck2TYjgVkdtrcqpznv","Ae9qPDWeiwii2zzyc05z","2b6wCnU8j7hnBHWGYsND","u83Co7T3lHfsaPkOKTvH","W51JbRRXmidxJXHVxleL",
        "n2YPYBPSoTcBi443jMWP","PZVVKlNMpuaGQcrb5eEU","hWS2x5LHr49LpkPiSGmY","ITP8kPJBsF7QYscqE742","aQMe6zIwug5VxAzyqzM6","CNKkTjGqvR406IXFd0ua",
        "3KHrG3Elxr24FQkNZsde","vHExtNCfy219eYHVLUVi","XEBDfxAaADZeN643ylDm","oByJ2hy4BeYjmesakNlq","QyvQP1xZDOWoVmPi6e3v","ivsWBMvUEpUtuucqSGLz",
        "Jsq2owtOG0Tx3CzxF8uD","bpn9bgrJHBRCCKXFrzcH","DmkfY0pDJcQHbRkNd1UL","4jhlKKnyKMOMKZHU0sCP","wgerxumsMnMRj752MUkT","YdbykeknNYLWSfsaym2X",
        "pa9E6YihPzJ0rnPilNK2","R76KTIgcQ9I50vcp7ft6","j43QGse6SKGazDAxTGba","K10Xscc1TlFf7LXFG8Te","cYX3fWbVUWDkGTkMsABi","EVU92G9QWwBpf1HUe1jm",
        "5SRgPq7KX7AtO9521t1q","xPPmBa5FZIyynhs9NUJu","ZMMsoU3A0jxDWpPhzmsy","qJJybE1u2TvIvxdpmOaD","SFGFXoZp3utN4FAx8fSH","kCDLK8Yj55sSDMXEUHAL",
        "LzARxSWe6GqWcUkMH8iP","dwyXkCU88gp1L2IUtA0T","Ftv46mS39Rn6ka51f2IX","7qsaT6QXbsmbTis92tr1","ynpgGQOSc3kgsqPhOV95","0kmnsANMeDil1ydoAmRa",
        "shjtfkLHfehpAGAwnOze","Tehz24JBhPfu9OXE9fhi","lbeFPOHwiqezIWlMVHZm","N8bMByFrk0cEh4ITI9Iq","e58SoiDllBaJQc51uAqu","G25Yb2Cgnc9Ooks9g28y",
        "8Z25XMAaoN7SXsQg2tQC","zWZbKwy5qo6XwzdoPVyG","1TXhxgwZrY425HAwBngL","tQUnj0uUtz37EPXDnOYP","UNRu6KsOua1cdXlLagHT","mKOATurJwLZgM5ITWHpX",
        "OHLGGepDxlYlld51I971","fEIMsYnyzWWqUlt8vBP5","HBGTfIlsAxVvttQgh2x9","9yDZ2sjnB8TA2Bdo3ufd","AvA5OchhDIRFBJAvQVXi","2sxcBWgcEjQJaRYDCnGm" };
    //加密后的数据//
    private static string[] deCodeStrs =new string[] 
    {   "rKVpm1l5BobJ2pPAsnTv3y8B+XI=","ScSLpzGfPalHfyCNGevH0IKlq1U=","PkuuwRRtwfn3Em/wE4yaI9wDwjQ=","GLXS7qbNaNOBn2dBkxBiyzuNPn0=",
        "/jKaXpoCPeif390Xwxn/GghjEgc=","TiBnt+2yvCJdFcDgQIHdilYYIX4=","O42e+3z+thiqRzIcGymIaZVyu9k=","Z7OL8COfeyRmvZWsQu+vDfS2x9c=",
        "05Lc6gF4TfnYJFTitCqPOV/ButM=","ZsblohxB72a2wicq1oL7GjeVkLE=","JdbblwHHcJ0JthSUj82ilSyycAk=","Pc7DJ5lnlhgsU6OF3GS8grd1+uc=",
        "qlMAmItBKzJdBBFjAlx/Hlpb6bE=","o8/is8YKJAtnk0dMHnKigTyQqHU=","15KJsHMapI0H8HufZkidoa1sKNQ=","B1aY2EOQpxe/Wb9hK8K51EBSR4s=",
        "InrlJOVG52RoNnxAuD1nTJC/lbs=","hvGZjZVGyZ1YJ5w0wfV7JfsA7Tc=","EN75pRYcx5heyUjFW0JLH6awOYg=","uzLcJWgvtaRtCdMHE33ZNhqLLY8=",
        "4sFkmmtbW894KtcbGX6Spew4W6g=","lkiq1pC8Gk2YexL+GSeM/CDwXKc=","vlXi0upXOYRJIaW+YO8oAgIeP30=","wVvOh17vTaS4t+vGSn3GaKXz6Fo=",
        "ufPaoUmu0Xmeks9TdIM8ecPjFkM=","G/e9Yu1KNCXvzJxRKOOtDKyM5p4=","LxCgHKT5mvUCGQFR+MmU8goOdko=","S6+HD+8j/5n/l9H+qb4hnqeuU8s=",
        "+OtmTD5bZfpgEGHarXBe9AeIGPE=","64yhSj1UZw47JGRFcD4lnSbt7tY=","gugQQlq5vAauY8Fb0jQQZw6wg94=","tFVSpcAkKCAPkp3j8oSLzqoad0U=",
        "zG/vMEtd1AiEl6Hl7SinveKcKQk=","LHNKTPMdUheslosOn/37Q+D0MD4=","FLfBiKV5Xj83Ksx+JlGqHVcLkEs=","wieR+MmG4AhpaRqPVvy9yaUknVw=",
        "wJWgIyeMyRvnwv5roYmhEPOpjdI=","BgqpsSBZlxf1Q4QqU0TFGfsQZcA=","E93I6TQfZpes0fqeSttA0TlmBiM=","BvYi4w4Pe63gMbaAFBp9KcTq/jg=",
        "Ql7DGzspvik0gN8DQ+G5IYf7zW4=","MphKl5R/uyYkad2ttq1YTz/u4e0=","nUDfMGrylU43/rwvs5CzAYQlYvY=","MYWN4ElFSf44NvsNAfIk/Rza0wM=",
        "HGASruj+KYMiFNeplnOP1vAl8yI=","AC3uVmOprRTKaD6G9AjkjXkgNOA=","EL8VbKjBkM4C4l2REXEBe2tChDI=","8/j76di1ktsJ66cDsRMWaNf1Vu8=",
        "DHiE13lab0EVD2TcPaKD+oe/FC4=","tQEgUksVq8KLOC1CNpWjunIAZ4E=","FNtSxJ8qGNUqQfuTqkUqN20LczA=","+QLCflvAfyh3mYJanBshnahxTw8=",
        "JqHF5b03o7go6r1Hy6lPYSqy184=","K/S4ufqve2KBFNXZBR85z2EUcZM=","i3lRE2rzVL6wiKlxUigkRz1oWKs=","0PHZ6c0UJsLjAqgF/usxwbtnVdM=",
        "M3e2lJGUlUH0UPVZ2UH8kWsOZxc=","cxAw97xfCmVfYsQf0OFq+Rt4UHk=","HLhHBEnieWUKJKVNY7907k86UP4=","V6f0e5AXiCvWFwXBlBaVzb8YDs0=",
        "7d/Dgz/Xq+MPhPb0Eu4v72Z9uqE=","xs9lzM15Q+KUQwrHINsZEbozqcc=","dPUPX8tjvsLsg4ENXjO9hFaL6pM=","Bw1Ju30OYTPCz6LByhN630T7YgA=",
        "eGehEVhbMQlMLZVJFjLAJEYv0ww=","4qfgdozY97QJWQFHBFP+yN3CKJU=","qjZnd99Aw6kO2Tn7G226ma58H2w=","QgGS1hBAop6tHChdqbMJHZZr+Nk=",
        "a/l0JuGbzTeZz1nRenX0/cQI5mQ=","VFsKco3j3vmEjwtEjkmwU7Fm+A8=","B23g2SonDIP0ccvjRaa+c/J7bhM=","zh9rQHggzeT1eh9Eh0afYBeDchI=",
        "gLlGNLyrSLoG2yTKMgDHVgAbokk=","cSV8m6yRj16UxsNStG6LYVyFP7c=","kYNogKSyTRpr/OuOft95z1dgxjs=","2yYr4d2LLeFHMIrkNrl5oDj1vhA=",
        "PgIRNIbamBc2g1XLGE/rFJH6kTI=","/xmtq87xX89vnp9/PTGtFz6hOVg=","TROK2Efx+Tjmt2Qz+d3pqY225eM=","+YSqHbDMKQG6lk483IMzVlq3B5g=",
        "VnmaECqcHHrP0HUZ8og8TDkRZ1c=","K2r6BcFwVUB1VMeViTVqbp8noek=","dITZXYBt9zSQTkEQ9BnV6pzSjkw=","0XjsEVypCSVepDlHYsLKQfdW2vM=",
        "Yrb0Rv5LnC4ZyI62tOFsVl20r5c=","0z6VSxZjo3G9wTZnzsB8qlHOrCg=","qaNtDNREQk7tPo86lDkAVZgAkRI=","SMCwCf28tiuJ4lW9W8fQzEdbwgI=",
        "2suM0a58E5GiYiRQjg/cBbl3jlY=","c8OJ8fDTSMYMXIYR234xH2mu9c0=","J47vm706dN+9S0CsmCsifgprG9s=","lkCHRzJwLG9Sg8VDnRl/OCkSB3k=",
        "uP8450t/s0BrRqX2g5bKdPzM8ho=","MCq8md4RC7APyF/GdZA+Rw8OvAc=","Jw3kN+gVi5ku2bnrqE+vIE+CISk=","llsl1WF5wUBQlBb2Ic2eLeYXexQ=",
        "J5IJeOtuAfT+Lmh0brjqPRdgBS4=","P+YbdUczBuwczoI25FdU5tL6s8o=","rmYubOWCmMQQ2z0usKj8qHwvgpg=","Dt0v20oz0qBfvcpwd4S3DsLZBxg=",
        "2mVM2Rpgyl/S6rCElG/npA3tLa8=","dFca19hqB6+6fJ8g0IZS23CnQZI=","jLivz7417nkELZecJcJkHfqLvks=","9NKpzwcdr8UgSZtYySfBx2x1b8w=",
        "AGUgNDyNMahEkzZ74Uz+mmEPIbM=","CdVCAarBCCSmnzjeCEKNKP2XDfQ=","A0zFG8nDf1ME8Puhh3GlGck9xHE=","qB9AHJtRLHW6TUNWoIQkkNsn1jg=",
        "ey+JXzYeNb26We4d3TvjLT1g4is=","jiGVD0k7qoeKsdULvFum5Ke53kQ=","Ob6B3MokJ34azbbvee4UrL/H+vU=","h0UXEFj8+ESgYPtRf0HoRXnKUVs=",
        "3rRjIne5Fj5KPbv3niHSW+1QX5Q=","ft/UMHpjVfE5vEzg3cnkpGTB/JQ=","vh5wqWRwiGP5FDZDwz8t2GQGp/o=","pJBcFG+4+vurBHbMnSk8eayXJ0o=",
        "iA5Mi8yF9zMspm4iJZz89Mn/zQ8=","TQ1VI7XFcwnhcFYfA8kjhAZjwGE=","7UU60zhjRfKEi/t9yLUyqMvN+W0=","rcg16GdBKEzsvNQHxH+bjyWV1oM="};

    /// <summary>
    /// Returns the vendor code as a string.
    /// </summary>
    static public string Code
    {
        get
        {
            //return vendorCodeString;
            return vendorCodeString1;
        }
    }

    public static string[] EncryptStrs
    {
        get
        {
            return encryptStrs;
        }
    }

    public static string[] DeCodeStrs
    {
        get { return deCodeStrs; }
    }

    public static string GetString(int strLength, params int[] Seed)
    {
        string strSep = ",";
        char[] chrSep = strSep.ToCharArray();
        string strChar = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z"
         + ",A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
        string[] aryChar = strChar.Split(chrSep, strChar.Length);
        string strRandom = string.Empty;
        Random Rnd;
        if (Seed != null && Seed.Length > 0)
        {
            Rnd = new Random(Seed[0]);
        }
        else
        {
            Rnd = new Random();
        }
        //生成随机字符串
        for (int i = 0; i < strLength; i++)
        {
            strRandom += aryChar[Rnd.Next(aryChar.Length)];
        }
        return strRandom;
    }

    public static byte[] copybyte(byte[] a, byte[] b)
    {
        byte[] c = new byte[a.Length + b.Length];
        a.CopyTo(c, 0);
        b.CopyTo(c, a.Length);
        return c;
    }
}
