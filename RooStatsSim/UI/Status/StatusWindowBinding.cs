﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using RooStatsSim.User;
using RooStatsSim.DB;
using RooStatsSim.DB.Table;

namespace RooStatsSim.UI.Status
{
    class StatusList : ObservableCollection<AbilityBinding<int>>
    {
        public StatusList()
        { }
        public StatusList(ref UserData param_status)
        {
            foreach (STATUS_ENUM status in Enum.GetValues(typeof(STATUS_ENUM)))
            {
                string statusName = Enum.GetName(typeof(STATUS_ENUM), status);
                Add(new AbilityBinding<int>(statusName, param_status.Status[(int)status].Point, param_status.Status[(int)status].AddPoint));
            }
        }
    }

    class NormalPropertyList : ObservableCollection<AbilityBinding<int>>
    {
        public NormalPropertyList(ref UserData user_data)
        {
            Add(new AbilityBinding<int>("HP", user_data.User_Item.i_option[ITYPE.HP], 0, Enum.GetName(typeof(ITYPE), ITYPE.HP)));
            Add(new AbilityBinding<int>("SP", user_data.User_Item.i_option[ITYPE.SP], 0, Enum.GetName(typeof(ITYPE), ITYPE.SP)));
            Add(new AbilityBinding<int>("ATK", user_data.User_Item.i_option[ITYPE.ATK], 0, Enum.GetName(typeof(ITYPE), ITYPE.ATK)));
            Add(new AbilityBinding<int>("DEF", user_data.User_Item.i_option[ITYPE.DEF], 0, Enum.GetName(typeof(ITYPE), ITYPE.DEF)));
            Add(new AbilityBinding<int>("MATK", user_data.User_Item.i_option[ITYPE.MATK], 0, Enum.GetName(typeof(ITYPE), ITYPE.MATK)));
            Add(new AbilityBinding<int>("MDEF", user_data.User_Item.i_option[ITYPE.MDEF], 0, Enum.GetName(typeof(ITYPE), ITYPE.MDEF)));
            Add(new AbilityBinding<int>("제련 ATK", user_data.User_Item.i_option[ITYPE.SMELTING_ATK], 0, Enum.GetName(typeof(ITYPE), ITYPE.SMELTING_ATK)));
            Add(new AbilityBinding<int>("제련 DEF", user_data.User_Item.i_option[ITYPE.SMELTING_DEF], 0, Enum.GetName(typeof(ITYPE), ITYPE.SMELTING_DEF)));
            Add(new AbilityBinding<int>("제련 MATK", user_data.User_Item.i_option[ITYPE.SMELTING_MATK], 0, Enum.GetName(typeof(ITYPE), ITYPE.SMELTING_MATK)));
            Add(new AbilityBinding<int>("제련 MDEF", user_data.User_Item.i_option[ITYPE.SMELTING_MDEF], 0, Enum.GetName(typeof(ITYPE), ITYPE.SMELTING_MDEF)));
            Add(new AbilityBinding<int>("HP 자연 회복", user_data.User_Item.i_option[ITYPE.HP_RECOVERY], 0, Enum.GetName(typeof(ITYPE), ITYPE.HP_RECOVERY)));
            Add(new AbilityBinding<int>("SP 자연 회복", user_data.User_Item.i_option[ITYPE.SP_RECOVERY], 0, Enum.GetName(typeof(ITYPE), ITYPE.SP_RECOVERY)));
            Add(new AbilityBinding<int>("HIT", user_data.User_Item.i_option[ITYPE.HIT], 0, Enum.GetName(typeof(ITYPE), ITYPE.HIT)));
            Add(new AbilityBinding<int>("FLEE", user_data.User_Item.i_option[ITYPE.FLEE], 0, Enum.GetName(typeof(ITYPE), ITYPE.FLEE)));
            Add(new AbilityBinding<int>("CRI", user_data.User_Item.i_option[ITYPE.CRI], 0, Enum.GetName(typeof(ITYPE), ITYPE.CRI)));
            Add(new AbilityBinding<int>("CDEF", user_data.User_Item.i_option[ITYPE.CDEF], 0, Enum.GetName(typeof(ITYPE), ITYPE.CDEF)));
        }
    }

    class AdvancedPropertyList : ObservableCollection<AbilityBinding<double>>
    {
        public AdvancedPropertyList(ref UserData user_data)
        {
            Add(new AbilityBinding<double>("ASPD", user_data.User_Item.d_option[DTYPE.ASPD], 0, Enum.GetName(typeof(DTYPE), DTYPE.ASPD)));
            Add(new AbilityBinding<double>("이동속도", user_data.User_Item.d_option[DTYPE.MOVING_SPEED], 0, Enum.GetName(typeof(DTYPE), DTYPE.MOVING_SPEED)));
            foreach(KeyValuePair<DTYPE, double> item in user_data.User_Item.d_option)
            {
                if ((item.Key == DTYPE.ASPD) || (item.Key == DTYPE.MOVING_SPEED))
                    continue;
                Add(new AbilityBinding<double>(EnumProperty_Kor.DTYPE_KOR[item.Key], user_data.User_Item.d_option[item.Key], 0, Enum.GetName(typeof(DTYPE), item.Key)));
            }
        }
    }
}