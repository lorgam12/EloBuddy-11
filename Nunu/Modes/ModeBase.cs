﻿using EloBuddy;
using EloBuddy.SDK;

namespace NinjaNunu.Modes
{
    public abstract class ModeBase
    {
        protected Spell.Targeted Q
        {
            get { return SpellManager.Q; }
        }
        protected Spell.Targeted W
        {
            get { return SpellManager.W; }
        }
        protected Spell.Targeted E
        {
            get { return SpellManager.E; }
        }
        protected Spell.Active R
        {
            get { return SpellManager.R; }
        }

        protected Spell.Targeted Smite
        {
            get { return SpellManager.Smite; }
        }

        protected bool HasSmite
        {
            get { return SpellManager.HasSmite(); }
        }

        protected bool ChannelingR()
        {
            return Player.Instance.Spellbook.IsChanneling;
        }

        public abstract bool ShouldBeExecuted();

        public abstract void Execute();
    }
}
