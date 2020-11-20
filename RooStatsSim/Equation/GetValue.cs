﻿using RooStatsSim.DB;
using RooStatsSim.DB.Table;
using RooStatsSim.Equation.Job;
using RooStatsSim.UI.Menu;
using RooStatsSim.User;
using System.Collections.Generic;
using System;

namespace RooStatsSim.Equation
{
    public class GetValue
    {
        UserData user;
        public GetValue()
        {
            user = UserData.GetInstance;
        }
        public UserData User_Data { get { return user; } }

        private int DefaultZeroValue(ITYPE itype)
        {
            int value = 0;
            string type_name = Enum.GetName(typeof(ITYPE), itype);
            if (user.User_Item.Option_ITYPE.ContainsKey(type_name))
                value = Convert.ToInt32(user.User_Item.Option_ITYPE[type_name]);
            return value;
        }
        private double DefaultOneValue(Dictionary<string, double> dic, string type)
        {
            double value = 1.0;
            if (dic.ContainsKey(type))
                value += (dic[type] / 100.0);
            return value;
        }
        private double DefaultOneValue_Decrese(Dictionary<string, double> dic, string type)
        {
            double value = 1.0;
            if (dic.ContainsKey(type))
                value -= (dic[type] / 100.0);
            return value;
        }


        public int WeaponATK() { return DefaultZeroValue(ITYPE.WEAPON_ATK); }
        public int SmeltingATK() { return DefaultZeroValue(ITYPE.SMELTING_ATK); }
        public int MasteryATK() { return DefaultZeroValue(ITYPE.MASTERY_ATK); }
        public int EquipATK() { return DefaultZeroValue(ITYPE.ATK); }
        public double PercentATK() { return DefaultOneValue(user.User_Item.Option_DTYPE, Enum.GetName(typeof(DTYPE), DTYPE.ATK_P)); }
        public double PercentPhysicalDamage(ATTACK_TYPE atk_type) { 
            double physical_damage = DefaultOneValue(user.User_Item.Option_DTYPE, Enum.GetName(typeof(DTYPE), DTYPE.PHYSICAL_DAMAGE));
            if (atk_type == ATTACK_TYPE.MELEE_TYPE)
                physical_damage += (1.0 - DefaultOneValue(user.User_Item.Option_DTYPE, Enum.GetName(typeof(DTYPE), DTYPE.MELEE_PHYSICAL_DAMAGE)));
            else if (atk_type == ATTACK_TYPE.RANGE_TYPE)
                physical_damage += (1.0 - DefaultOneValue(user.User_Item.Option_DTYPE, Enum.GetName(typeof(DTYPE), DTYPE.RANGE_PHYSICAL_DAMAGE)));
            return physical_damage;
        }
        public int AdditionalPhysicalDamage() { return DefaultZeroValue(ITYPE.PHYSICAL_DAMAGE_ADDITIONAL); }
        public double DefenseRatio() { return Defense.GetDefRatio(MenuBox._roo_db.Mob_db[user.SelectedEnemy].Def, 0, MenuBox._roo_db.Mob_db[user.SelectedEnemy].StatusInfo.Vit, 
                                            DefaultOneValue_Decrese(user.User_Item.Option_DTYPE, Enum.GetName(typeof(DTYPE), DTYPE.IGNORE_PHYSICAL_DEFENSE))); }
        public double ElementIncreseDamage() {
            return DefaultOneValue(user.User_Item.Option_MONSTER_ELEMENT_DMG_TYPE, Enum.GetName(typeof(ELEMENT_DMG_TYPE), (ELEMENT_DMG_TYPE)MenuBox._roo_db.Mob_db[user.SelectedEnemy].Element)); }
        public double TribeIncreseDamage() { 
            return DefaultOneValue(user.User_Item.Option_TRIBE_DMG_TYPE, Enum.GetName(typeof(TRIBE_DMG_TYPE), (TRIBE_DMG_TYPE)MenuBox._roo_db.Mob_db[user.SelectedEnemy].Tribe)); }
        public double SizeIncreseDamage() { 
            return DefaultOneValue(user.User_Item.Option_MONSTER_SIZE_DMG_TYPE, Enum.GetName(typeof(MONSTER_SIZE_DMG_TYPE), (MONSTER_SIZE_DMG_TYPE)MenuBox._roo_db.Mob_db[user.SelectedEnemy].Size)); }
        public double MonsterTypeIncreseDamage() { 
            return DefaultOneValue(user.User_Item.Option_MONSTER_KINDS_DMG_TYPE, Enum.GetName(typeof(MONSTER_KINDS_DMG_TYPE), (MONSTER_KINDS_DMG_TYPE)0)); } //MenuBox._roo_db.Mob_db[user.SelectedEnemy].IsBoss)}
        public double WeaponSizePanelty() { return AdvantageTable.GetSizePanelty(user.Equip.List[(int)EQUIP_TYPE_ENUM.WEAPON].Equip.Weapon_type, (MONSTER_SIZE)MenuBox._roo_db.Mob_db[user.SelectedEnemy].Size); }
        public double ElementInteraction() { return AdvantageTable.GetElementRatio(user.User_Item.AttackerElement, (ELEMENT_TYPE)MenuBox._roo_db.Mob_db[user.SelectedEnemy].Element); }
    }
}
