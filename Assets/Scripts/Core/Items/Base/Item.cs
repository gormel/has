using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Common;
using Assets.Scripts.Core.Common.ParameterStatuses;

namespace Assets.Scripts.Core.Items.Base
{
    public abstract class Item
    {
        public List<string> PropertyDescriptions { get; } = new List<string>();

        private (ParameterStatus Status, Func<Player, Parameter> Param)[] mParameters;

        protected abstract (ParameterStatus Status, Func<Player, Parameter> Param)[] Parameters();

        public void OnPuton(Player player)
        {
            if (mParameters == null)
                mParameters = Parameters();

            foreach (var parameter in mParameters)
            {
                parameter.Param(player).Statuses.Add(parameter.Status);
            }
        }

        public void OnPutOff(Player player)
        {
            if (mParameters == null)
                mParameters = Parameters();

            foreach (var parameter in mParameters)
            {
                parameter.Param(player).Statuses.Remove(parameter.Status);
            }
        }
    }
}
