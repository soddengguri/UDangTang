using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Container.Popup
{
    public class PopupInfo
    {

        private PopupInfo(Builder builder)
        {
            this.Title = builder.Title;
            this.Timer = builder.Timer;
            this.Pieces = builder.Pieces;
            this.Bonus = builder.Bonus;
            this.Buttons = builder.Buttons;
            this.Listener = builder.Listener;
            this.Animation = builder.Animation;
            this.PauseScene = builder.PauseScene;
        }

        public bool PauseScene { get; private set; }
        public string Title { get; private set; }
        public string Timer { get; private set; }
        public string Pieces { get; private set; }
        public string Bonus { get; private set; }
        public PopupButtonType[] Buttons { get; private set; }
        public System.Action<PopupButtonType> Listener { get; private set; }
        public PopupAnimationType Animation { get; private set; }


        public class Builder
        {
            public Builder()
            {
                this.Title = string.Empty;
                this.Timer = string.Empty;
                this.Pieces = string.Empty;
                this.Bonus = string.Empty;
                this.Buttons = null;
                this.Listener = null;
                this.Animation = PopupAnimationType.None;
                this.PauseScene = false;
            }
            internal string Title { get; private set; }
            internal string Timer { get; private set; }
            internal string Pieces { get; private set; }
            internal string Bonus { get; private set; }
            internal bool PauseScene { get; private set; }
            internal PopupButtonType[] Buttons { get; private set; }
            internal System.Action<PopupButtonType> Listener { get; private set; }
            internal PopupAnimationType Animation { get; private set; }

            public Builder SetTitle(string title)
            {
                this.Title = title;
                return this;
            }

            public Builder SetTimer(string timer)
            {
                this.Timer = timer;
                return this;
            }

            public Builder SetPieces(string pieces)
            {
                this.Pieces = pieces;
                return this;
            }

            public Builder SetBonus(string bonus)
            {
                this.Bonus = bonus;
                return this;
            }

            public Builder SetButtons(params PopupButtonType[] buttons)
            {
                this.Buttons = buttons;
                return this;
            }

            public Builder SetListener(System.Action<PopupButtonType> listener)
            {
                this.Listener = listener;
                return this;
            }

            public Builder SetAnimation(PopupAnimationType animation)
            {
                this.Animation = animation;
                return this;
            }

            public Builder SetPauseScene(bool isPause)
            {
                this.PauseScene = isPause;
                return this;
            }

            public PopupInfo Build()
            {
                return new PopupInfo(this);
            }
        }
    }
}