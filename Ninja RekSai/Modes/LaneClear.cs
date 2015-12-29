﻿using EloBuddy;
using EloBuddy.SDK;
using System.Linq;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Enumerations;

using Settings = RekSai.Config.LaneClear.LaneClearMenu;

namespace RekSai.Modes
{
    public sealed class LaneClear : ModeBase
    {
        private static bool burrowed = false;

        

        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear);
        }

        public override void Execute()
        {
            if (Player.Instance.HasBuff("RekSaiW"))
                burrowed = true;
            else burrowed = false;

            var minionsE = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.Position, E.Range).OrderByDescending(a => a.MaxHealth).FirstOrDefault();
            var minionsQ2 = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.Position, 800).OrderByDescending(a => a.MaxHealth).FirstOrDefault();
            var minionsW = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.Position, Player.Instance.BoundingRadius + 175).OrderByDescending(a => a.MaxHealth).FirstOrDefault();

            if (burrowed)
            {
                if (Q2.IsOnCooldown && W.IsReady() && Settings.UseW && minionsW != null)
                {
                    W.Cast();
                    return;
                }

                if (Settings.UseQ2 && Q2.IsReady() && minionsQ2 != null)
                {
                    Q2.Cast(minionsQ2);
                    return;
                }


            }

            if (!burrowed)
            {
                if (Settings.UseE && E.IsReady())
                {
                    if (minionsE.Health <= SmiteDamage.EDamage(minionsE))
                    {
                        E.Cast(minionsE);
                        return;
                    }
                    if (Player.Instance.Mana == 100)
                    {
                        E.Cast(minionsE);
                        return;
                    }
                }

                if (W.IsReady() && Settings.UseW && EntityManager.MinionsAndMonsters.Combined.Where(a => a.IsValidTarget(400)).Count() < 1)
                {
                    W.Cast();
                    return;
                }

                if (Q2.IsReady() && minionsQ2 != null && !minionsW.HasBuff("reksaiknockupimmune") && !Player.Instance.HasBuff("RekSaiQ"))
                {
                    W.Cast();
                    return;
                }
            }
        }

    }
}
