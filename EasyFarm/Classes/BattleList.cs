﻿
/*///////////////////////////////////////////////////////////////////
<EasyFarm, general farming utility for FFXI.>
Copyright (C) <2013>  <Zerolimits>

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
*/
///////////////////////////////////////////////////////////////////

using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyFarm.Classes
{
    public class BattleList : BindableBase
    {
        public BattleList() { }

        public BattleList(string name)
        {
            _name = name;
            _actions = new ObservableCollection<BattleAbility>();
            _actions.Add(new BattleAbility() { Name = "Default" });
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private ObservableCollection<BattleAbility> _actions;

        public ObservableCollection<BattleAbility> Actions
        {
            get { return _actions; }
            set { SetProperty(ref _actions, value); }
        }
    }
}