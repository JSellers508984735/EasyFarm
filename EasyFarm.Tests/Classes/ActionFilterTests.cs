using System;
using Xunit;
using Moq;
using MemoryAPI;
using MemoryAPI.Navigation;
using MemoryAPI.Tests;
using EasyFarm.Classes;

namespace EasyFarm.Tests.Classes
{
    public class ActionFilterTest
    {
        [Fact]
        public void AbilityNotUsableWhenDisabled()
        {
            var battleAbility = FindAbility();
            battleAbility.IsEnabled = false;
            var result = ActionFilters.BuffingFilter(null, battleAbility);
            Assert.False(result);
        }

        [Fact]
        public void AbilityNotUsableWithBlankName()
        {
            var battleAbility = FindAbility();
            battleAbility.Name = "";
            var result = ActionFilters.BuffingFilter(null, battleAbility);
            Assert.False(result);
        }

        [Fact]
        public void AbilityNotUsableWithTooLittleMp()
        {
            var battleAbility = FindAbility();
            battleAbility.Ability.MpCost = 1;

            var player = FindPlayer();
            player.MPCurrent = 0;
            var memoryAPI = FindMemoryApi(player);

            var result = ActionFilters.BuffingFilter(memoryAPI, battleAbility);
            Assert.False(result);
        }

        [Fact]
        public void AbilityNotUsableWithTooLittleTp()
        {
            var battleAbility = FindAbility();
            battleAbility.Ability.TpCost = 1;

            var player = FindPlayer();
            player.TPCurrent = 0;
            var memoryAPI = FindMemoryApi(player);

            var result = ActionFilters.BuffingFilter(memoryAPI, battleAbility);
            Assert.False(result);
        }

        [Fact]
        public void AbilityNotUsableWhenMpNotInReserveRange()
        {
            var battleAbility = FindAbility();
            battleAbility.MPReserveLow = 0;
            battleAbility.MPReserveHigh = 25;

            var player = FindPlayer();
            player.MPPCurrent = 100;
            var memoryAPI = FindMemoryApi(player);

            var result = ActionFilters.BuffingFilter(memoryAPI, battleAbility);
            Assert.False(result);
        }

        [Fact]
        public void AbilityNotUsableWhenTpNotInReserveRange()
        {
            var battleAbility = FindAbility();

            battleAbility.TPReserveLow = 1000;
            battleAbility.TPReserveHigh = 1000;

            var player = FindPlayer();
            player.TPCurrent = 1;
            var memoryAPI = FindMemoryApi(player);

            var result = ActionFilters.BuffingFilter(memoryAPI, battleAbility);
            Assert.False(result);
        }

        /// <summary>
        /// Fixed bug where MPReserve was being used to calculate whether a
        /// weaponskill could be used: should have been using TPReserve
        /// </summary>
        [Fact]
        public void MpReserveWasUseInsteadOfTpReserveWhenCheckingTpReserveValue()
        {
            var battleAbility = FindAbility();
            battleAbility.MPReserveLow = 1;
            battleAbility.MPReserveHigh = 1;

            battleAbility.TPReserveLow = 1000;
            battleAbility.TPReserveHigh = 1000;

            var player = FindPlayer();
            player.MPPCurrent = 1;
            player.TPCurrent = 1;
            var memoryAPI = FindMemoryApi(player);

            var result = ActionFilters.BuffingFilter(memoryAPI, battleAbility);
            Assert.False(result);
        }

        public IMemoryAPI FindMemoryApi(IPlayerTools player)
        {
            var memoryAPI = new Mock<IMemoryAPI>();
            memoryAPI.Setup(x => x.Player).Returns(player);
            return memoryAPI.Object;
        }

        public BattleAbility FindAbility()
        {
            var battleAbility = new BattleAbility();
            battleAbility.IsEnabled = true;
            battleAbility.Name = "valid";
            return battleAbility;
        }

        public FakePlayer FindPlayer()
        {
            var player = new FakePlayer();
            player.HPPCurrent=100;
            player.MPCurrent=10000;
            player.MPPCurrent=100;
            player.Name="Mykezero";
            player.Status=Status.Standing;
            player.TPCurrent=100;
            return player;
        }
    }
}
