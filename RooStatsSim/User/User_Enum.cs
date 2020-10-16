﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RooStatsSim.User
{
    enum LEVEL_ENUM : int
    {
        BASE = 0,
        BASE_POINT,
        JOB,
        JOB_POINT,
    }
    enum STATUS_ENUM : int
    {
        STR = 0,
        AGI,
        VIT,
        INT,
        DEX,
        LUK
    }
    enum MEDAL_ENUM : int
    {
        VALOR,
        GUARDIAN,
        WISDOM,
        CHARM,
        GALE,
    }
    enum RIDING_ENUM : int
    {
        ATK_MATK,
        MAX_HP,
        ATK_MATK_PERCENT,
    }
}